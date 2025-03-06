using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Ivi.DriverCore;
using Keysight.KtIviNetDriver;
 
Console.WriteLine("  KtExample");
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
    using var driver = new KtIviNetDriver(resourceName, idquery, reset, simulate);
 
    Console.WriteLine("Driver Initialized");
    // Print a few IIvi.DriverCore properties
    Console.WriteLine("Component Vendor:  {0}", driver.ComponentVendor);
    Console.WriteLine("Revision:    {0}", driver.Generation);
    Console.WriteLine("Component Version:      {0}", driver.ComponentVersion);
    Console.WriteLine("All Supported Models:    {0}", string.Join(", ", driver.GetSupportInstrumentModels()));
 
    // TODO: Exercise driver methods and properties
 
    // Check instrument for errors
    Console.WriteLine();
    var result = new ErrorQueryResult();
    do
    {
        result = driver.ErrorQuery();
        Console.WriteLine("ErrorQuery: {0}, {1}", result.Code, result.Message);
    } while (result.Code != 0);
}
catch (Exception ex)
{
    Console.WriteLine("Exception: " + ex.Message);
}
 
Console.WriteLine("\nDone - Press Enter to Exit");
Console.ReadLine();
