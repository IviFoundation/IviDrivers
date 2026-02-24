# IviDrivers

This repository is maintained by the IVI Foundation and contains code and specifications related to instrument drivers. Specifically:

- IVI Driver Specifications
- IVI Driver Shared Components that support the specifications
- Additional Documentation regarding IVI Foundation Instrument Drivers that may be useful for driver designers
- Example instrument drivers that developers can use to get started with the IVI driver standards

See [Instrument Drivers Specs Repo Org](Documentation/InstrumentDriverSpecsRepoOrg.md) for details on the organization of this repository

The [IVI Foundation Web Site](https://www.ivifoundation.org) has additional information on the IVI Foundation, its standards, and how to get started using them.

## Specifications

The IVI Specifications in this repository are:

| Specification | Description |
| --- | pdf | --- |
| IVI Core | [pdf](https://github.com/IviFoundation/IviDrivers/blob/joe/updatesPrimarilyForPDFs/IviDriverCore/1.0/Spec/IviDriverCore-1.0.pdf) | This specification establishes the IVI Generation 2026 driver standards. It describes requirements common to all IVI Core Drivers, regardless of implementation language. Additional specifications (in this table) detail the requirements of drivers for specific programming languages.|
| IVI-NET | [pdf](https://github.com/IviFoundation/IviDrivers/blob/joe/updatesPrimarilyForPDFs/IviDriverNet/1.0/Spec/IviDriverNet-1.0.pdf) | This specification contains the Microsoft .NET 6+ specific requirements for an IVI.NET driver, it is an IVI Language-Specific specification. Drivers that comply with this specification are also required to comply with the [IVI Driver Core specification](https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/IviDriverCore.md). |
| IVI-ANSI-C | [pdf](https://github.com/IviFoundation/IviDrivers/blob/joe/updatesPrimarilyForPDFs/IviDriverAnsiC/1.0/Spec/IviDriverAnsi-C-1.0.pdf) |This specification contains the ANSI C specific requirements for an IVI-ANSI-C driver, it is an IVI Language-Specific specification. This specification is to be used in conjunction with the [IVI Driver Core specification](https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/IviDriverCore.md). |

The following are the major directories in this repository:

| Directory | Contents |
| --------- | -------- |
| IviDriverAnsiC | The IVI Language-Specific specification for ANSI-C drivers. This directory also contains an example include file containing examples of the required driver functions. |
| IviDriverCore | The *IVI Driver Core Specification* and related files. It describes requirements common to all IVI Core Drivers, regardless of implementation language. IVI provides additional specifications that detail the requirements of drivers for specific languages. |
| IviEula | The IVI End User License Agreement used for all IVI provided software, including software used for both IO and Drivers |
| IviDriverNet | The IVI Language-Specific specification for Microsoft .NET 6+ specific requirements for an IVI .NET driver. This directory also contains the IVI Driver Shared Components (IVI DSC) for use with these drivers. |
| Generations | The IVI Specifications are organized into generations that help identify families of specifications that are to be used together. The various IVI Generations are documented in the *Generations* directory.

See the [IVI Foundation Website](https://www.ivifoundation.org) for more information on the foundation and these standards.

## Contact

To contact the foundation about the material in this repository, see: [Contact IVI Foundation](https://www.ivifoundation.org/About-the-Foundation/Contact-Us.html)
