# Example Driver Introduction Document

The following prose shows an example driver introduction document. In
the following, productions indicated by italic texts within '<>'
should be replaced by actual values for the driver.

============== Driver Introduction Document Example ==================

Sample Driver Introduction Document for an IVI driver

## Driver Documentation

**README.md** can be found in <*directory*>. It contains
notes about installation, known issues, and the driver's version
history.

**Help Documentation:** Driver help may be found in
<*DriverIdentifier*>.md at <*url*>. It contains:

- Suggestions for getting started, with links to examples

- General information about using the driver

- Reference information for the driver's application programming
  interface (API), including all methods and properties.

- Information about using the driver in a variety of development
  environments including Visual Studio, LabVIEW, and MATLAB

- IVI compliance documentation

## Driver Source Code and Examples

The IVI.NET driver source code can be found at <*url*>. For
instructions on rebuilding the driver, refer to the "Building the
Driver Source Code" topic in the help file.

Driver example(s) can be found in <*url*>. For instructions on
building the examples, refer to the "Driver Examples" topic in the
help file.

## Connecting to the Instrument

The "Driver Examples" topic in the help file documents program examples
for common development environments. Each of these examples illustrates
how to connect to an instrument in the respective development
environment.

## Configuring Instrument Settings

The *\<DriverIdentifier\>* API includes methods and properties for
setting instrument state variables, controlling the instrument, and
reading results from the instrument. These are documented in the
*Driver API Reference* topic in the help file.

## Configuring Driver Settings

The *\<DriverIdentifier\>* API includes methods and properties for
setting variable used to control driver operation. These are documented
in the *Driver API Reference* topic in the help file.

## Direct I/O

The *\<DriverIdentifier\>* API includes methods and properties that
allow calling programs to read and write directly with the instrument.
In addition, the *DirectIO* property provides a reference to the
underlying I/O library.  These are documented in the *Direct I/O*
topic in the help file.

## Instrument Command Coverage

The *Driver API Reference* topic in the help file lists the instrument
command(s) that the driver implements for each function and property.

Some commands and queries are not suitable for an instrument driver.
The following commands are NOT implemented in this driver:

- All commands in the *\<DIAGnostic\>* subsystem

- All commands in the *\<CALibrate\>* subsystem

- All commands in the *\<FORMat\>* subsystem

- All commands in the *\<DISPlay\>* subsystem

- All commands in the *\<SYSTem:COMMunicate\>*subsystem

- Undocumented SCPI commands

- Specific commands:

  - *HARDCopy*

  - *MEMory:STATe*

  - *CURSor*

Driver users can send any commands to a message-based instrument using
the driver's Direct I/O functions.

## Known Issues

The README.md file and the Compliance Document contain information about
known issues.

## Contact Support

If you have feedback or need help using this driver, contact
\<*appropriate support contact information*\>.

## Trademarks

*\<Optionally add trademark statements here\>*
