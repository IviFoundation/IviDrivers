# IVI .NET Drivers With the CS Shared Components

- [IVI .NET Drivers With the CS Shared Components](#ivi-net-drivers-with-the-cs-shared-components)
  - [History and Intent](#history-and-intent)
    - [Resolving the Local Nature of NuGet Installed Assemblies with the IVI Global Configuration Store](#resolving-the-local-nature-of-nuget-installed-assemblies-with-the-ivi-global-configuration-store)
    - [Relationship to the IVI Generation 2014 Specifications](#relationship-to-the-ivi-generation-2014-specifications)
    - [Relationship to the IVI Core and IVI .NET 2025 Specifications](#relationship-to-the-ivi-core-and-ivi-net-2025-specifications)
  - [The IVI .NET CS Shared Components](#the-ivi-net-cs-shared-components)
  - [Using a Driver That Incorporates the CS Shared Components](#using-a-driver-that-incorporates-the-cs-shared-components)
    - [Nuget Package](#nuget-package)
    - [Configuration Store Initialization and Usage](#configuration-store-initialization-and-usage)
      - [Using Multiple Configuration Stores](#using-multiple-configuration-stores)
      - [Interactions Between Multiple Application Configurations](#interactions-between-multiple-application-configurations)
  - [Creating a Driver With the CS Shared Components](#creating-a-driver-with-the-cs-shared-components)
    - [Requirements On IVI Generation 2025 Drivers Using These Components](#requirements-on-ivi-generation-2025-drivers-using-these-components)
    - [Configuration Store Access During Driver Initialization](#configuration-store-access-during-driver-initialization)
    - [Creating a Dual Target Driver](#creating-a-dual-target-driver)
    - [Providing Configuration Store Registration With An Installer](#providing-configuration-store-registration-with-an-installer)
  - [Obsolete IVI Generation 2014 Requirements](#obsolete-ivi-generation-2014-requirements)
    - [Installation - requirement for an installer](#installation---requirement-for-an-installer)
    - [Requirements Irrelevant Dure to Global Nature of IVI Gen 2014](#requirements-irrelevant-dure-to-global-nature-of-ivi-gen-2014)
    - [Bizarre requirements littered in IVI Style guide and IVI 3.1 such as numerical rounding, capitalization, etc](#bizarre-requirements-littered-in-ivi-style-guide-and-ivi-31-such-as-numerical-rounding-capitalization-etc)
  - [Driver Implementation of Required Interfaces](#driver-implementation-of-required-interfaces)
  - [Maintaining the CS Shared Components](#maintaining-the-cs-shared-components)
  - [Proposing Changes to the CS Shared Components](#proposing-changes-to-the-cs-shared-components)

<!-- This document uses HTML comments to suggest appropriate content and conventional markdown content to propse content -->

<!-- This document should be enhanced with things that we learn as we do prototyping of the drivers using the CS Shared Components -->

This document describes the IVI **C**onfigurable **S**ettings Shared Components (henceforth referred to as the CS Shared Components).  These components are provided by the IVI Foundation to facilitate authoring and using Microsoft .NET drivers that work with the IVI Configuration Store to:

- Abstractly instantiate drivers based on the configuration in the IVI Configuration Store
- Configure initial values of settings in the driver that are specified in the IVI Configuration Store

## History and Intent

As the IVI Foundation developed updates to the .NET Driver specifications to support .NET 6+, they determined that there was a need for a lighter, more nimble specification.  However, there were also needs to:

1. migrate previous generations of drivers to the new specification

2. continue to support the breadth of features provided by earlier generations of the standard for those customers that require them

Therefore, when the IVI Foundation created IVI Generation 2025 standards, which do not require all of the features of the previous standards, they also created the *CS Shared Components* to enable driver developers to create drivers that are easily migrated from the IVI Generation 2014 drivers, and can also provide the features associated with those drivers and comply with the IVI Generation 2025 standards.

This document details the IVI CS Shared Components and how to use them.

### Resolving the Local Nature of NuGet Installed Assemblies with the IVI Global Configuration Store

<!-- Explain the general problem, and the solution and how it plays out for .NET Framework and .NET 6+ -->

### Relationship to the IVI Generation 2014 Specifications

<!-- Explain why drivers using these components do not comply with the Gen 2014 specifications.  Sort of a high-level explanation of the section below on obsolete requirements. -->

For details see: [obsolete generation 2014 requirements](#obsolete-ivi-generation-2014-requirements)

### Relationship to the IVI Core and IVI .NET 2025 Specifications

<!-- Explain how these specifications led to an evolution in the standards, and how they all relate -->

<!-- Especially clarifying use of the CS Shared Components vs. the others IVI 2025 Shared Components. -->

<!-- Mention the compliance guarantees if you use *either* these components or the others. -->

## The IVI .NET CS Shared Components

<!-- Any detail on their role not obvious from the introduction -->

<!-- Dual targetted packages for local deployment with NuGet -->

## Using a Driver That Incorporates the CS Shared Components

The CS Shared Components enable framework drivers to comply with IVI Generation 2014 standards, and all the requirements around the use of the IVI Configuration Store.   However, IVI Generation 2014 does not detail how drivers acquired with NuGet work with the Configuration Store.

Although the CS Shared Components could be used to build drivers and deploy them with an installer, this section describes using drivers for .NET FW and .NET 6+ when acquiring them with NuGet.

This section describes from a users perspective how to use drivers built with the IVI CS Shared Components and how they interact with the IVI Configuration Store.

### Nuget Package

<!-- Driver typically provided as a dual target package, .NET FW version behavior, .NET 6+ version behavior.  Mention that the driver NuGet package will have a dependency on the IVI CS Shared Components. -->

### Configuration Store Initialization and Usage

<!-- mention the associated driver installer for registration, and the role of NI-MAX or similar tools -->

#### Using Multiple Configuration Stores

<!-- Discuss the Gen 2014 provisions for multiple configuration stores, and how to load them, and how they may be useful to create local scoped configuration store. -->

#### Interactions Between Multiple Application Configurations

<!-- Discuss potential confusion between globally installed drivers and NuGet installed drivers -->

<!-- Techniques for putting all of your configuration in the global config store, that is what do you do so that mulitple drivers used in multiple applications to not get conflated -->

<!-- Need a warning that since each application has a local copy of the Configuration Server that any incompatibilities in how each Configuration Server version handles the configuration store would be troublesome. -->

## Creating a Driver With the CS Shared Components

The previous sections on *using* drivers establishes most of the requirements on drivers from the perspective of a driver consumer.  This section explicitly discusses the requirements on a driver to support this customer usage and comply with IVI Generation 2025.

In addition to this section, [Driver Implementation of Required Interfaces](#driver-implementation-of-required-interfaces), describes APIs that drivers must implement to comply with IVI Generation 2025.

### Requirements On IVI Generation 2025 Drivers Using These Components

<!-- Per Gen 2014, what the driver has to do to legitimately claim it supports the CS Shared components such as, checking the Configuration Store, proper behaviors.  -->

<!-- Any requirements we want to carry forward from IVI 3.1, 3.5, and others. -->

This section details the driver requirements.  Where the requirements are substantially included in the IVI Generation 2014 requirements, they are only referenced here.

### Configuration Store Access During Driver Initialization

### Creating a Dual Target Driver

### Providing Configuration Store Registration With An Installer

IVI Generation 2014 and before registered drivers in the IVI configuration store when they were installed. By default the The IVI Configuration Store is a global store for driver configuration (including configuration for the IVI Driver Factory which is an abstract factory).

To use the IVI Configuration Store with .NET 6+ applications, the driver must support some way to register its presence in the IVI Configuration store.

<!-- details on how all this works -->

## Obsolete IVI Generation 2014 Requirements

<!-- IVI Generation 2025 Drivers with CS Shared Components Need Not Comply -->
<!-- Various requirements from IVI Generation 2014, that drivers are explicitly permitted to ignore. -->

### Installation - requirement for an installer

<!-- Basic requirement for an installer is replaced by a NuGet package, and a companion installer to do registration -->

<!-- Need outline the requirements on the companion installer -->

### Requirements Irrelevant Dure to Global Nature of IVI Gen 2014

<!-- Installation to IVI directories, registry entries, installation to the GAC, other things that are globally geared -->

### Bizarre requirements littered in IVI Style guide and IVI 3.1 such as numerical rounding, capitalization, etc

<!-- Seems that some effort should go into calling out the fact that IVI Gen 2025 does not have these requirements and they are not implicitly being extended to all drivers that use the CS Shared Components. -->

<!-- Need to carefully consider if interchangeability concerns would lead us to wanting to retain some of these requirements --- that was nominally the reason for many of their inclusion in the original specifications. -->

<!-- Perhaps a broad statement that only the appropriate detailed requirements should be considered obligatory????? -->

## Driver Implementation of Required Interfaces

Although IVI Generation 2025 does not require that drivers implement all of the methods in the IVI common driver interfaces, drivers that support the CS Shared Components are expected to support certain APIs.

Per the IVI Generation 2025 requirements, drivers are required to implement the following interfaces:

<!-- Fill in standard 3 interfaces, presumably referencing Gen 2014 for implementation -->

<!-- Need to call out methods and interfaces that must be implemented for Gen 2025 compliance  -->

## Maintaining the CS Shared Components

This section documents how the IVI Foundation updates and maintains the CS Shared Components

## Proposing Changes to the CS Shared Components
