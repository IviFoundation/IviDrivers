# Package Info

|field   | value |
|---     |  ---  |
|Title   | IVI Driver Core Shared Library |
|Description |IVI Foundation Driver Shared Library for IVI Generation 2026  |
|Authors |  IVI Foundation |
|Owners  | IVI Foundation |
|Tags    | IVI  IVI-Driver-NET-Conformant IVI-Driver-SharedComponents IVI-Foundation |

<!-- Following MD is the README.MD file -->
# IviFoundation.DriverCore Shared Component 

The IVI Driver Core Shared Library contains an interface implemented by all IVI .NET Drivers that conform to IVI Generation 2026. For details on IVI Generation 2026 and the associated standards, see <https://ivifoundation.org>.  The standard regulating this package can be found at [IVI Foundation](https://www.ivifoundation.org/downloads/PostGen2025/IviDriverNet.pdf) or on [github](https://github.com/IviFoundation/IviDrivers/blob/main/IviDriverNet/1.0/Spec/IviDriverNet.md).

The interface in the assembly of this NuGet package provides generally useful utility APIs for working with IVI drivers.  In addition, providing this common interface permits drivers to be used abstractly.

## Overview

The **IviDriverCore** Shared Components are essential for the development and use of IVI.NET Framework and .NET 6.0+ drivers. These dual-targeted shared components provide a standardized set of services that are common across all IVI .NET Framework and .NET 6.0+ drivers, ensuring consistency and compatibility when combining drivers and software from various vendors.

## Key Features

- IVI Driver Core Specification version 1.0
- Targeted support
    - netstandard2.0

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
