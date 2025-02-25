using Ivi.DriverCore;
using System.Text;
using System.Net.Sockets;


namespace Keysight.KtIviNetDriver
{
    /// <summary>
    /// Class <c>KtIviNetDriver</c> for a sample Dmm driver proto code.
    /// </summary>
    public sealed class KtIviNetDriver : IIviDriverCore, IDisposable
    {
        private TcpClient? tcpClient;
        private NetworkStream? stream;
        private string _ipAddress;
        private int _port;
        private bool _simulate = true;
        bool _reset = true;
        string _InstrumentManufacturer = "Keysight";
        string _InstrumentModel = "KtDmm";
        string simModel = "KtSim123";

        internal bool _QueryInstrumentStatus = true;
        private bool disposed = false;
        /// <summary>
        /// The firmware revision reported by the physical instrument.  If Simulation is enabled, this property 
        /// returns the following: "The 'InstrumentFirmwareRevision' operation is not available while in simulation
        /// mode.".
        /// </summary>
        public string ComponentVersion => "1.0.0";
        /// <summary>
        /// The name of the manufacturer reported by the physical instrument. If Simulation is enabled, this 
        /// property returns the following:
        /// "The 'InstrumentManufacturer' operation is not available while in simulation mode.".
        /// </summary>
        public string ComponentVendor => "Keysight Technologies";

        /// <summary>
        /// The name of the manufacturer reported by the physical instrument. If Simulation is enabled, this 
        /// property returns the following:
        /// "The 'InstrumentManufacturer' operation is not available while in simulation mode.".
        /// </summary>
        public string InstrumentManufacturer
        {

            get
            {

                return _InstrumentManufacturer;
            }
        }

        /// <summary>
        /// The model number or name reported by the physical instrument. If Simulation is enabled or the 
        /// instrument is not capable of reporting the model number or name, a string is returned that  explains
        /// the condition.  Model is limited to 256 bytes.
        /// </summary>
        public string InstrumentModel
        {
            get
            {
                return _InstrumentModel;
            }
        }

        /// <summary>
        /// If True, the driver does not perform I/O to the instrument, and returns simulated values for output 
        /// parameters.
        /// </summary>
        public bool Simulate
        {
            get
            {
                return _simulate;
            }
        }
        /// <summary>
        /// The name of the driver generation reported by the physical instrument. If Simulation is enabled, this 
        /// property returns the following:
        /// "The 'Driver Generation' operation is not available while in simulation mode.".
        /// </summary>
        public string Generation => "IVI-2025";
        /// <summary>
        /// If True, the driver queries the instrument status at the end of each method or property that performs 
        /// I/O to the instrument.  If an error is reported, use ErrorQuery to retrieve error messages one at a 
        /// time from the instrument.
        /// </summary>
        public bool QueryInstrumentStatus { get { return _QueryInstrumentStatus; } set { value = _QueryInstrumentStatus; } }

        /// <summary>
        /// This method reset the instrument.
        /// </summary>
        public void Reset()
        {

        }
        /// <summary>
        /// Initializes an instrument session if simulate=true, I/O will not be done.
        /// </summary>
        public KtIviNetDriver(string resourceName, bool idQuery, bool reset, string options)
        {
            var settingsPairs = options.Split(',');

            //Set IO Mechanism here
            _ipAddress = resourceName;
            _port = 5025;
            _reset = reset;

            // Iterate through each key-value pair
            foreach (var pair in settingsPairs)
            {
                var keyValue = pair.Split('=');

                if (keyValue.Length == 2)
                {
                    string key = keyValue[0].Trim();
                    string value = keyValue[1].Trim();

                    // Map each key to the corresponding property
                    switch (key)
                    {

                        case "Simulate":
                            _simulate = Convert.ToBoolean(value);
                            break;
                        case "Model":
                            if (value != "")
                                simModel = value;
                            break;
                        default:
                            break;
                    }
                }
            }

            if (_simulate)
            {
                Console.WriteLine($"Connected to {_ipAddress}:{_port}");
                _InstrumentManufacturer = "Keysight Technologies";
                _InstrumentModel = simModel;
            }
            else
            {
                tcpClient = null;
                stream = null;
                tcpClient = new TcpClient();
                tcpClient.Connect(_ipAddress, _port);
                Console.WriteLine($"Connected to {_ipAddress}:{_port}");
                stream = tcpClient.GetStream();
                if (stream == null || !stream.CanWrite)
                {
                    Console.WriteLine("Error: Network stream is not available for writing.");
                    return;
                }
                string command = "*IDN?\n";
                byte[] commandBytes = Encoding.ASCII.GetBytes(command);
                stream.Write(commandBytes, 0, commandBytes.Length);
                Console.WriteLine($"Sent command: {command.Trim()}");
                byte[] responseBuffer = new byte[1024];
                int bytesRead = stream.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
                var instrumentInfo = response.Split(',');
                _InstrumentManufacturer = instrumentInfo[0];
                _InstrumentModel = instrumentInfo[1];
            }
        }
        /// <summary>
        /// Close an instrument session.
        /// </summary>
        public void Close()
        {

            if (_simulate)
            {
                Console.WriteLine("Session closed successfully.");
            }
            else
            {
                try
                {
                    if (stream != null)
                    {
                        stream.Close();
                    }
                    if (tcpClient != null)
                    {
                        tcpClient.Close();
                    }
                    Console.WriteLine("Session closed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error closing session: {ex.Message}");
                }
            }
        }
        /// <summary>
        /// It has implemented a finalizer.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);

            // Use SupressFinalize in case a subclass 
            // of this type implements a finalizer.
            GC.SuppressFinalize(this);
        }
        /// <summary>
        /// Releases all resources used by the KtIviNetDriver.
        /// </summary>
        public void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing && tcpClient != null)
                {
                    tcpClient.Dispose();
                }

                disposed = true;
            }
        }
    /// <summary>
    /// Result of an error query operation.
    /// </summary>
        public ErrorQueryResult ErrorQuery() => new ErrorQueryResult(0, "No Error");
        
        /// <summary>
        /// Retrieves the list of supported instrument models compatible with this driver.
        /// </summary>
        public string[] GetSupportInstrumentModels()=> new string[] {"KtDmm1","KtDmm2"};

    }
}
