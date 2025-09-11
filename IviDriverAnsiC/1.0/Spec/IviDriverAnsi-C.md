# IVI Driver ANSI C Specification

| Version Number | Date of Version    | Version Notes                   |
|--------------- | ---------------    | -------------                   |
| 0.1            |  May 2025  | Preliminary Draft for LXI Development   |
| 0.2            | July 2025  | First version in IVI repo, with updates per meeting |

## Abstract

This specification contains the ANSI C specific requirements for an IVI-ANSI-C driver, it is an IVI Language-Specific specification. Drivers that comply with this specification are also required to comply with the *IVI Driver Core Specification*.

## Authorship

This specification is developed by member companies of the IVI Foundation. Feedback is encouraged. To view the list of member vendors or provide feedback, please visit the IVI Foundation website at [www.ivifoundation.org](https://www.ivifoundation.org).

## Warranty

The IVI Foundation and its member companies make no warranty of any kind with regard to this material, including, but not limited to, the implied warranties of merchantability and fitness for a particular purpose. The IVI Foundation and its member companies shall not be liable for errors contained herein or for incidental or consequential damages in connection with the furnishing, performance, or use of this material.

## Trademarks

Product and company names listed are trademarks or trade names of their respective companies.

No investigation has been made of common-law trademark rights in any work.

## Table of Contents

- [IVI Driver ANSI C Specification](#ivi-driver-ansi-c-specification)
  - [Abstract](#abstract)
  - [Authorship](#authorship)
  - [Warranty](#warranty)
  - [Trademarks](#trademarks)
  - [Table of Contents](#table-of-contents)
  - [Overview of the IVI-ANSI-C Driver Language Specification](#overview-of-the-ivi-ansi-c-driver-language-specification)
    - [Relationship to the IVI Driver Core Specification](#relationship-to-the-ivi-driver-core-specification)
    - [Relationship of IVI-ANSI-C to the IVI-C Specifications](#relationship-of-ivi-ansi-c-to-the-ivi-c-specifications)
    - [Substitutions](#substitutions)
  - [IVI-ANSI-C Driver Architecture](#ivi-ansi-c-driver-architecture)
    - [Operating Systems and Bitness](#operating-systems-and-bitness)
    - [Target ANSI-C Versions](#target-ansi-c-versions)
    - [IVI-ANSI-C Naming](#ivi-ansi-c-naming)
    - [IVI-ANSI-C Filenames](#ivi-ansi-c-filenames)
    - [IVI-ANSI-C Data Types](#ivi-ansi-c-data-types)
      - [IVI-ANSI-C String Encoding](#ivi-ansi-c-string-encoding)
    - [IVI-ANSI-C Header Files](#ivi-ansi-c-header-files)
      - [Multiple Inclusion](#multiple-inclusion)
    - [IVI-ANSI-C Function Style](#ivi-ansi-c-function-style)
      - [IVI-ANSI-C Function Naming](#ivi-ansi-c-function-naming)
      - [The Session Parameter](#the-session-parameter)
      - [IVI-ANSI-C Status and Error Handling](#ivi-ansi-c-status-and-error-handling)
      - [Properties](#properties)
      - [Enumerated Types and Enumeration Constants](#enumerated-types-and-enumeration-constants)
    - [Repeated Capabilities](#repeated-capabilities)
    - [Documentation and Source Code](#documentation-and-source-code)
  - [Thread Safety](#thread-safety)
  - [Base IVI-ANSI-C API](#base-ivi-ansi-c-api)
    - [Required Driver API Mapping Table](#required-driver-api-mapping-table)
    - [Additional Required Functions for IVI-ANSI-C Drivers](#additional-required-functions-for-ivi-ansi-c-drivers)
      - [Error Message Function](#error-message-function)
      - [Error Query All](#error-query-all)
    - [ANSI-C Initialize (Init) Function Prototypes](#ansi-c-initialize-init-function-prototypes)
    - [IVI-ANSI-C Interface](#ivi-ansi-c-interface)
    - [Direct IO functions](#direct-io-functions)
      - [Direct IO ANSI-C Prototypes](#direct-io-ansi-c-prototypes)
  - [Packaging Requirements for ANSI-C](#packaging-requirements-for-ansi-c)
    - [Signing](#signing)
  - [IVI-ANSI-C Driver Conformance](#ivi-ansi-c-driver-conformance)
    - [Driver Registration](#driver-registration)

## Overview of the IVI-ANSI-C Driver Language Specification

This specification contains the ANSI-C specific requirements for an IVI-ANSI-C driver, it is an IVI Language-Specific specification. Drivers that comply with this specification are also required to comply with the *IVI Driver Core Specification*.

This specification has several recommendations (identified by the use of the work *should* instead of *shall* in the requirement).  These are included to provide a more consistent customer experience.  However, in general, design decisions are left to the driver designer.

### Relationship to the IVI Driver Core Specification

This specification contains ANSI-C specific requirements for drivers that provide a library for use with ANSI-C compilers or other clients that can use a compiled library to interface to an instrument.

This specification also requires that drivers comply with the [IVI Driver Core Specification](https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/IviDriverCore.md).  That specification is language independent and has several requirements that conformant drivers are required to satisfy independent of the driver language such as documentation, testing, and source code availability.

### Relationship of IVI-ANSI-C to the IVI-C Specifications

This specification, and other IVI Driver Core specifications have less extensive requirements to facilitate instrument interchangeability than IVI-C. For instance, IVI-ANSI-C drivers do not require the IVI Configuration Store.

However, there is no limitation to driver users utilizing both IVI-C and IVI-ANSI-C drivers in their system.  Nor is there any inherent limitation to using an IVI-C driver in an ANSI-C setting.

Drivers that conform to this specification do not automatically conform with IVI-C nor do IVI-C drivers automatically conform to this specification.
### Substitutions

This specification uses paired angle brackets to indicate that the text between the brackets is not the actual text to use, but instead indicates the text that is used in place of the bracketed text. The *IVI Driver Core Specification* describes these substitutions.

The *IVI Driver Core Specification* uses the '<DriverIdentifier>' to indicate the name that uniquely identifies the driver.  For IVI-ANSI-C drivers, the '<DriverIdentifier>' shall be constructed as:

- The first 2 characters shall be the vendor abbreviation assigned to the vendor in the IVI Specification [VPP-9](https://www.ivifoundation.org/downloads/VPP/vpp9_4.35_2024-08-08.docx).  Note than any vendor will be assigned an available 2-character abbreviation of their choice at no charge by the IVI Foundation.  This abbreviation shall always be in upper case.
- Additional characters are added that identify the instrument models supported, and any other driver identifying information the vendor chooses. If a vendor expects multiple versions of a driver to be used in a single application, the vendor must differentiate the identifiers by incorporating the driver version into the vendor-provided string.  These additional characters shall not include underscores ('_') due to the use of underscore to separate the `<DriverIdentifier`>.

Vendors should try to keep the '<DriverIdentifier>' short because it appears in any driver symbols that are put into a global namespace.

This document uses the following conventions regarding the '<DriverIdentifier>':

- '<DRIVER_IDENTIFIER>' refers to the driver identifier in upper case with underscores between words and separated from succeeding tokens with an underscore. The vendor abbreviation is NOT separated from the instrument model token with an underscore. An underscore is used to separate the driver identifier from the rest of the symbol. For instance, the token 'Foo' defined by vendor 'XY' for model family 'SigGen42' becomes: `XYSIGGEN42_FOO`.
- '<driver_identifier>' refers to the driver identifier in snake case, separated from succeeding tokens with an underscore. That is, in lower case with underscores between words.  The vendor abbreviation is NOT separated from the instrument model token name with an underscore.  An underscore is used to separate the driver identifier from the rest of the symbol. For instance, the token 'Foo' defined by vendor 'XY' for model family 'SigGen42' becomes: `xysiggen42_foo`.
- '<DriverIdentifier'> is used when the context does not require further clarification, or when pascal case is used. The vendor abbreviation is all upper case as is the first character of the model token. The driver identifier is separated from the rest of the symbol by putting the first character of the rest of the symbol in upper case. For instance, the token 'Foo' defined by vendor 'XY' for model family 'SigGen42' becomes: `XYSigGen42Foo`.

## IVI-ANSI-C Driver Architecture

This section describes how IVI-ANSI-C instrument drivers use ANSI-C. This section does not attempt to describe the technical features of ANSI-C, except where necessary to explain a particular IVI-ANSI-C feature. This section assumes that the reader is familiar with ANSI-C.

### Operating Systems and Bitness

IVI-ANSI-C drivers shall support a compiler on a version of Microsoft Windows that was current when the driver was released or last updated.  Driver vendors are encouraged to also support IVI-ANSI-C drivers on other operating systems and compilers important to their users.

In addition to the compliance documentation required by [IVI Driver Core](#link) IVI-ANSI-C drivers shall also document the compilers and compiler versions with which the driver has been tested.

### Target ANSI-C Versions

IVI-ANSI-C driver binaries (libraries) shall support clients using ISO/IEC 9899:1999 compilers (henceforth referred to as C99).  Drivers are permitted to provide binaries with APIs that support newer ANSI C features, however they shall provide all instrument capabilities from C99 callable APIs.

When IVI-ANSI-C driver source code is provided, it shall be compilable by C99 compilers, however drivers are encouraged to support newer versions of the ANSI-C standards as well.

### IVI-ANSI-C Naming

To avoid naming collisions, symbols that the driver puts into the global name space shall be guaranteed unique by prefixing the symbol with an appropriately cased version of the `<DriverIdentifier>`.

In the following table, examples are all for a device with `<DriverIdentifier>` of `AC123Dev`, which would represent an instrument vendor identified by `AC` and a model or family identified by `123Dev`.  [Substitutions](#substitutions) has usage information on the driver identifier.

The following casing rules shall be followed:

| Language Element | Example | Rule |
| ---------------- | ------- | -------------- |
| function names   | AC1234Dev_my_function | Function names shall be in snake case preceded by the `<driver_identifier>`.  Snake case is, words in lower case separated by underscores. |
| enumeration constants | AC123DEV_COLORS_TEAL |  Enumeration constants shall be upper-case with underscore separators ('_') preceded by the `<DRIVER_IDENTIFIER>`. The enumeration constant name should be composed of the `<DRIVER_IDENTIFIER>` followed by a term identifying the enumeration type, and finally the constant name.  That is: `<DRIVER_IDENTIFIER>_<ENUM TYPE>_<CONSTANT>`. |
| `const` and macros (`#define`) | AC123DEV_MAX_POWER | `const` and macros shall be upper-case with underscore separators ('_') preceded by `<DRIVER_IDENTIFIER>`|
| types (`typedef`, `struct`, `enum`) | AC123DevMySpecialType | Types shall be in Pascal case (also known as upper camel-case) preceded by the `<DriverIdentifier>`.  That is, words begin with upper case letter.  Conventional exceptions for acronyms. |
| formal parameter names | start_frequency | Formal parameter names should be snake case. The driver identifier should not be used |

> **Observation:**
> > The IVI Foundation, grants available 2-character vendor identifiers to any driver vendor requesting them at no cost.  Assigned identifiers can be found in [IVI Foundation vendor registration](https://www.ivifoundation.org/downloads/VPP/vpp9_4.35_2024-08-08.pdf).

> > **Observation:**
> > Since each vendor is assigned a unique 2-character prefix, this scheme eliminates conflicts between vendors.  Each vendor then manages the other characters in the `<DriverIdentifier>` to eliminate collisions.

### IVI-ANSI-C Filenames

Driver file names shall be snake case.

Source code files shall use *.c* and *.h* suffixes for C source and C header files respectively.

Binary files should use filename suffixes conventional for the operating system and compilers they are targeted to.

In general IVI-ANSI-C drivers are composed of numerous files.  The name of each file that is specific to the driver shall be prefixed by the '<driver_identifier>' in snake case.

If needed, vendors are permitted to include files with the driver that are common to multiple drivers provided by the vendor.  These files do not require '<driver_identifier>' but shall be prefixed with the vendor 2-character VPP-9 prefix followed by other identifiers assigned by the driver vendor.

> **Observation:**
> > The provision to not require '<driver_identifier>' in the filename permits vendors to have common include files that define types that are common to several drivers.  For instance, a struct for waveforms.

### IVI-ANSI-C Data Types

Drivers should prefer the fundamental data types intrinsic to ANSI-C and, the types defined in ANSI-C include files.  This includes, but is not limited to: '<stdint.h>', '<float.h>' and '<string.h>'.

Drivers are encouraged to define other driver-specific types when ANSI types are not available.  Vendors may find it beneficial to define types that are common to multiple drivers.  However, drivers should avoid creating new types that are synonymous with those provided by ANSI.

Drivers should consider defining a driver-defined type for the driver session, thereby providing type-checking for that parameter.

Drivers shall provide all include files necessary to use the driver in the driver package.

Drivers that provide source code shall provide all include files necessary to compile the driver with the driver source code.

#### IVI-ANSI-C String Encoding

IVI ANSI-C Drivers public APIs shall use UTF-8 string encoding.  

> **Observation:**
>> The string encoding used to communicate with the instrument is instrument specific. For performance reasons drivers are not required to validate the encoding of strings exchanged with the instrument.

### IVI-ANSI-C Header Files

Drivers shall provide include files for driver clients that contain:

- include directives for any ANSI-C include files required by the driver
- prototypes for all functions provided by the driver
- definitions for all types not provided by ANSI-C that are needed by the driver client
- definitions of enumerated types and enumeration constants
- definitions of any macros used by the driver

#### Multiple Inclusion

All driver include files shall be protected against multiple inclusions.  For instance, by defining a symbol when the file is first loaded, and subsequently bypassing included content on subsequent loads.

Example: A driver xysiggen42_sg_types.h, would define a symbol `XYSIGGEN42_SG_TYPES_H` when first loaded, then enclose the body of the .h file in appropriate `#ifdef` directives.

### IVI-ANSI-C Function Style

The following sub-sections call out required IVI-ANSI-C style.  There are additional rules and recommendations in [repeated capabilities](#repeated-capabilities).

#### IVI-ANSI-C Function Naming

IVI-ANSI-C function names shall be snake case.

In general drivers are encouraged to organize ANSI-C function names into a hierarchy.  This is beneficial because:

- it provides helpful organization for customers to navigate complex APIs
- It permits vendors targeting drivers to both ANSI-C and object oriented languages
- It allows the documentation to be leveraged between ANSI-C and object oriented languages

Vendors should consider emulating the hierarchical API by appending the names of the elements of the hierarchy in generating the C identifier.  For instance:

```C++
  Dmm32.Measurement.Voltage.Span(start=1, stop=10);
```

Would translated into:

```C
  int32_t status = XYDmm32_measurement_voltage_span(Dmm32Session, start, stop);
```

> **Observation:**
> > As the user types the C Identifier in a smart editor, the way that choices are presented to the user by the editor emulates a hierarchy.

#### The Session Parameter

As shown in the [Base API](#base-ivi-ansi-c-api) drivers shall implement an *open* function that has an *out* parameter named *session*.  

Drivers shall provide a *typedef* that specifies the type of the *session*. The type defined by the *typedef* shall be named `<DriverIdentifier>Session`.

Drivers shall also provide a *const* that specifies a value that can be used as a sentinel to indicate an invalid session.  The *const* shall be named `<DRIVER_IDENTIFIER>_INVALID_SESSION`.

All driver functions that reference a specific instance of the driver shall take this *session* as the first parameter.

> **Observations:**
> > Driver designers frequently choose an integer for the session parameter which is used as an index to access the driver data.  Driver designers also frequently choose to use a pointer type for the session parameter, which directly points to the driver data.  These and other approaches are permitted by these rules.  Regardless, it is wise for the driver to take some steps to validate the session.

#### IVI-ANSI-C Status and Error Handling

All IVI-ANSI-C functions that may result in an error shall indicate errors to the client using the function return value.  The return value shall be an *int32_t*.  Zero shall be used to indicate success.

Negative return values shall indicate an error, positive return values shall indicate non-fatal warnings.

> **Observation:**
> > The driver function `<driver_identifier>_instrument_error_get()` is used to handle errors detected within the instrument that may not be thrown as ANSI-C exceptions.

#### Properties

Properties are values that can be individually either get, set, or both.  

Functions that get or set the value of the property shall have the word `get` or `set` as the last token of the function name.

Driver designers should give properties a name that makes sense in the API context.  For instance the term *span* may be used for unrelated properties such as a frequency span or a hysteresis band.  However, they are disambiguated based on the driver hierarchy.

The get/set functions shall:

- return a success code
- the first parameter shall be the driver session
- the next parameter(s) shall be repeated capability selector(s) if needed
- the final parameter shall be a pointer to the value for get and the value to be set for set

> > **Observation**
> Placing get/set at the *end* of the function name ensures that property accessors alphabetize appropriately into the function namespace.

#### Enumerated Types and Enumeration Constants

IVI-ANSI-C drivers may define enumerated types, which are integral types where the allowed values are specified by a set of named enumeration constants.

Enumerated type names shall be of the form `<DriverIdentifier><EnumeratedTypeName>`, in Pascal case.

Enumeration constant names shall be of the form `<DRIVER_IDENTIFIER>_<ENUMERATED_TYPE_NAME>_<ENUMERATION_CONSTANT_NAME>`, in upper case with underscores between words.

> **Observation**
> > Enumeration constants are not scoped by the enumerated type to which they belong.  Prefixing enumeration constants with the enumerated type name prevents name conflicts between types.

The implementation of enumerated types is vendor-defined. Recommended implementations:

- A `typedef` for an `enum` type with corresponding enumeration constants.

  ```C
  typedef enum {
      XYDMM32_FUNCTION_DC_VOLTS = 1,
      XYDMM32_FUNCTION_AC_VOLTS = 2
  } XYDmm32Function;
  ```

- A `typedef` for an integral type, with `#define`d enumeration constants.

  ```C
  typedef uint32_t XYDmm32Function;
  #define XYDMM32_FUNCTION_DC_VOLTS (1)
  #define XYDMM32_FUNCTION_AC_VOLTS (2)
  ```

> **Observation:**
> > In C, the `enum` keyword must be specified when referring to a named `enum` type (e.g. `enum XYDmm32Function`).  In C++, the `enum` keyword is optional and the `enum` type may be used directly (e.g. `XYDmm32Function`). Defining a `typedef` for an unnamed `enum` type allows referring to the type without specifying the `enum` keyword.

> **Observation:**
> > In C99, the underlying integral type of an `enum` is implementation-defined.  Adding enumeration constants to an existing `enum` may change the width and/or signedness of its underlying integral type, breaking binary and/or source compatibility.  Popular C compilers for Windows and desktop Linux use 32-bit integers when possible, but embedded platforms or compiler options such as GCC's `-fshort-enums` may behave differently.
> >
> > C23 and C++11 extend the `enum` syntax to allow specifying the underlying integral type.  This is not legal C99 syntax, so drivers may only use this syntax if it is guarded with appropriate C preprocessor conditionals.
> >
> > Example:
> >
> > ```C
> > #if (defined(__STDC_VERSION__) && __STDC_VERSION__ >= 202311L) || (defined(__cplusplus) && __cplusplus >= 201103L)
> > #define XYDMM32_ENUM_TYPE : uint32_t
> > #else
> > #define XYDMM32_ENUM_TYPE
> > #endif
> > 
> > typedef enum XYDMM32_ENUM_TYPE {
> >     XYDMM32_FUNCTION_DC_VOLTS = 1,
> >     XYDMM32_FUNCTION_AC_VOLTS = 2
> > } XYDmm32Function;
> > ```

> **Observation:**
> > In C99, `enum` types are not type-safe: an `enum` is compatible with its underlying integral type.  As a result, `enum` types with the same underlying integral type may be used interchangeably.  For example, if `XYDmm32Function` and `XYDmm32TempTransducerType` have the same underlying integral type, then you can pass `XYDMM32_FUNCTION_DC_VOLTS` to the `XYDmm32_temp_transducer_type_set` function without getting a compiler error or warning.  In C++, `enum` types are type-safe, so compiling the same code with a C++ compiler would produce an error or warning.

If the sign of the enumerated type has no significance for the driver, drivers should prefer unsigned types.

> **Observation:**
> > These enumeration rules permit bit-mapped "flag" enumerations.

### Repeated Capabilities

Repeated capabilities may be represented in two ways in IVI-ANSI-C drivers. Repeated capability instances may be specified by a method that selects the active instance (the *selector style*) or by selecting a particular instance using a function parameter. See the *IVI Core Driver Specification* for more information on this.

Driver functions that require a repeated capability parameter(s) should pass it as the second (and or following) parameter(s), after the *session*.  

If a driver implements multiple repeated capabilities (for instance N markers on M waveforms), the driver API may either define a scheme whereby multiple repeated capabilities can be passed in a single parameter, or it may use additional parameters.

For instance, if a driver chooses to use method parameters to identify N markers on M waveforms, it could:

- accept 2 parameters one for the marker and another for the waveform
- it could bitmap the parameters into an integer, so the 3 third marker on the second waveform may be identified as '0x23' (providing 4 bits for both parameters)
- it could accept a repeated capability structure defined by the driver
- it could treat the repeated capability identifiers as strings and pass a string such as "2:3"

Drivers are permitted to choose any of these, or use other approaches.

> **Observation:**
> > There are additional details on possible repeated capability implementation strategies in the [IVI SCPI standard](#link), the [IVI Core Repeated Capability Document](#link), and the [IVI Generation 2014](#link) driver specifications.

> **Observation:**
> > Some applications requiring repeated capability parameters that operate on multiple instances of a repeated capability at the same time. For these, bitmapping each repeated capability into an integer may work well.

> **Observation:**
> > A useful solution to the challenge of nested repeated capabilities is to treat them as a string with each element syntactically separated as in [IVI-C](#link) .  The string is then parsed by the driver.

> **Observation:**
> > If using a structure, driver designers need to be aware that it may be awkward for the driver client to construct and pass the structure.  Furthermore, stable ABIs are often difficult to provide with structure. The selection of pass-by-value and pass-by-reference needs to be made cautiously.

### Documentation and Source Code

This specification does not have specific requirements on the format or distribution method of documentation and source code other than those called out in *IVI Driver Core Specification*.

## Thread Safety

IVI-ANSI-C drivers shall be thread-safe.  That is, driver functions shall tolerate being called from foreign threads while the driver is currently executing on another thread.

## Base IVI-ANSI-C API

This section gives a complete description of each function required for an IVI-ANSI-C Core driver. The following table shows the mapping between the required base driver APIs described in the [IVI Driver Core specification](#link) and the corresponding IVI-ANSI-C specific APIs described in this section.

### Required Driver API Mapping Table

| Required Driver API (IVI Driver Core)|Core IVI-ANSI-C API                          |
|---------------------------------|---------------------------------                 |
| Initialization                  | <driver_identifier>_init()                       |
| Driver Version                  | <driver_identifier>_driver_version_get()         |
| Driver Vendor                   | <driver_identifier>_driver_vendor_get()          |
| Error Query                     | <driver_identifier>_error_query()                |
| Instrument Manufacturer         | <driver_identifier>_instrument_manufacturer_get()|
| Instrument Model                | <driver_identifier>_instrument_model_get()       |
| Query Instrument Status Enabled (Get) | <driver_identifier>_query_instrument_status_enabled_get()|
| Query Instrument Status Enabled (Set) | <driver_identifier>_query_instrument_status_enabled_set()|
| Reset                           | <driver_identifier>_reset()                      |
| Simulate Enabled                | <driver_identifier>_simulate_get                 |
| Supported Instrument Models     | <driver_identifier>_supported_instrument_models_get() |

### Additional Required Functions for IVI-ANSI-C Drivers

| Required Driver API (IVI Driver Core)|Core IVI-ANSI-C API                          |
|---------------------------------|---------------------------------                 |
| Error Message                  |char* <driver_identifier>_error_message()  |
| Error Query All                | <driver_identifier>_error_query_all() |

#### Error Message Function

This function converts an error returned by an ANSI-C driver function call into a human readable string.  Note that this function does not accept a session parameter. If the passed *error* code is not defined by the driver, the driver should return an appropriate error message.

Prototype:

```C
char* <driver_identifier>_error_message(int32_t error);
```

#### Error Query All

This function repetitively queries the device for errors, ensuring that any device hosted error queue is empty.  If it discovers more than one error it shall concatenate the error messages together, separating them with a new-line and return the resulting string.  Any strings returned by the device that have an embedded new-line will require special handling by the driver.

This function uses client-allocated memory to return the result.  The usual IVI-ANSI-C client-allocated memory protocol is used to return the value.

>**Observation:**
> > Implementing this function may require that the driver read the complete error queue from the device, keeping it in a cache, in order to determine its size before engaging in the client-allocated memory protocol with the client.

Prototype:

> [!NOTE] Need to fill in the prototype after we agree to the client-allocated memory protocol.

```C
char* <driver_identifier>_error_query_all()
```

### ANSI-C Initialize (Init) Function Prototypes

The IVI-ANSI-C drivers shall implement an initializer with the following prototype:

```C
int32_t <driver_identifier>_init(const char *resource_name, bool id_query,  bool reset, <SESSION_TYPE>* session_out);
int32_t <driver_identifier>_init_with_options(const char *resource_name, bool id_query, bool reset, const char* options, <SESSION_TYPE>* session_out);
```

For the <driver_identifier>_init() function, *simulation* is initially disabled.  For <driver_identifier>_init_with_options() the *simulation* is initially disabled unless specified otherwise in the *options* string.

The format of the *options* string shall be: "<name1>=<value>;<name2>=<value>;...".

IVI-ANSI-C drivers may implement additional initializers.

The parameters are defined in the [IVI Driver Core Specification](#link).  The following table shows their names and types for ANSI-C:

| Inputs        |     Description     |    Data Type  |
| ------------- | ------------------- | ------------  |
| resource_name |   Resource Name     |  const char * |
| id_query      |   ID Query          |  bool         |
| reset         |   Reset             |  bool         |

### IVI-ANSI-C Interface

```C
int32_t <driver_identifier>_init(const char *resource_name, bool id_query,  bool reset, <SESSION_TYPE>* session_out);
int32_t <driver_identifier>_init_with_options(const char *resource_name, bool id_query, bool reset, const char* options, <SESSION_TYPE>* session_out);


  AND THE REST

```

ANSI-C-specific Notes (see [IVI Driver Core Specification](#link) for general requirements):

- Drivers are permitted to implement a Set function on `Simulate`. However, if they do so, they shall properly manage the driver state when turning simulation on and off.

### Direct IO functions

Per the *IVI Driver Core specification*, IVI Drivers for instruments that have an ASCII command set such as SCPI shall also provide an API for sending messages to and from the instrument over the ASCII command channel. This section specifies those functions.

Drivers may implement these functions in the driver hierarchy if it makes sense.

In the following '<hierarchy>' indicates whatever hierarchy path the driver designer chooses for the direct IO functions.

| Required Driver API (IVI Driver Core) | Core IVI-ANSI-C API     |
| ------------------------------------- | --------------------    |
| I/O Timeout Set                       | `<driver_identifier>_<hierarchy>_timeout_milliseconds_set()` |
| I/O Timeout Get                       | `<driver_identifier>_<hierarchy>_timeout_milliseconds_get()` |
| Read Bytes                            | `<driver_identifier>_<hierarchy>_read_bytes()`   |
| Read String                           | `<driver_identifier>_<hierarchy>_read_string()`  |
| Write Bytes                           | `driver_identifier>_<hierarchy>_write_bytes()`   |
| Write String                          | `<driver_identifier>_<hierarchy>_write_string()` |

#### Direct IO ANSI-C Prototypes

> [!NOTE]
> Need to update read_bytes, read_string when we finalize the client-allocated memory scheme.

```C
error <driver_identifier>_<hierarchy>_timeout_milliseconds_set(const void* session, const long);
error <driver_identifier>_<hierarchy>_timeout_milliseconds_get(const void* session, const &long);
error <driver_identifier>_<hierarchy>_iosession_get(const void* session, const void **iosession);    // Optional
error <driver_identifier>_<hierarchy>_read_bytes(const void* session, const byte **, const long maxLength, const long &actualLength);
error <driver_identifier>_<hierarchy>_read_string(const void* session, const char *);
error <driver_identifier>_<hierarchy>_write_bytes(const void* session, const byte *, const long length);
error <driver_identifier>_<hierarchy>_write_string(const void* session, const char *);
```

Notes:

- The *optional* `iosession` read-only property should return a session for the underlying IO library.

>**Observation:**
> > Drivers should consider including a query function that combines read and write.

## Packaging Requirements for ANSI-C

IVI-ANSI-C drivers are provided as packages.  The packaging technology should be appropriate for the driver user for instance: zip, tar, or self-extracting archives.

Driver packages shall include:

- Driver binaries for the supported OS/Compiler
- Driver license terms
- The IVI Compliance document per the IVI Driver Core Specification
- Documentation or a README file that indicates how to acquire documentation
- Documentation or a README file that indicates how to acquire source code (if provided for this driver, per the [Core Driver specification](#link) requirements)

Driver packages may include additional files at the discretion of the provider.

### Signing

All files included in the IVI-ANSI-C package shall be signed by the driver vendor.

## IVI-ANSI-C Driver Conformance

IVI-ANSI-C Drivers are required to conform to all of the rules in this document. They are also required to be registered on the IVI website.

Drivers that satisfy these requirements are IVI-ANSI-C drivers and may be referred to as such.

Registered conformant drivers are permitted to use the IVI Conformant Logo.

### Driver Registration

Driver providers wishing to obtain and use the IVI Conformance logo shall submit to the IVI Foundation the driver compliance document described in *IVI Driver Core Specification*, Section [Driver Conformance](../../../IviDriverCore/1.0/Spec/IviDriverCore.md#driver-conformance) along with driver information and a point of contact for the driver. The information shall be submitted to the [IVI Foundation website](https://ivifoundation.org). Complete upload instructions are available on the site. Driver vendors who submit compliance documents may use the IVI Conformant logo graphics.

The IVI Foundation may make some driver information available to the public for the purpose of promoting IVI drivers. All information is maintained in accordance with the IVI Privacy Policy, which is available on the IVI Foundation website.
