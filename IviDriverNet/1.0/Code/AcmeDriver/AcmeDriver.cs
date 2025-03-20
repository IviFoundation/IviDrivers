using Ivi.DriverCore;
using System.Text;
using System.Net.Sockets;


namespace Acme.AcmeDriver
{
    /// <summary>
    /// Class <c>AcmeDriver</c> for a sample Dmm driver proto code.
    /// </summary>
    public sealed class AcmeDriver : IIviDriverCore, IDisposable
    {
        private TcpClient? _tcpClient; 
        private NetworkStream? _stream; 
        private string _ipAddress = string.Empty;
        private int _port;
        private bool _simulate = true;
        private bool _reset = true;
        private string _instrumentManufacturer = "Acme";
        private string _instrumentModel = "KtDmm";
        private string _simModel = "KtSim123";
        private bool _queryInstrumentStatus = true;
        private bool _disposed = false;
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
        public string ComponentVendor => "Acme Technologies";
        /// <summary>
        /// The name of the manufacturer reported by the physical instrument. If Simulation is enabled, this 
        /// property returns the following:
        /// "The 'InstrumentManufacturer' operation is not available while in simulation mode.".
        /// </summary>
        public string InstrumentManufacturer => _instrumentManufacturer; 
        /// <summary>
        /// The model number or name reported by the physical instrument. If Simulation is enabled or the 
        /// instrument is not capable of reporting the model number or name, a string is returned that  explains
        /// the condition.  Model is limited to 256 bytes.
        /// </summary>
        public string InstrumentModel => _instrumentModel; 
        /// <summary>
        /// If True, the driver does not perform I/O to the instrument, and returns simulated values for output 
        /// parameters.
        /// </summary>
        public bool Simulate => _simulate;
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
        public bool QueryInstrumentStatus
        {
            get => _queryInstrumentStatus;
            set => _queryInstrumentStatus = value;
        }

        /// <summary>
        /// This method reset the instrument.
        /// </summary>
        public void Reset()
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="AcmeDriver"/> class.
        /// </summary>
        /// <param name="resourceName">The resource name of the instrument.</param>
        public AcmeDriver(string resourceName) : this(resourceName, false, false, false)
        {

        }
        /// <summary>
        /// Initializes a new instance with ID query option.
        /// </summary>
        /// <param name="resourceName">The resource name of the instrument.</param>
        /// <param name="idQuery">If true, performs an ID query.</param>
        public AcmeDriver(string resourceName, bool idQuery) : this(resourceName, idQuery, false, false)
        {

        }
        /// <summary>
        /// Initializes a new instance with ID query and reset options.
        /// </summary>
        /// <param name="resourceName">The resource name of the instrument.</param>
        /// <param name="idQuery">If true, performs an ID query.</param>
        /// <param name="reset">If true, resets the instrument.</param>
        public AcmeDriver(string resourceName, bool idQuery, bool reset) : this(resourceName, idQuery, reset, false)
        {

        }
        /// <summary>
        /// Initializes a new instance with full configuration options.
        /// </summary>
        public AcmeDriver(string resourceName, bool idQuery, bool reset, bool simulate)
        {
            Initialize(resourceName, idQuery, reset, simulate);
        }
                /// <summary>
        /// Initializes the driver with given parameters.
        /// </summary>
        public void Initialize(string resourceName, bool idQuery, bool reset, bool simulate)
        {
            // var settingsPairs = options.Split(',');

            //Set IO Mechanism here
            _ipAddress = resourceName;
            _port = 5025;
            _reset = reset;
            _simulate = simulate;

            // Iterate through each key-value pair
            if (_simulate)
            {
                Console.WriteLine($"Connected to {_ipAddress}:{_port}");
                _instrumentManufacturer = "Acme Technologies";
                _instrumentModel = _simModel;
            }
            else
            {
                _tcpClient?.Dispose();
                _stream?.Dispose();

                _tcpClient = new TcpClient();
                _tcpClient.Connect(_ipAddress, _port);
                Console.WriteLine($"Connected to {_ipAddress}:{_port}");

                _stream = _tcpClient.GetStream();
                if (_stream == null || !_stream.CanWrite)
                {
                    Console.WriteLine("Error: Network stream is not available for writing.");
                    return;
                }
                string command = "*IDN?\n";
                byte[] commandBytes = Encoding.ASCII.GetBytes(command);
                _stream.Write(commandBytes, 0, commandBytes.Length);
                Console.WriteLine($"Sent command: {command.Trim()}");

                byte[] responseBuffer = new byte[1024];
                int bytesRead = _stream.Read(responseBuffer, 0, responseBuffer.Length);
                string response = Encoding.ASCII.GetString(responseBuffer, 0, bytesRead);
                var instrumentInfo = response.Split(',');
                _instrumentManufacturer = instrumentInfo[0];
                _instrumentModel = instrumentInfo[1];
            }
        }
        /// <summary>
        /// Close an instrument session.
        /// </summary>
        public void Close()
        {
            Dispose();
        }
        /// <summary>
        /// Disposes all resources used by the driver.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                _stream?.Dispose();
                _tcpClient?.Dispose();
                _disposed = true;
            }
        }
        /// <summary>
        /// Result of an error query operation.
        /// </summary>
        public ErrorQueryResult ErrorQuery() => new ErrorQueryResult(0, "No Error");

        /// <summary>
        /// Retrieves the list of supported instrument models compatible with this driver.
        /// </summary>
        public string[] GetSupportInstrumentModels() => new string[] { "KtDmm1", "KtDmm2" };
    }
}
