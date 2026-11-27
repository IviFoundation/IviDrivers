using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Reflection;
using Ivi.DriverCore;
using Acme.AcmeDriver;

static string PackageVersionOf(Type type) =>
    type.Assembly.GetCustomAttribute<AssemblyInformationalVersionAttribute>()?.InformationalVersion
    ?? type.Assembly.GetName().Version?.ToString()
    ?? "unknown";

Console.WriteLine("  AcmeExample");
Console.WriteLine();
 
// Edit resourceName and options as needed. resourceName is ignored if option Simulate=true
var resourceName = "resourceName";
// resourceName is an IP Address (e.g. 127.0.0.1) of the instrument, this example is working on raw socket connection with port 5025
 
// Edit the initialization options as needed.
var simulate = true;
var idquery = true;
var reset = true;
 
try
{
    // Call driver constructor with options.  'using' block calls driver.Close() when exiting.
    using var driver = new AcmeDriver(resourceName, idquery, reset, simulate);
 
    Console.WriteLine("Driver Initialized");
    // Shows which packages were consumed (the newest in the local feed, preview or release).
    Console.WriteLine("AcmeDriver package version:         {0}", PackageVersionOf(typeof(AcmeDriver)));
    Console.WriteLine("IviFoundation.DriverCore version:   {0}", PackageVersionOf(typeof(IIviDriverCore)));
    // Print a few IIvi.DriverCore properties
    Console.WriteLine("Component Vendor:  {0}", driver.ComponentVendor);
    Console.WriteLine("Revision:    {0}", driver.Generation);
    Console.WriteLine("Component Version:      {0}", driver.ComponentVersion);
    Console.WriteLine("All Supported Models:    {0}", string.Join(", ", driver.GetSupportedInstrumentModels()));
 
    // TODO: Exercise driver methods and properties
 
    // Check instrument for errors
    Console.WriteLine();
    ErrorQueryResult? result;
    while ((result = driver.ErrorQuery()) is { } error)
    {
        Console.WriteLine("ErrorQuery: {0}, {1}", error.Code, error.Message);
    }
    Console.WriteLine("ErrorQuery: no errors");
}
catch (Exception ex)
{
    Console.WriteLine("Exception: " + ex.Message);
    // A non-zero exit code lets a pipeline use this example to validate the driver packages.
    Environment.ExitCode = 1;
}

// Only wait for Enter when a person is running the example. In a pipeline stdin is redirected or absent.
if (!Console.IsInputRedirected)
{
    Console.WriteLine("\nDone - Press Enter to Exit");
    Console.ReadLine();
}
