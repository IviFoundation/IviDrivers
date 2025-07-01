# IVI Driver ANSI C Specification

| Version Number | Date of Version    | Version Notes                   |
|--------------- | ---------------    | -------------                   |
| 0.1            |  May 2025  | Preliminary Draft for LXI Development   |
| 0.2            | July 2025  | First version in IVI repo, with updates per meeting |

> [!NOTE:]
>
> - Memory management - need to know sizes of things (e.g., arrays etc).  Do you do the "IVI Dance" to call with a null pointer then get the size?  Can we define a way to do this consistently.  Perhaps return the size instead of the error code?
>
> - Good practice for libraries to have global initialize/finalize function so you can join worker threads you have in the background.  NI sees a need for this a lot.  Perhaps it could be optional but called out (important when loading the drivers in the background - need a way to clean it up before the process unloads).  Seems like, at least for Windows, these should be called in DLL_PROCESS_ATTACH/DETACH??  would we make that a requirement?
>
> - If we think about 2014 and 2026 coexisting.  Would a 2014 driver still comply since it is more specific requirement?  How do the specs relate? Expectation that a 2014 driver would still comply.  Expect them to coexist.
>
> - What provisions do we need to include in the spec for ABI compatibility?  Especially regarding using and permitting use of *int* and *enum*
>
> 2025-07-01
>   - Need to discuss typing.  Do we want to permit stronger typing by:
>      - Defining some common types such as "ResultType" for errors/warnings
>      - We could specify a type for session, or we could  (as written here) either suggest or require driver-defined types.


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
    - [Substitutions](#substitutions)
  - [IVI-ANSI-C Driver Architecture](#ivi-ansi-c-driver-architecture)
    - [Operating Systems and Bitness](#operating-systems-and-bitness)
    - [Target ANSI-C Versions](#target-ansi-c-versions)
    - [IVI-ANSI-C Naming](#ivi-ansi-c-naming)
    - [IVI-ANSI-C Filenames](#ivi-ansi-c-filenames)
    - [IVI-ANSI-C Data Types](#ivi-ansi-c-data-types)
    - [IVI-ANSI-C Header Files](#ivi-ansi-c-header-files)
      - [Multiple Inclusion](#multiple-inclusion)
    - [IVI-ANSI-C Function Style](#ivi-ansi-c-function-style)
      - [IVI-ANSI-C Function Naming](#ivi-ansi-c-function-naming)
      - [The Session Parameter](#the-session-parameter)
      - [IVI-ANSI-C Status and Error Handling](#ivi-ansi-c-status-and-error-handling)
      - [Properties](#properties)
      - [Enumerated Types and Their Members](#enumerated-types-and-their-members)
    - [Repeated Capabilities](#repeated-capabilities)
    - [Documentation and Source Code](#documentation-and-source-code)
  - [Thread Safety](#thread-safety)
  - [Base IVI-ANSI-C API](#base-ivi-ansi-c-api)
    - [Required Driver API Mapping Table](#required-driver-api-mapping-table)
    - [Additional Required Functions for IVI-ANSI-C Drivers](#additional-required-functions-for-ivi-ansi-c-drivers)
    - [ANSI-C Initialize Function Prototypes](#ansi-c-initialize-function-prototypes)
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

### Substitutions

This specification uses paired angle brackets to indicate that the text between the brackets is not the actual text to use, but instead indicates the text that is used in place of the bracketed text. The *IVI Driver Core Specification* describes these substitutions.

The *IVI Driver Core Specification* uses the '<DriverIdentifier>' to indicate the name that uniquely identifies the driver.  For IVI-ANSI-C drivers, the '<DriverIdentifier>' shall be constructed as:

- The first 2 characters shall be the vendor abbreviation assigned to the vendor in the IVI Specification [VPP-9](https://www.ivifoundation.org/downloads/VPP/vpp9_4.35_2024-08-08.docx).  Note than any vendor will be assigned an available 2-character abbreviation of their choice at no charge by the IVI Foundation.
- Additional characters are added that identify the instrument models supported, and any other driver identifying information the vendor chooses. If a vendor expects multiple versions of a driver to be used in a single application, the vendor must differentiate the identifiers by incorporating the driver version into the vendor-provided string.

Vendors should try to keep the '<DriverIdentifier>' short because it appears in any driver symbols that are put into a global namespace.

This document uses the following conventions regarding the '<DriverIdentifier>':

- '<DRIVER_IDENTIFIER>' refers to the driver identifier in upper case with underscores between words and separated from succeeding tokens with an underscore. The vendor abbreviation is NOT separated from the instrument model token with an underscore. For instance, the token 'Foo' defined by vendor 'XY' for model family 'SigGen42' becomes: `XYSIGGEN42_FOO`.
- '<driver_identifier>' refers to the driver identifier in snake case, separated from succeeding tokens with an underscore. That is, in lower case with underscores between words.  The vendor abbreviation is NOT separated from the instrument model token name with an underscore. For instance, the token 'Foo' defined by vendor 'XY' for model family 'SigGen42' becomes: `xysiggen42_foo`.
- '<DriverIdentifier'> is used when the context does not require further clarification, or when pascal case is used. The vendor abbreviation is all upper case as is the first character of the model token.  For instance, the token 'Foo' defined by vendor 'XY' for model family 'SigGen42' becomes: `XYSigGen42Foo`.

## IVI-ANSI-C Driver Architecture

This section describes how IVI-ANSI-C instrument drivers use ANSI-C. This section does not attempt to describe the technical features of ANSI-C, except where necessary to explain a particular IVI-ANSI-C feature. This section assumes that the reader is familiar with ANSI-C.

### Operating Systems and Bitness

> [!NOTE] Need to close on compiler requirement for Windows.

IVI-ANSI-C drivers shall support a popular compiler on a version of Microsoft Windows that was current when the driver was released or last updated.  Driver vendors are encouraged to also support IVI-ANSI-C drivers on other operating systems and compilers important to their users.

> [!NOTE]  2025-06-17 Verify if we need to call out OS or if they are already in requirements.
> 2025-07-01 -- the OS is required but not the compiler.

In addition to the compliance documentation required by [IVI Driver Core](#link) IVI-ANSI-C drivers shall also document the compilers and compiler versions with which the driver has been tested.

### Target ANSI-C Versions

IVI-ANSI-C driver binaries (libraries) shall support clients using ISO/IEC 9899:1999 compilers (henceforth referred to as C99).  Drivers are permitted to provide binaries with APIs that support newer ANSI C features, however they shall provide all instrument capabilities from C99 callable APIs.

When IVI-ANSI-C driver source code is provided, it shall be compilable by C99 compilers, however drivers are encouraged to support newer versions of the ANSI-C standards as well.

### IVI-ANSI-C Naming

To avoid naming collisions, symbols that the driver puts into the global name space shall be guaranteed unique by prefixing the symbol with an appropriately cased version of the `<DriverIdentifier>`.

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

> [!NOTE]  
> This draft requires using void* in lieu of vi_session.  Need to be sure we like that.
>
> We should consider if there are commonly used types we should have a common definition of.  Particularly time (absolute or relative).  Do the ANSI complex type(s) satisfy all of the complex use-cases?
>
> If we had IVI-specified types, we would need to think about the delivery and management of the common IVI/VPP .h files. (do they go with each driver or is there an IVI shared component?)
> 

Drivers should prefer the fundamental data types intrinsic to ANSI-C and, the types defined in ANSI-C include files.  This includes, but is not limited to: '<stdint.h>', '<float.h>' and '<string.h>'.

Drivers are encouraged to define other driver-specific types when ANSI types are not available.  Vendors may find it beneficial to define types that are common to multiple drivers.  However, drivers should avoid creating new types that are synonymous with those provided by ANSI.

> [!NOTE] Added 2025-07-02.  Need to discuss the case of types.

The names of types defined by the driver shall be of the form `<DriverIdentifier><TypeName>` and they shall be in Pascal case.

Drivers should consider defining a driver-defined type for the driver session, thereby providing type-checking for that parameter.

Drivers shall provide all include files necessary to use the driver in the driver package.

Drivers that provide source code shall provide all include files necessary to compile the driver with the driver source code.

### IVI-ANSI-C Header Files

> [!NOTE]  
> Need to decide what we need to say here.
>
> - File naming 
> - How enumerations are declared (since we don't an enum type in C99)
> - types -- what do we need beyond C99, what of visa_types.h ???
> 
> 2025-06-17 --- added section on multiple inclusion. 
>
> 2025-07-01 --- OOPS!!!  Enumerations ARE supported in C99 (better be more careful with AI!).  Prose updated for that (much simpler).  Note that enumeration member names go into the global namespace 

Drivers shall provide include files for driver clients that contain:

- include directives for any ANSI-C include files required by the driver
- prototypes for all functions provided by the driver
- definitions for all types not provided by ANSI-C that are needed by the driver client
- definitions of enumerated types and their members
- definitions of any macros used by the driver

Definitions for enumerated members and other macros shall be prefixed with '<DRIVER_IDENTIFIER>' and be upper case.
  
#### Multiple Inclusion

All driver include files shall be protected against multiple inclusions by defining a symbol that includes the '<DRIVER_IDENTIFIER>' and file name when first loaded, and subsequently  bypasses included content on subsequent loads.

For instance, a driver xysiggen42_sg_types.h, would define a symbol `XYSIGGEN42_SG_TYPES_H` when first loaded, then enclose the body of the .h file in appropriate `#ifdef` directives.

### IVI-ANSI-C Function Style

> [!NOTE]  
> Need to make sure snake case is called out and in the right section.  Expect it near this context.
> 2025-07-01: Added it below.

The following sub-sections call out required IVI-ANSI-C style.  There are additional rules and recommendations in [repeated capabilities](#repeated-capabilities).

#### IVI-ANSI-C Function Naming

> [!NOTE]  Should should probably be titled something more like "function naming" and also include broader discussion especially re. use of things like nouns/verbs in the function name.

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
  int status = XYDmm32_measurement_voltage_span(Dmm32Session, start, stop);
```

> **Observation:**
> > As the user types the C Identifier in a smart editor, the way that choices are presented to the user by the editor emulates a hierarchy.

#### The Session Parameter

> [!NOTE]  Consider making the session typesafe by having the driver specify a type for it.
> 2025-06-24 --- we generally like this suggestion, should incorporate in next draft.
> 2025-07-01 -- DONE, but not as a requirement (?)

As shown in the [Base API](#base-ivi-ansi-c-api) drivers shall implement an *open* function that returns a pointer type named *session*.  Drivers may strongly type this by defining a driver-specific type or use a generic type (_void *_).  In practice, this opaque pointer references an underlying object-like structure that contains instance data for this instance of the driver.

All driver functions that reference a specific instance of the driver shall take this *session* as the first parameter.

#### IVI-ANSI-C Status and Error Handling

> [!NOTE]
> Should we define a required (or optional?) function to convert the return value to a human readable string?  `char[] <driver_identifier>_error_message()`.  Note this is an example of a function that seems to not require the session parameter.
> Discussion 2025-06-17:  Add it, and make it required.
> Update 2025-07-01: Added it.  Needs discussion :)
>
> Disussion 2025-06-24: 
> - VXIp&p uses a 32 bit integer.  So code targeted to both VXIp&p and this spec would have to do some translation.  May want to use a 32-bit integer.
> - Consistent with the comment on session, this could be more strongly typed, but would probably need to be typed beyond the scope of the driver itself.
> - The existing specs call out numeric ranges for the errors. Should we call that out.
> - suggestion that the sign statement below (neg/pos) should be a shall.

All IVI-ANSI-C functions that may result in an error shall indicate errors to the client using the function return value.  The return value shall be an *int*.  Zero shall be used to indicate success.

Negative return values shall indicate an error, positive return values shall indicate non-fatal warnings.

> **Observation:**
> > The driver function `<driver_identifier>_instrument_error_get()` is used to handle errors detected within the instrument that may not be thrown as ANSI-C exceptions.

> **Observation:**
> > By specifying the return type as an *int*, the compiler is directed to use whatever size integer can be most efficiently represented, while guaranteeing at least 16 bits.  Therefore errors and warnings must fit in a 16-bit signed integer.

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

#### Enumerated Types and Their Members

> [!NOTE]  
> Need to decide what we need to say here.
>
> 2025-06-16, drafted a proposal


IVI-ANSI-C drivers shall use the C99 *enum* type for enumerations.

- One of the driver include files shall include the *enum* definition.  As with all IVI-ANSI-C types, the name shall composed as `<DriverIdentifier><EnumTypeName>` in Pascal case.
- The enumeration member names are also placed in the global namespace therefore, they shall be composed as: `<DRIVER_IDENTIFIER>_<ENUM_TYPE_NAME>_<ENUMERATION_MEMBER_NAME>'`.  Note that every character in this string shall be uppercase.

> [!NOTE] I don't care for this recommendation, but I believe we agreed to it?
> 2025-07-01 -- Sorry, I don't recall the following statement --- makes no sense to me -- probably quickly typed in during a meeting (?)

If the sign of the enumerated type has no significance for the driver, drivers should prefer unsigned types.

> **Observation:**
> > These enumeration rules permit bit-mapped enumerations to be created as needed by the API.

### Repeated Capabilities

Repeated capabilities may be represented in two ways in IVI-ANSI-C drivers. Repeated capability instances may be specified by a method that selects the active instance (the *selector style*) or by selecting a particular instance using a function parameter. See the *IVI Core Driver Specification* for more information on this.

> [!NOTE]  following sentence is incompatible w/ the paragraph after it....
>
> Need to have a general discussion about how flexible the spec should be on multiple repeated capabilities.
>
> Note the flexibility that strings bring.
> 

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
> > A useful historical solution to the challenge of nested repeated capabilities is to treat them as a string with each element syntactically separated.  The string is then parsed by the driver.

### Documentation and Source Code

This specification does not have specific requirements on the format or distribution method of documentation and source code other than those called out in *IVI Driver Core Specification*.

## Thread Safety

IVI-ANSI-C drivers shall be thread-safe.  That is, driver functions shall tolerate being called from foreign threads while the driver is currently executing on another thread.

## Base IVI-ANSI-C API

This section gives a complete description of each function required for an IVI-ANSI-C Core driver. The following table shows the mapping between the required base driver APIs described in the [IVI Driver Core specification](#link) and the corresponding IVI-ANSI-C specific APIs described in this section.

### Required Driver API Mapping Table

| Required Driver API (IVI Driver Core)|Core IVI-ANSI-C API                          |
|---------------------------------|---------------------------------                 |
| Initialization                  | <driver_identifier>_open()                       |
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

> [!NOTE:]  Added this per our discussion at June meeting.  Doesn't seem to need the session, do we want to include it in the prototype?
> Also, do we want to permit this to return an error?  Seems like just a message that indicates "Invalid error number" would be suitable.

| Required Driver API (IVI Driver Core)|Core IVI-ANSI-C API                          |
|---------------------------------|---------------------------------                 |
| Error Message                  |char[] <driver_identifier>_error_message(<error_type> error)  |

### ANSI-C Initialize Function Prototypes

The IVI-ANSI-C drivers shall implement two initializers with the following prototypes.

```C
int <driver_identifier>_initialize(const char *resource_name, const _Bool id_query, const _Bool reset, void* &session)
```

If the client uses this function, *simulation* is initially disabled.

> [!NOTE]  Per discussion 2025-06-24
> - do we want to use bool instead of _Bool?  This would require including stdbool.h, but this provides true/false definitions also, so seems reasonable.
> - can we find a common way to pass options in, or do we just go with the ambiguous statement below?   Note that IVI-C really wants a string. VXIp&p has no option string.

IVI-ANSI-C drivers shall implement an additional initializer that provides a way for the client to specify driver options (such as *simulation*). The mechanism by which these parameters are passed is driver-specific.

IVI-ANSI-C drivers may implement additional initializers.

The parameters are defined in the [IVI Driver Core Specification](#link).  The following table shows their names and types for ANSI-C:

| Inputs        |     Description     |    Data Type |
| ------------- | ------------------- | ------------ |
| resource_name |   Resource Name     |  char *      |
| id_query      |   ID Query          |  _Bool       |
| reset         |   Reset             |  _Bool       |

### IVI-ANSI-C Interface

```C

NEED TO FILL THIS IN!!!

```

ANSI-C-specific Notes (see *IVI Driver Core Specification* for general requirements):

- Drivers are permitted to implement a Set function on `Simulate`. However, if they do so, they shall properly manage the driver state when turning simulation on and off.

### Direct IO functions

Per the *IVI Driver Core specification*, IVI Drivers for instruments that have an ASCII command set such as SCPI shall also provide an API for sending messages to and from the instrument over the ASCII command channel. This section specifies those functions.

Drivers may implement these functions in the driver hierarchy it makes sense.

In the following, '<driver_identifier>' indicates the usual snake case driver identifier. '<hierarchy>' indicates whatever hierarchy path the driver designer chooses for the direct IO functions.

| Required Driver API (IVI Driver Core) | Core IVI-ANSI-C API     |
| ------------------------------------- | --------------------    |
| I/O Timeout Set                       | `<driver_identifier>_<hierarchy>_timeout_milliseconds_set()` |
| I/O Timeout Get                       | `<driver_identifier>_<hierarchy>_timeout_milliseconds_get()` |
| Read Bytes                            | `<driver_identifier>_<hierarchy>_read_bytes()`   |
| Read String                           | `<driver_identifier>_<hierarchy>_read_string()`  |
| Write Bytes                           | `driver_identifier>_<hierarchy>_write_bytes()`   |
| Write String                          | `<driver_identifier>_<hierarchy>_write_string()` |

#### Direct IO ANSI-C Prototypes

In the following, '<driver_identifier>' indicates the usual snake case driver identifier. '<hierarchy>' indicates whatever hierarchy path the driver designer chooses for the direct IO functions.

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
