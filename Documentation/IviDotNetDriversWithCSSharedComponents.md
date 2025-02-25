# IVI .NET Drivers With the CS Shared Components

- [IVI .NET Drivers With the CS Shared Components](#ivi-net-drivers-with-the-cs-shared-components)
  - [History and Intent](#history-and-intent)
    - [Relationship to the IVI Core and IVI Core .NET Specifications](#relationship-to-the-ivi-core-and-ivi-core-net-specifications)
    - [Relationship to the IVI Generation 2014 Specifications](#relationship-to-the-ivi-generation-2014-specifications)
  - [Upgrading an IVI 2014 .NET Framework Driver to .NET 6+ (TODO)](#upgrading-an-ivi-2014-net-framework-driver-to-net-6-todo)
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

## Upgrading an IVI 2014 .NET Framework Driver to .NET 6+ (TODO)

- deliver via NuGet
- Describe why you might want to include an installer. Describe what an installer buys you, and what you lose if you do not have one.  
  - Do we point out some ways to do this? (e.g., thin installer for Config Store, vs full framework driver installer?)
- Include notes about what’s required to do in order to claim you are a “CS” driver?
  - YES! -- But where do we draw the line?  Do we expect a "CS" driver to play nice with the global config store if properly installed and setup? (I think so?)
  - Probably should require reasonable behavior (no need to elaborate) if no confiugation store is found
- If you do not have an installer, you need to be careful when reading “configuration”
- Make sure we include information about the compliance document
- Perhaps just a bullet list of some potential differences from the original Gen 2014 driver:
  - Some simple observation (?) that a migrated driver will naturally not need to comply with IVI 2014 installation requirements, and perhaps detailed driver requirements intentionally elided from the Generation 2026 documents.
  - Observe that various 2014 requirements around global settings (such as installation to common IVI directories) do not make sense for a CS Driver acquired with NuGet

## Using a CS Shared Components .NET 6+ Driver (TODO)

- beware configuration – it’s possible your existing applications no longer work if they depend on configurable settings.
- beware the static factories – it’s possible your existing applications no longer work if they use the static factories (requires configurable settings, and static references to the driver implementations).
- beware app domains (they don't exist)

## Proposing changes to the CS Shared Components

Email admin@ivifoundation.org (or should this be posted in GitHub and is that what we recommend people to use?)
