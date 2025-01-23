# Example README.md

This file introduces version 1.1.0 of the Keysight Technologies KtQ1212x instrument driver.

- Links to more detailed documentation.
- A version history of the driver.
- The compliance documentation, which includes driver information, prerequisites,
  and known issues, as well as a description of the unit test setups used by the
  vendor to qualify the driver.

Additional Documentation:

- The [KtQ1212x Driver Introduction Document](.\Example___Introduction to
  KtQ1212x___.md) includes additional information related to initially using
  the driver.
- The [KtQ1212x Driver Help](<*Link to Driver Help*>) includes more detailed
  information about the driver, including an API reference and instructions for
  using the driver in various development environments.
- Examples may be found online at [KtQ1212x Driver Examples](<*Link to Driver
  Examples*>)

Version History:

- 1.1.0: Adds support for the Keysight Q12121A.

- 1.0.0: Initial release supporting the Keysight Q12120A.

--------------------------------------------------

# Compliance Documentation for the Keysight KtQ1212x Driver

## IVI Compliance Category

This IVI.NET Instrument Driver complies with:

- Ivi Driver Core Specification (Version 1.0)
- Ivi Driver .NET Language Specification (Version 1.0)

## Driver Identification

Driver Version: 1.1.0

Driver Vendor: Keysight Technologies

Driver Identifier: KtQ1212x

Description:  IVI.NET instrument driver for the Keysight Q1212x series of
Function/Arbitrary Waveform Generators

Driver Bitness: 32-bit/64-bit

Source Code: The source code for this driver is available on the Keysight
Technologies web site.

## Hardware Information

Instrument Manufacturer: Keysight Technologies

Supported Instrument Models: Q12120A, Q12121A

Supported Bus Interfaces: USB, LAN, GPIB

## Software Prerequisites Information

Supported Operating Systems:

- 64-bit Windows 10 (all supported versions)
- Windows 11 (all supported versions)

Driver Dependencies (Version):

- .NET Runtime (6.0 or higher)
- VISA.NET for IVI.NET Drivers (7.4 or higher)

## Unit Testing

### Test Setup 1

Instrument Model (Firmware Version): Keysight Q12120A (2.1, 2.21)

Bus Interface: GPIB

Operating System (Version): 64-bit Windows 10 (21H2), Windows 11 (23H2)

Driver Bitness: 32-bit, 64-bit

VISA Vendor (Version): Keysight IO Libraries 2024 (21.0.47)

IVI.NET Core Driver Shared Components Version: 1.0.0

### Test Setup 2

Instrument Model (Firmware Version): Keysight Q12121A (1.13)

Bus Interface: USB, LAN

Operating System (Version): Windows 11 (23H2)

Driver Bitness: 64-bit

VISA Vendor (Version): Keysight IO Libraries 2024 (21.0.47)

IVI.NET Core Driver Shared Components Version: 1.0.0

## Driver Installation Testing

Operating System (Version): 64-bit Windows 10 (21H2), Windows 11 (23H2)

## Driver Buildability

Operating System (Version): Windows 11 (23H2)

## Driver Test Failures

> Known Issues: None
