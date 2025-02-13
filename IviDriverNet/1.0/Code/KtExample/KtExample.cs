using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Ivi.DriverCore;
using Keysight.KtIviNetDriver;

namespace KtIviNetDriver_Cs_Example1
{
    /// <summary>
    /// KtIviNetDriver IVI.NET Driver C# Example Program
    /// 
    /// Creates a driver object, reads a few Identity interface properties, and checks the instrument error queue.
    /// May include additional instrument specific functionality.
    /// 
    /// See driver help topic "Programming with the IVI.NET Driver in Various Development Environments"
    /// for additional programming information.
    ///
    /// Runs in simulation mode without an instrument.
    /// 
    /// Requires a reference to the driver's .NET assembly.
    /// 
    /// </summary>
    class Program
	{
		public static void Main(string[] args)
		{
            Console.WriteLine("  CS_Example1");
            Console.WriteLine();

            // Edit resourceName and options as needed.  resourceName is ignored if option Simulate=true
            // For this example, resourceName may be a VISA address(e.g. "TCPIP0::<IP_Address>::INSTR") or a VISA alias.
            // For more information on using VISA aliases, refer to the Keysight IO Libraries Connection Expert documentation.
            var resourceName = "MyVisaAlias";
            //resourceName = "TCPIP0::127.0.0.1::INSTR";

            // Edit the initialization options as needed.
            var options = "QueryInstrStatus=false, Simulate=true, DriverSetup= Trace=false";
            var idquery = true;
            var reset   = true;

            try
            {
                // Call driver constructor with options.  'using' block calls driver.Close() when exiting.
                using (var driver = new KtIviNetDriver(resourceName, idquery, reset, options))
			    {
                    Console.WriteLine("Driver Initialized");

                   // Print a few IIviDriverIdentity properties
                    Console.WriteLine("Identifier:  {0}", driver.DriverVendor);
                    Console.WriteLine("Revision:    {0}", driver.Generation);
                    Console.WriteLine("Vendor:      {0}", driver.DriverVersion);
                    Console.WriteLine("Description: {0}", driver.GetSupportInstrumentModels());

                    // TODO: Exercise driver methods and properties


                    // Check instrument for errors
                    ErrorQueryResult result;
                    Console.WriteLine();
                    //do
                    //{
                    //    result = driver.ErrorQuery();
                    //    // Console.WriteLine("ErrorQuery: {0}, {1}", result., result.Message);
                    //} while (result.Code != 0);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
            }

			Console.WriteLine("\nDone - Press Enter to Exit");
			Console.ReadLine();
		}

    }
}
