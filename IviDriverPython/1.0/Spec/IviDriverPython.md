
# IVI Driver Python Specification

| Version Number | Date of Version | Version Notes                                  |
|----------------|-----------------|------------------------------------------------|
| 0.3            | June 30 2025    | First version on the IVI Foundation repository |
| 0.2            | June 2025       | LXI Working group changes                      |
| 0.1            | May 2025        | Preliminary Draft for LXI Development          |

## Abstract

This specification contains the Python specific requirements for an IVI-Python driver, it is an IVI Language-Specific specification.
Drivers that comply with this specification are also required to comply with the *IVI Driver Core Specification*.

## Authorship

> [!NOTE]
> Standard IVI Boilerplate included, but changed IVI to LXI for now.

This specification is developed by member companies of the LXI Foundation. Feedback is encouraged. 
To view the list of member vendors or provide feedback, please visit the IVI Foundation website at [www.ivifoundation.org](https://www.ivifoundation.org).

## Warranty

The IVI Foundation and its member companies make no warranty of any kind with regard to this material, including, but not limited to, the implied warranties of merchantability and fitness for a particular purpose. The IVI Foundation and its member companies shall not be liable for errors contained herein or for incidental or consequential damages in connection with the furnishing, performance, or use of this material.

## Trademarks

Product and company names listed are trademarks or trade names of their respective companies.

No investigation has been made of common-law trademark rights in any work.

## Table of Contents

- [IVI Driver Python Specification](#ivi-driver-python-specification)
  - [Abstract](#abstract)
  - [Authorship](#authorship)
  - [Warranty](#warranty)
  - [Trademarks](#trademarks)
  - [Table of Contents](#table-of-contents)
  - [Overview of the IVI-Python Driver Language Specification](#overview-of-the-ivi-python-driver-language-specification)
    - [Substitutions](#substitutions)
  - [IVI-Python Driver Architecture](#ivi-python-driver-architecture)
    - [Style Guide](#style-guide)
    - [Bitness](#bitness)
    - [Target Python Versions](#target-python-versions)
    - [IVI-Python Packages](#ivi-python-packages)
      - [Distribution Package Naming](#distribution-package-naming)
      - [Top Package Naming](#top-package-naming)
    - [IVI-Python Driver Structure](#ivi-python-driver-structure)
    - [Repeated Capabilities](#repeated-capabilities)
      - [Collection Style Repeated Capabilities and the Hierarchy](#collection-style-repeated-capabilities-and-the-hierarchy)
      - [Repeated Capability Reference Property Naming](#repeated-capability-reference-property-naming)
    - [IVI-Python Error Handling](#ivi-python-error-handling)
    - [Documentation and Source Code](#documentation-and-source-code)
  - [Base IVI-Python API](#base-ivi-python-api)
    - [Required Driver API Mapping Table](#required-driver-api-mapping-table)
    - [Constructor](#constructor)
      - [Python Constructor Prototype](#python-constructor-prototype)
    - [IVI-Python Utility Interface](#ivi-python-utility-interface)
    - [Direct IO Properties and Methods](#direct-io-properties-and-methods)
  - [Package Requirements](#package-requirements)
    - [Package Configuration File Content](#package-configuration-file-content)
  - [IVI-Python Driver Conformance](#ivi-python-driver-conformance)
    - [Driver Registration](#driver-registration)

## Overview of the IVI-Python Driver Language Specification

This specification contains the Python specific requirements for an IVI-Python driver, it is an IVI Language-Specific specification. Drivers that comply with this specification are also required to comply with the *IVI Driver Core Specification*.

This specification has several recommendations (identified by the use of the work *should* instead of *shall* in the requirement). These are included to provide a more consistent customer experience. However, in general, design decisions are left to the driver designer.

### Substitutions

This specification uses paired angle brackets to indicate that the text between the brackets is not the actual text to use, but instead indicates the text that is used in place of the bracketed text. The *IVI Driver Core Specification* describes these substitutions.

## IVI-Python Driver Architecture

This section describes how IVI-Python instrument drivers use Python. This section does not attempt to describe the technical features of Python, except where necessary to explain a particular IVI-Python feature. This section assumes that the reader is familiar with Python technology.

### Style Guide

IVI-Python drivers shall comply with PEP-8 (*Style Guide for Python Code*).

### Bitness

The IVI Python standard does not require certain operating systems and releases.
The drivers shall be provided as 64-bit version, the 32-bit drivers are optional.
The compliance document for an IVI driver states whether the driver is available in a 64-bit version, a 32-bit version, or both.

### Target Python Versions

IVI-Python drivers shall target Python 3.8 or later.

### IVI-Python Packages

IVI-Python drivers shall be organized as a package, including a top-package `*__init__.py*` file.

#### Terms used
- `<Dist-pckg-name>` - Python Distribution Package Name. Example: `vendorxy-specan`
- `<Top-pckg-name>` - driver's top-level package name. Example:  `vendorxy_specan`
 
#### Distribution Package Naming
The name of the distribution package for the driver shall follow the [Python naming guideline](https://packaging.python.org/en/latest/specifications/name-normalization/):
The name should be lowercased with all runs of the characters ., -, or _ replaced with a single - character. This can be implemented in Python with the re module:

`<Dist-pckg-name>` composition:
- Variant 1: `<VendorPrefix>-<Instrument>` Example: `vendorxy-specan`
- Variant 2: `<VendorPrefix>-<Instrument>-<CustomSuffix>`. Example: `vendorxy-mrtester-lte`

Variant 2 can be used for cases where the instrument contains more sub-systems, and the driver is designed only for one of them.
`<Instrument>` can represent a specific model, or a family of instruments.

#### Top Package Naming

`<Top-pckg-name>` should be derived from the distribution package name, and shall follow python package naming guideline - all lower case, words separated by underscore.
For example, if a distribution package name is `vendorxy-specan` the top-level package should be called `vendorxy_specan`.

#### Reference Property and Class Naming

For reference exposed to the user, IVI-Python drivers shall follow the PEP-8 Python naming guidelines.

### IVI-Python Driver Structure

#### Terms used
- `<Driver-identifier>` - driver's name and the top-level class name: Example: `VendorxySpecan`
- `<Sub-pckg-name>` - further driver's packages: Example: `measurement_group`
- `<Reference-property>` - further driver's interface access property: Example: `measurement`
- `<Interface-class>` - further driver's interface class name. Example: `MeasurementCls`

- [!NOTE]: need to discuss/specify the Sub-package-name, module names, class names to avoid ambivalence.

The main class `<Driver-identifier>` defined in `root.py` shall include one or multiple properties `<Reference-property>`. A reference property is used to access the driver's structure. Each reference property returns an instance of `<Interface-class>`. `<Interface-class>` may in turn include further `<Reference-property>`(s), and so on. The hierarchy may be arbitrarily deep. Sub-modules within the driver may be named at the driver vendors discretion.

Recommended instrument driver structure:

[!NOTE] - structure for discussion on 'shall' and 'should', or whether even to suggest a driver structure and sub-package naming.

```
<dist-pckg-name>/                ← The project root
│
├── LICENSE
├── pyproject.toml
├── README.md
│ 
└─ <top-pckg-name>/             ← The root package of the driver
    ├── __init__.py             ← At minimum, should contain import: from .root import <Driver-identifier>
    ├── root.py                 ← The module containing the driver's top-level class <Driver-identifier>
    ├── ivi_utility_module.py   ← Features required by the Ivi Core Specification.
    ├── direct_io_module.py     ← Features required by the Ivi Core Specification for SPCI - based instruments.
    │
    ├── <sub-pckg-name-1>/     
    │   ├── __init__.py
    │   └── <sub-pckg-module-name-1>  <- Contains class definition for `<Interface-class-1>`
    │
    └── <sub-pckg-name-2>/
        ├── __init__.py
        └── <sub-pckg-module-name-2>
            │
            ├── <sub-pckg-name-2-1>/
            │   ├── __init__.py
            │   └──<sub-pckg-module-name-1-1>
            │
            └── <sub-pckg-name-2-2>/
                ├── __init__.py
                └──<sub-pckg-module-name-1-1>
```
Concrete example of the driver's tree structure:

```
vendorxy-specan/                ← The project root
│
├── LICENSE
├── pyproject.toml
├── README.md
│ 
└─ vendorxy_specan/              ← The root package of the driver
    ├── __init__.py              ← At minimum, should contain import: from .root import VendorXySpecan
    ├── root.py                  ← The module containing the driver's top-level class VendorXySpecan
    │                            VendorXySpecan contains: 
    │                              <Reference-property> configuration instantiating ConfigurationCls from configuration_module.py
    │                              <Reference-property> measurement instantiating MeasurementCls from measurement_module.py
    │
    ├── ivi_utility_module.py    ← Features required by the Ivi Core Specification.
    ├── direct_io_module.py      ← Features required by the Ivi Core Specification for SPCI - based instruments
    │
    ├── configuration_group/     
    │   ├── __init__.py
    │   └── configuration_module.py  <- Contains class definition for <Interface-class-1> ConfigurationCls
    │
    └── measurement_group/
        ├── __init__.py
        ├── measurement_module.py      <- Contains class definition for <Interface-class-2> MeasurementCls
        │                                 MeasurementCls contains: 
        │                                   <Reference-property> spectrum instantiating SpectrumCls from spectrum_module.py
        │                                   <Reference-property> marker instantiating MarkerCls from marker_module.py
        │
        ├── spectrum_group/
        │   ├── __init__.py
        │   └── spectrum_module.py    <- Contains class definition for <Interface-class-2-1> SpectrumCls
        │
        └── marker_group/
            ├── __init__.py
            └── marker_module.py    <- Contains class definition for <Interface-class-2-1> MarkerCls
```

Following the concrete driver structure example above, consider the following user python code: 

```python
io = VendorXySpecan("TCPIP::192.168.1.101")
trace = io.measurement.spectrum.get_trace()
```

`io` is a reference to an instance of the main class `VendorXySpecan`. The main class contains a reference property `measurement` (type `MeasurementCls`). `MeasurementCls` contains a reference property `spectrum` (type `MeasurementCls`). `SpectrumCls` contains the method `get_trace()`.

> **Observation:**
> > As the user types each of these names, Code-completion makes navigating the hierarchy easy. It displays a dropdown list of methods and properties in the corresponding class or interface. After typing `io` followed by a period, a list of all the properties and methods in `io` appears, allowing the user to select one. After selecting `measurement` and typing the period, a list of the methods and properties in `MeasurementCls` appears. After selecting `spectrum` and typing the period, a list of the methods and properties in `SpectrumCls` appears, and the user can see and select `get_trace()`.
> 

[!NOTE] Should we require type hints?

### Repeated Capabilities

Repeated capabilities may be represented in two ways in IVI-Python drivers. Repeated capability instances may be specified by:

1) a method that selects the active instance (the *selector style*) for subsequent operations
2) selecting a particular instance from a collection (the *collection style*). 

See the *IVI Core Driver Specification* for details.

For IVI-Python drivers, collection style repeated capabilities are recommended.

#### Collection Style Repeated Capabilities and the Hierarchy

Collection style repeated capabilities consist of at least two classes. The first is the collection itself, and the second is the object returned by the subscript operator (`[]`) of the collection. In the hierarchy, a reference property returns the collection object. Then the collection's subscript operator `[]` is used to return an item from the collection. Each item in the collection represents one instance of the repeated capability.

Collection style repeated capabilities may be indexed by a string, enum, integer, or other Python object.

Consider the following example code:

```Python
my_peak = io.measurement.marker["B"].peak
```

`io` is a reference to the main class. `io` contains an interface reference property named `measurement`,
which is not returns a reference to the trace collection.
The subscript operator (`["B"]`) selects the item named "B" from the collection and returns a reference to an object that uniquely represents the "B" marker. That interface or class contains the property `peak`.

Collections may be implemented in a variety of ways.

- Many collections do not need to add or remove members after the driver is constructed. These can be implemented as Python read-only collections.

- Applications that need to dynamically add or remove methods can use appropriate types, such as *dictionary*.

- Developers may create custom collections by implementing __get_item(), __set_item(), __iter__() and __next__().

#### Repeated Capability Reference Property Naming

Drivers should name the classes and interfaces associated with Repeated Capability Reference Properties as described in this section.

In the following `<RcName>` is the name of the repeated capability.

- Repeated capability collection classes should be named: `<RcName>Collection`

- The interface or class returned by the collection's Item operator should be named: `<RcName>`

- The interface or class returned by the collection's Item operator should include a property called *name*. *name* returns the physical repeated capability identifier defined by the specific driver for the repeated capability that corresponds to the index that the user specifies.

For example, consider a marker repeated capability. The `<RcName>` = `Marker`, and the collection class is `MarkerCollection`:

```python
from typing import Dict

class MarkerIndex(Enum):
    """Enum indexer of the Marker Collection items."""
    Marker_1 = 1
    Marker_2 = 2
    Marker_3 = 3

class Marker:
    """Marker functions of the instrument."""

    def __init__(self, name: str, value: int):
        self._name: str = name
        self._value: int = value
        self._x_coor: float = 1E6
        self._y_coor: float = -10.0

    @property
    def name(self) -> str:
        """Logical name representing the command value for the user."""
        return self._name

    @property
    def value(self) -> int:
        """Command value for the instrument."""
        return self._value

    @property
    def x_coor(self) -> float:
        return self._x_coor
    
    @property
    def y_coor(self) -> float:
        return self._y_coor

class MarkerCollection(dict):
    """Collection of the Marker items."""

    def __init__(self):
        super().__init__()
        self["Marker1"] = Marker("Marker1", 1)
        self["Marker2"] = Marker("Marker2", 2)
        self["Marker3"] = Marker("Marker3", 3)
```

### Driver Structure Interfaces

Python IVI Drivers use tree-like structure of interfaces, some with repeated capabilities, and some without.
Consider a Spectrum Analyzer driver with a non-repeated capabilities reference property `measurement` which contains repeated capabilities reference property `marker`:

```python
io = VendorXySpecan("TCPIP::192.168.1.101")
```
Interface accessor without the repeated capability shall be implemented as read-only property:

```python
ax = io.measurement
```

Reference property with the repeated capability shall be implemented as a read-only property returning the whole collection of the items.
Indexer data type of the collection, shall be enum and string. If it makes sense, for example if the underlying communication uses SCPI commands, the driver should implement integer indexer:

```python
marker_items_collection = io.measurement
marker_item_1a = io.measurement.marker[MarkerIndex.Marker_1]  # Enum indexer
marker_item_1b = io.measurement.marker['Marker_1']  # String indexer
marker_item_1c = io.measurement.marker[1]  # Optional integer indexer
```
In addition, to improve the user experience by utilizing the code-completion, the drivers shall implement a method-like accessors with enum and string parameter data types. 
The method accessor shall have the same name as the property, with the suffix `_item`:

```python
marker_item_2a = io.measurement.marker_item(VerticalIndex.Vertical_2)  # Enum selector
marker_item_2b = io.measurement.marker_item('Vertical_2')  # String selector
marker_item_2c = io.measurement.marker_item(2)  # Optional integer selector
```
[!NOTE] Zen of Python - do not have duplicated features. If there is a way to get a good code-completion without the `marker_item` methods, we should not require them.

### IVI-Python Error Handling

All IVI-Python instrument drivers shall consistently use the standard Python exception mechanism to report errors. Neither return values nor *out* parameters shall be used to return error information.

Discussion 2025/06/24: Should we define an exception class and its specific fields? Often, there is a need to pair the raised exception to a driver and an instance of the driver.

> **Observation:**
> > The method `query_instrument_error()` is used to handle errors within the instrument that may not be thrown as Python exceptions.

### Documentation and Source Code

This specification does not have specific requirements on the format or distribution method of documentation and source code other than those called out in *IVI Driver Core Specification*.

> **Observation:**
> > Driver developers shall provide the doc-string style documentation and are encouraged to provide an online documentation. The package shall include a README.md file that directs customers where they can find additional material.

## Base IVI-Python API

This section gives a complete description of each constructor, method, or property required for an IVI-Python Core driver. The following table shows the mapping between the required base driver APIs described in the IVI Driver Core specification and the corresponding IVI-Python specific APIs described in this section.

### Required Driver API Mapping Table

| Required Driver API (IVI Driver Core) | IVI-Python API                          |
|---------------------------------------|-----------------------------------------|
| Initialization                        | Driver Constructors                     |
| Driver Version                        | Property: driver_version                |
| Driver Vendor                         | Property: driver_vendor                 |
| Error Query                           | Method: error_query()                   |
| Instrument Manufacturer               | Property: instrument_manufacturer       |
| Instrument Model                      | Property: instrument_model              |
| Query Instrument Status Enabled       | Property: query_instrument_status       |
| Reset                                 | Method: reset()                         |
| Simulate Enabled                      | Property: simulate                      |
| Supported Instrument Models           | Property: supported_instrument_models   |

### Constructor

In IVI-Python, the constructor provide the initialization functionality described in *IVI Driver Core Specification*. This section specifies the required IVI-Python specific driver constructors.

#### Python Constructor Prototype

The IVI-Python drivers shall implement a constructor with the following prototype:

  `<DriverIdentifier>(resource_name: str, id_query: bool, reset: bool, options: dict or None = None)` 

Example for DriverIdentifier `VendorXySpecan`:

```Python
"""Module root.py"""
class VendorXySpecan:
 
    def __init__(self, resource_name: str, id_query: bool = True, reset: bool = False, options: dict or None = None):
        # Initialization of the Specan.
        self.io: Resource = pyvisa.ResourceManager().open_resource(resource_name)
        self.id_query: bool = id_query
        self.reset: bool = reset
        self.options: dict = options
```

Python TypedDict is a recommended data type compared to a standard dictionary. In run-time, it is a standard dict type,
but the advantage is code-completion and type hinting in static analysis. Example:

```Python
from typing import TypedDict

class Options(TypedDict, total=False):
  """Definition of the driver's Options items."""
  simulate: bool
  clear_status_on_init: str
  block_data_chunk: int

opt = Options()
opt['simulate'] = True
opt['block_data_chunk'] = 12
opt['simulate'] = 0 # static analysis shows an error on value type
opt['something'] = False # static analysis shows an error on key name
```

IVI-Python drivers may provide an additional optional parameters for the client to specify driver options (such as simulation or options as string). The mechanism by which these parameters are passed is driver-specific.

### IVI-Python Utility Interface

IVI-Python drivers shall implement the class defined in this section. The driver shall provide an interface reference property to acquire the drivers instance of the class. 

The reference property shall be named *ivi_utility*, shall be available in the root driver class.
The driver developer is responsible for defining an instantiable class that inherits from `IviUtility`.

```Python
from abc import ABC, abstractmethod
from typing import Any, List, Tuple

class IviUtility(ABC):
  
  @property
  @abstractmethod
  def driver_version(self) -> str:
    pass

  @property
  @abstractmethod
  def driver_vendor(self) -> str:
    pass

  @property
  @abstractmethod
  def instrument_manufacturer(self) -> str:
    pass

  @property
  @abstractmethod
  def instrument_model(self) -> str:
    pass

  @property
  @abstractmethod
  def instrument_serial_number(self) -> str:
    pass

  @property
  @abstractmethod
  def instrument_firmware(self) -> str:
    pass

  @property
  @abstractmethod
  def query_instrument_status_enabled(self) -> bool:
    pass

  @property
  @abstractmethod
  def simulation_enabled(self) -> bool:
    pass

  @abstractmethod
  def error_query(self) -> ErrorQueryResult or None:
    """Returns all the errors currently reported in the instrument's error queue, the last error occurred comes first.
    Returns None if no error is present."""
    pass

  @abstractmethod
  def check_status(self) -> None:
    """Combines error_query_all and Exception rising when some errors are detected.
    [!NOTE] -still need to discuss this requirement, maybe just state 'should'?"""
    pass

  @abstractmethod
  def reset(self):
    pass

  @property
  @abstractmethod
  def supported_instrument_models(self) -> Tuple[str, ...]:
    """Returns supported models, one per element."""
    pass


class ErrorQueryResult:

  def __init__(self):
    self._errors: List[Tuple[int, str]] = []

  def add_error(self, code: int, message: str) -> None:
    """Adds one error to the error list."""
    el = (code, message)
    self._errors.append(el)

  def has_errors(self) -> bool:
    """Returns true, if there is at least one error in the error list."""
    return len(self._errors) > 0

  def get_last_error(self) -> Tuple[int, str] or None:
    """Returns the error that occurred as the last.
    In this implementation, this is the error first put in the list, meaning item with index 0."""
    if not self.has_errors():
      return None
    return self._errors[0]

  def get_errors(self) -> List[Tuple[int, str]]:
    """Returns all the errors as a list, last occurred error has index 0."""
    return self._errors
```
Discussion 2025/06/24: We chose just one method for error query: error_query() which reads all the errors from the instrument's error queue.
check_status(): discuss the purpose and the name.
Python warnings in the drivers?

Python-specific Notes (see *IVI Driver Core Specification* for general requirements):

- Drivers are permitted to implement a Set accessor on `simulate`. However, if they do so, they shall properly manage the driver state when turning simulation on and off.

### Direct IO Properties and Methods

Per the *IVI Driver Core specification*, IVI Drivers for instruments that have an ASCII command set such as SCPI shall also provide API for sending messages to and from the instrument over the ASCII command channel. This section specifies those properties and methods.

The interface reference property should be named *ivi_direct_io*. The interface reference property should be available on the root driver class.

```Python
from abc import ABC, abstractmethod
from typing import Any, List

class IviDirectIo(ABC):
    def __init__(self, io: Resource):
      self._io = io

    @property
    def session(self):
      return self._io
 
    @property
    @abstractmethod
    def io_timeout_ms(self) -> int:
        pass
 
    @io_timeout_ms.setter
    @abstractmethod
    def io_timeout_ms(self, timeout_ms: int) -> None:
        pass

    @abstractmethod
    def read_bytes(self, count: int) -> bytes:
        pass
 
    @abstractmethod
    def read_string(self) -> str:
        pass
 
    @abstractmethod
    def write_bytes(self, data: bytes) -> None:
      pass
 
    @abstractmethod
    def write_string(self, data: str) -> None:
      pass
```

Notes:

- The *optional* `session` property should return the underlying IO library.

## Package Requirements

The following sections detail the package requirements. The driver shall use the modern package format with *pyproject.toml* file.

All IVI-Python driver packages shall include the following files:

- The driver
- Readme File (`README.md`)
- Project TOML file (`pyproject.toml`)

### Package Configuration File Content

*pyproject.toml* shall include the following TOML tables. The actual values in the examples are for demonstration purposes only:

#### Build-System

Build system defines what backend to use for building and packaging the driver.
Minimum content:

```toml
[build-system]
requires = ["setuptools>=42", "wheel"]
build-backend = "setuptools.build_meta"
```

#### Project

Project meta-information. Minimum content: 

```toml
[project]
name = "vendorxy-specan"
version = "1.0"
requires-python = ">= 3.8"
authors = [ {name = "VendorXy"} ]
description = "This is a short description for the vendorxy-specan"
readme = {file = "README.md", content-type = "text/markdown"}
license = "MIT"
classifiers = [ "Programming Language :: Python" ]

[project.urls]
Documentation = "https://readthedocs.org"
```

Optional content:
```toml
[project]
dependencies = ["pyvisa"]
keywords = ["vendorxy", "specan", "signal", "analysis"]

```

[!NOTE] - SUPPORTED_INSTRUMENTS = "comma separated list" in the toml file? is that possible? Maybe as classifier?

## IVI-Python Driver Conformance

IVI-Python Drivers are required to conform to all the rules in this document. They are also required to be registered on the IVI website.

Drivers that satisfy these requirements are IVI-Python drivers and may be referred to as such.

Registered conformant drivers are permitted to use the IVI Conformant Logo.

### Driver Registration

Driver providers wishing to obtain and use the IVI Conformance logo shall submit to the IVI Foundation the driver compliance document described in *IVI Driver Core Specification*, Section [Driver Conformance](../../../IviDriverCore/1.0/Spec/IviDriverCore.md#driver-conformance) along with driver information and a point of contact for the driver. The information shall be submitted to the [IVI Foundation website](https://ivifoundation.org). Complete upload instructions are available on the site. Driver vendors who submit compliance documents may use the IVI Conformant logo graphics.

The IVI Foundation may make some driver information available to the public for the purpose of promoting IVI drivers. All information is maintained in accordance with the IVI Privacy Policy, which is available on the IVI Foundation website.
