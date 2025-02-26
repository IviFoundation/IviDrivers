# IviFoundation.DriverCore Shared Component 

## Overview

The **IviDriverCore** Shared Components are essential for the development and use of IVI.NET Framework and .NET 6.0+ drivers. These netstandard2.0 shared components provide a standardized set of services that are common across all IVI .NET Framework and .NET 6.0+ drivers, ensuring consistency and compatibility when combining drivers and software from various vendors.

## Key Features
- IVI Driver Core Specification version 1.0
- Targeted support
    - netstandard2.0

## Package Details
- Type: NuGet
- Location: NuGet.org
    - Access: Public
    - Name: IviFoundation.DriverCore
    - Version: 1.0.0-preview
## Installation

To install the package via **.NET CLI**, run:

```sh

dotnet add package IviFoundation.DriverCore --version 1.0.0-preview

```

Or, using **NuGet Package Manager**:

```sh

NuGet\Install-Package IviFoundation.DriverCore -Version 1.0.0-preview

```
 
## Usage Example

Here’s how you can use `IviDriverCore` Shared Component in your .NET project:
 
```csharp

using Ivi.DriverCore;
 
public class Example

{

    public void InitializeDriver()

    {

        // Example usage of IVI Driver Core

        var driver = new IviDriver();

        driver.Initialize();

        Console.WriteLine("Driver Initialized Successfully.");

    }

}

```
 
