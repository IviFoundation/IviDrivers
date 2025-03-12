# IVI .NET Drivers With the CS Shared Components

> [!WARNING]
> DRAFT VERSION UNDER DISCUSSION BY THE IVI CONSORTIUM.  Generally accurate, but check back for current version.

- [IVI .NET Drivers With the CS Shared Components](#ivi-net-drivers-with-the-cs-shared-components)
  - [History and Intent](#history-and-intent)
    - [Relationship to the IVI Core and IVI Core .NET Specifications](#relationship-to-the-ivi-core-and-ivi-core-net-specifications)
    - [Relationship to the IVI Generation 2014 Specifications](#relationship-to-the-ivi-generation-2014-specifications)
  - [Upgrading an IVI 2014 .NET Framework Driver to .NET 6+](#upgrading-an-ivi-2014-net-framework-driver-to-net-6)
  - [Using a CS Shared Components .NET 6+ Driver (TODO)](#using-a-cs-shared-components-net-6-driver-todo)
  - [Proposing changes to the CS Shared Components](#proposing-changes-to-the-cs-shared-components)

This document describes the IVI Configurable Settings Shared Components (henceforth referred to as the CS Shared Components). These components are provided by the IVI Foundation to facilitate authoring and using Microsoft .NET drivers that work with the IVI Configuration Store to:

- Abstractly instantiate drivers based on configuration in the IVI Configuration Store
- Configure initial values of settings in the driver that are specified in the IVI Configuration Store

## History and Intent

As the IVI Foundation developed updates to the .NET Driver specifications to support .NET 6+, they determined there was a need for a lighter, more nimble specification. However, there were also needs to:

1. migrate previous generations of drivers to the new specification
2. continue to provide some level of support for the breadth of features provided by earlier generations of the standard

The IVI Generation 2026 standard was created to satisfy the need for a nimbler specification. IVI Generation 2026 does not require all the features of previous standards.

To enable migration of previous generations of drivers and the feature set the IVI Foundation provides the _CS Shared Components_. The CS Shared Components are a port of the existing IVI 2014 .NET Framework Shared Components to .NET 6. They are delivered via a [NuGet package](https://nuget.org), and support making IVI 2014 .NET Framework drivers compatible with .NET 6+.

### Relationship to the IVI Core and IVI Core .NET Specifications

An existing IVI Generation 2014 .NET Framework driver is compliant with the IVI Generation 2026 Driver Core Specification, as described [here](https://github.com/IviFoundation/IviDriver/blob/main/IviDriverCore/1.0/Spec/IviDriverCore.md). To be compliant with the .NET Specific driver specification, you must adhere to the [IVI Driver Core .NET specification](https://github.com/IviFoundation/IviDriver/blob/main/IviDriverNet/1.0/Spec/IviDriverNet.md). For example, you must [provide a NuGet package](https://github.com/IviFoundation/IviDriver/blob/main/IviDriverNet/1.0/Spec/IviDriverNet.md#packaging-requirements-for-net-6) that contains your driver implementation.

### Relationship to the IVI Generation 2014 Specifications

Because the IVI Generation 2014 specification is only written for .NET Framework, it is not possible to create a .NET 6+ driver that is compliant with the IVI Generation 2014 specification. However, the CS Shared Components are designed to be compatible with .NET 6+ to enable driver vendors to migrate their .NET Framework drivers to support .NET 6+ and be compliant with IVI Generation 2026.

## Upgrading an IVI 2014 .NET Framework Driver to .NET 6+

- In order to be an IVI Generation 2026 .NET Core compliant driver, a NuGet package must be provided in addition to or instead of the previously required standalone installer. The following provides documentation for [providing an IVI .NET Core compliant NuGet package](https://github.com/IviFoundation/IviDriver/blob/main/IviDriverNet/1.0/Spec/IviDriverNet.md#packaging-requirements-for-net-6).
- Previously IVI Generation 2014 compliant drivers are IVI Generation 2026 compliant if the .NET support is provided via a Nuget package and targets .NET 6+. The IVI Generation 2026 CS Shared Components were created as a .NET 6+ compatible replacement for the IVI Generation 2014 Shared Components.
- NuGet packages are unable to install IVI Configuration Store support for Configurable Settings. As a result, when migrating an existing driver to IVI Generation 2026 .NET Core standards, it is recommended that a standalone installer is distributed in addition to the NuGet package. The following are some ways to accomplish this:
  - Create a thin installer that only provides support for updating the IVI Configuration Store.
  - Provide the existing .NET Framework driver installer.
- In order to be an IVI Configurable Settings driver, the driver must:
  - Deliver a .NET 6+ driver support NuGet package that depends on the IVI Generation 2026 CS Shared Components NuGet package.
  - Supply support for the IVI Configuration Store via a standalone installer as described above.
- If a standalone installer is not distributed, the driver is **not** an IVI Configurable Settings driver. However, as long as the criteria above is met, the driver can be an IVI Generations 2026 .NET Core compliant driver. In this scenario, the driver must work in the absence of the IVI Configuration Store.
- As part of migrating IVI Generation 2014 .NET Framework drivers to IVI Generation 2026 .NET Core, appropriate compliance documentation must be provided and installed with the NuGet .NET 6+ driver support package that complies with the IVI Generation 2026 .NET standards. For specific details, visit [IviDriverCore Compliance Documentation](https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverCore/1.0/Spec/IviDriverCore.md#compliance-documentation)
  - In the compliance documentation, it is recommended to explicitly specify whether the driver is an IVI Configurable Settings driver in addition to IVI Generation 2026 .NET Core compliant.
- A driver migrating from IVI Generation 2014 .NET Framework to IVI Generation 2026 .NET Core is not required to follow IVI Generation 2014 Installation requirements. Only the IVI Generation 2026 .NET Core driver requirements are necessary.
  - For example, a driver performing this migration no longer needs to follow previously mandated IVI Generation 2014 .NET Framework installation requirements such as installation to Common IVI Directories, which is unable to be accomplished via a NuGet package.

## Using a CS Shared Components .NET 6+ Driver (TODO)

- beware configuration – it’s possible your existing applications no longer work if they depend on configurable settings.
- beware the static factories – it’s possible your existing applications no longer work if they use the static factories (requires configurable settings, and static references to the driver implementations).
- beware app domains (they don't exist)

## Proposing changes to the CS Shared Components

Email admin@ivifoundation.org (or should this be posted in GitHub and is that what we recommend people to use?)
