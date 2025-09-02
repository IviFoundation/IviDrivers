# IVI Driver Python Specification

| Version Number | Date of Version | Version Notes                         |
|----------------|-----------------|---------------------------------------|
| 0.4            | July 2025       | Transferred to IVI Foundation repo    |
| 0.2            | June 2025       | LXI Working group changes             |
| 0.1            | May 2025        | Preliminary Draft for LXI Development |

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
    - [IVI-Python Naming](#ivi-python-naming)
    - [IVI-Python Packages](#ivi-python-packages)
      - [IVI-Python Package Versioning](#ivi-python-package-versioning)
      - [IVI-Python Packages Naming](#ivi-python-distribution-packages-naming)
    - [IVI-Python Driver Classes](#ivi-python-driver-classes)
    - [IVI-Python Hierarchy](#ivi-python-hierarchy)
      - [Reference Property and Class Naming](#reference-property-and-class-naming)
    - [Repeated Capabilities](#repeated-capabilities)
      - [Collection Style Repeated Capabilities and the Hierarchy](#collection-style-repeated-capabilities-and-the-hierarchy)
      - [Repeated Capability Reference Property Naming](#repeated-capability-reference-property-naming)
    - [IVI-Python Error Handling](#ivi-python-error-handling)
    - [Documentation and Source Code](#documentation-and-source-code)
  - [Base IVI-Python API](#base-ivi-python-api)
    - [Required Driver API Mapping Table](#required-driver-api-mapping-table)
    - [Constructors](#constructors)
      - [Python Constructor Prototype](#python-constructor-prototype)
    - [IVI-Python Utility Interface](#ivi-python-utility-interface)
    - [Direct IO Properties and Methods](#direct-io-properties-and-methods)
  - [Package Requirements](#package-requirements)
    - [Package Meta-data](#package-meta-data)
    - [Contents](#contents)
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

The IVI Python standard does not require certain operating systems and releases. The drivers shall be provided as 64-bit version, the 32-bit drivers are optional. The compliance document for an IVI driver states whether the driver is available in a 64-bit version, a 32-bit version, or both.

### Target Python Versions

IVI-Python drivers shall target Python 3.8 or later.

### IVI-Python Naming

IVI-Python drivers shall follow the PEP-8 Python naming guidelines.

### IVI-Python Packages

The IVI-Python driver shall be organized as a package, including an `__init__.py` file. Exactly one driver per distribution package shall be present. 

#### IVI-Python Package Versioning
The package should use [semantic versioning](https://semver.org/) (semver).

#### IVI-Python Distribution Packages Naming
The name of the package for the driver shall follow the [Python naming guideline](https://packaging.python.org/en/latest/specifications/name-normalization/):
The name should be lowercased with all runs of the characters ., -, or _ replaced with a single - character. This can be implemented in Python with the re module:

```Python
import re

def normalize(name):
    return re.sub(r"[-_.]+", "-", name).lower()
```

Name composition:
- `<vendorPrefix>-<driverIdentifier>` Example: `myvendor-specan`

### IVI-Python Driver Classes

IVI-Python drivers are object-oriented. There shall be a class that represents the entire driver. That class is instantiated for each distinct instrument that will be controlled. The name of the class shall be `<DriverIdentifier>`.

### IVI-Python Hierarchy

Modules within the driver may be named at the driver vendors discretion. An IVI-Python driver shall organize the driver's API as a hierarchy of classes. Each of the interfaces is implemented by one of the driver's classes.

One of the classes provided by the driver shall be the IVI-specified driver utility class defined in [IVI-Python Utility Interface](#ivi-python-utility-interface)

The root of the hierarchy shall be the main class `<DriverIdentifier>`.

The main class shall include properties that return references to child classes. A child class may in turn include properties that return references to its child classes, and so on. These *reference properties* may then be used to navigate to any instrument functionality from the main class. The hierarchy may be arbitrarily deep.

The names of the reference properties should be the snake-case form of the class name it references.

Consider the following example code:

```python
kt1234.cls2.cls3.measure()
```

`kt1234` is a reference to an instance of the main class. `kt1234` contains an interface reference property named `cls2`. `cls2` contains a reference property named `cls3`, which returns a reference to a class `Cls3`. `Cls3` contains the method `measure()`.

> **Observation:**
> > As the user types each of these names, IntelliSense makes navigating the hierarchy easy. It displays a dropdown list of methods and properties in the corresponding class or interface. After typing `kt1234` followed by a period, a list of all the properties and methods in `kt1234` appears, allowing the user to select one. After selecting `cls2` and typing the period, a list of the methods and properties in `cls2` appears. After selecting `cls3` and typing the period, a list of the methods and properties in `Cls3` appears, and the user can see and select `measure()`.

#### Reference Property and Class Naming

For reference exposed to the user, IVI-Python drivers shall follow the PEP-8 Python naming guidelines.

### Repeated Capabilities

Repeated capabilities may be represented in two ways in IVI-Python drivers. Repeated capability instances may be specified by:

1) a method that selects the active instance (the *selector style*) for subsequent operations
2) selecting a particular instance from a collection (the *collection style*). 

See the *IVI Core Driver Specification* for details.

For IVI-Python drivers, collection style repeated capabilities are recommended.

#### Collection Style Repeated Capabilities and the Hierarchy

Collection style repeated capabilities consist of at least two classes. The first is the collection itself, and the second is the object returned by the subscript operator (`[]`) of the collection. In the hierarchy, a reference property returns the collection object. Then the collection's subscript operator `[]` is used to return an item from the collection. Each item in the collection represents one instance of the repeated capability.

Collection style repeated capabilities may be indexed by a string, integer, or other Python object.

Consider the following example code:

```Python
my_peak = kt1234.trace["B"].peak
```

`kt1234` is a reference to the main class. `kt1234` contains an interface reference property named `trace`, which returns a reference to the trace collection. The subscript operator (`["B"]`) selects the item named "B" from the collection and returns a reference to an object that uniquely represents the "B" trace. That interface or class contains the property `peak`.

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

For example, consider a trigger repeated capability. The `<RcName>` = `Trigger`, and the collection class is `TriggerCollection`:

```python
from typing import Dict

class Trigger:
    """Trigger functions of the instrument."""

    def __init__(self, name: str, value: int):
        self._name: str = name
        self._value: int = value
        self._level: float = 1.0

    @property
    def name(self) -> str:
        """Logical name representing the command value for the user."""
        return self._name

    @property
    def value(self) -> int:
        """Command value for the instrument."""
        return self._value

    @property
    def level(self) -> float:
        return self._level

class TriggerCollection(dict):
    """Collection of the Trigger items."""

    def __init__(self):
        super().__init__()
        self["TriggerA"] = Trigger("TriggerA", 1)
        self["TriggerB"] = Trigger("TriggerB", 2)
        self["TriggerC"] = Trigger("TriggerC", 3)
```

### Driver Structure Interfaces

Python IVI Drivers use tree-like structure of interfaces, some with repeated capabilities, and some without.
Consider an Oscilloscope driver with a non-repeated capabilities interface `axes` which contains repeated capabilities interface `vertical`:

```python
io = Oscilloscope("TCPIP::192.168.1.101")
```
Interface accessor without the repeated capability shall be implemented as read-only property:

```python
ax = io.axes
```

Interface accessor with the repeated capability shall be implemented as a read-only property returning the whole collection of the items. Indexer data type of the collection, shall be enum and string. If it makes sense, for example if the underlying communication uses SCPI commands, the driver should implement integer indexer. The Interface accessor should be a plural word, to hint to the user that the data type is a collection: 

```python
vertical_items_collection = io.axes.verticals
vertical_item_1a = io.axes.verticals[VerticalIndex.Vertical_1]
vertical_item_1b = io.axes.verticals['Vertical_1']
vertical_item_1c = io.axes.verticals[1]  # Optional integer indexer
```
In addition, to improve the user experience by utilizing the code-completion, the drivers shall implement a method-like accessors with enum and string parameter data types. 
The method accessor shall have the same name as the property, with the suffix `_item`:

```python
vertical_item_2a = io.axes.vertical_item(VerticalIndex.Vertical_2)
vertical_item_2b = io.axes.vertical_item('Vertical_2')
vertical_item_2c = io.axes.vertical_item(2)  # Optional integer indexer
```

### IVI-Python Error Handling

All IVI-Python instrument drivers shall consistently use the standard Python exception mechanism to report errors. Neither return values nor *out* parameters shall be used to return error information.

> **Observation:**
> > The method `query_instrument_error()` is used to handle errors within the instrument that may not be thrown as Python exceptions.

### Documentation and Source Code

This specification does not have specific requirements on the format or distribution method of documentation and source code other than those called out in *IVI Driver Core Specification*.

> **Observation:**
> > Driver developers are encouraged to include documentation and source code in the driver package. At a minimum the package should include a README file that directs customers where they can find additional material.

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

### Constructors

In IVI-Python, constructors provide the initialization functionality described in *IVI Driver Core Specification*. This section specifies the required IVI-Python specific driver constructors.

#### Python Constructor Prototype

The IVI-Python drivers shall implement constructor with the following prototype:

  `<DriverIdentifier>(resource_name: str, id_query: bool, reset: bool, options: dict or str or None = None)` 

Example for DriverIdentifier `MyPowerMeter`:

```Python
class MyPowerMeter:
 
    def __init__(self, resource_name: str, id_query: bool = True, reset: bool = False, options: dict or str or None = None):
        # Initialization of the Powermeter.
        self.io: Resource = pyvisa.ResourceManager().open_resource(resource_name)
        self.id_query: bool = id_query
        self.reset: bool = reset
        self.options: dict = options

```

Python TypedDict is a recommended data type compared to a standard dictionary. In run-time, it is a standard dict type, but the advantage is code-completion and type hinting in static analysis. Example:

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

IVI-Python drivers shall provide an additional optional parameters for the client to specify driver options (such as simulation or options as string). The mechanism by which these parameters are passed is driver-specific.

```Python
from typing import TypedDict

class Options(TypedDict, total=False):
	simulate: bool
	clear_status_on_init: str
	block_data_chunk: int
```

The parameters are defined in the *IVI Driver Core Specification*. The following table shows their names and types for Python:

| Inputs        | Description   | Data Type |
|---------------|---------------|-----------|
| resource_name | Resource Name | str       |
| id_query      | ID Query      | bool      |
| reset         | Reset         | bool      |

Notes:

- *IVI Driver Python* constructors are implemented on the class name `<DriverIdentifier>`.

### IVI-Python Utility Interface

IVI-Python drivers shall implement the class defined in this section. The driver shall provide an interface reference property to acquire the drivers instance of the class. 

The interface reference property shall be named *ivi_utility*. The interface reference property shall be available on the root driver class. The driver developer is responsible for defining an instantiable class that inherits from `IviUtility`.

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
    """Returns the last error in the instrument's error queue.
    Returns None if no error is present."""
    pass

  @abstractmethod
  def error_query_all(self) -> List[ErrorQueryResult]:
    """Returns all the errors currently reported in the instrument's error queue.
    If no error is present, the method returns an empty list."""
    pass

  @abstractmethod
  def check_status(self) -> None:
    """Combines error_query_all and Exception rising when some errors are detected."""
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
  def __init__(self, code: int, message: str):
    self._code = code
    self._message = message

  @property
  def code(self) -> int:
    return self._code

  @property
  def message(self) -> str:
    return self._message
```

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

The following sections detail the package requirements.

### Package Meta-data

Project meta-information. 

At least one instrument model supported by the driver shall be mentioned in the keywords list.

Below, is an example of the toml file content: 

```toml
[project]
name = "vendorxy-specan"
version = "1.0"
requires-python = ">= 3.8"
authors = [ {name = "VendorXy"} ]
description = "This is a short description for the vendorxy-specan"
readme = {file = "README.md", content-type = "text/markdown"}
license = "MIT"
classifiers = [ "Programming Language :: Python", "Intended Audience :: Telecommunications Industry" ]
dependencies = ["pyvisa"]
keywords = ["vendorxy", "SpecanModel_ABC"]

[project.urls]
Documentation = "https://readthedocs.org"
```

### Contents

All IVI-Python driver packages shall include the following files:

- The driver

- Readme file (`README.md`). This file shall contain the same list of supported instrument models as in the package meta-data.


## IVI-Python Driver Conformance

IVI-Python Drivers are required to conform to all the rules in this document. They are also required to be registered on the IVI website.

Drivers that satisfy these requirements are IVI-Python drivers and may be referred to as such.

Registered conformant drivers are permitted to use the IVI Conformant Logo.

### Driver Registration

Driver providers wishing to obtain and use the IVI Conformance logo shall submit to the IVI Foundation the driver compliance document described in *IVI Driver Core Specification*, Section [Driver Conformance](../../../IviDriverCore/1.0/Spec/IviDriverCore.md#driver-conformance) along with driver information and a point of contact for the driver. The information shall be submitted to the [IVI Foundation website](https://ivifoundation.org). Complete upload instructions are available on the site. Driver vendors who submit compliance documents may use the IVI Conformant logo graphics.

The IVI Foundation may make some driver information available to the public for the purpose of promoting IVI drivers. All information is maintained in accordance with the IVI Privacy Policy, which is available on the IVI Foundation website.