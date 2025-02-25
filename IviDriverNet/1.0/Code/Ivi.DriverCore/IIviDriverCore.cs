namespace Ivi.DriverCore
{

    /// <summary>
    /// Interface for IVI Driver Core. This interface defines the essential properties and methods that IVI drivers must implement.
    /// </summary>
    public interface IIviDriverCore
    {
        /// <summary>
        /// Gets the Component version of the driver.
        /// </summary>
        /// <remarks>
        /// The version string should follow the format "MajorVersion.MinorVersion.PatchVersion".
        /// </remarks>
        String ComponentVersion { get; }

        /// <summary>
        /// Gets the name of the Component vendor.
        /// </summary>
        /// <remarks>
        /// Example: "Keysight Technologies".
        /// </remarks>
        String ComponentVendor { get; }

        /// <summary>
        /// Gets the name of the instrument's manufacturer.
        /// </summary>
        /// <remarks>
        /// Example: "Keysight Technologies".
        /// </remarks>
        String InstrumentManufacturer { get; }

        /// <summary>
        /// Gets the model number or name of the instrument.
        /// </summary>
        /// <remarks>
        /// Example: "34410A".
        /// </remarks>
        String InstrumentModel { get; }

        /// <summary>
        /// Gets or sets a value indicating whether the instrument's status should be queried after each operation.
        /// </summary>
        /// <remarks>
        /// When set to true, the driver queries the instrument's status after every method call to check for errors.
        /// </remarks>
        Boolean QueryInstrumentStatus { get; set; }

        /// <summary>
        /// Gets a value indicating whether the driver is operating in simulation mode.
        /// </summary>
        /// <remarks>
        /// In simulation mode, the driver does not perform actual I/O with the instrument and generates simulated output.
        /// </remarks>
        Boolean Simulate { get; }

        /// <summary>
        /// Queries the instrument for any error information.
        /// </summary>
        /// <returns>
        /// Returns an error result that provides details of the instrument's error state.
        /// </returns>
        /// <remarks>
        /// The error query result should reflect the status or errors from the instrument based on its error queue or registers.
        /// </remarks>
        ErrorQueryResult ErrorQuery();

        /// <summary>
        /// Resets the instrument to a known state.
        /// </summary>
        /// <remarks>
        /// Typically, this sends a reset command (e.g., "*RST") to the instrument to ensure it is in a known state.
        /// </remarks>
        void Reset();

        /// <summary>
        /// Retrieves the list of supported instrument models compatible with this driver.
        /// </summary>
        /// <returns>
        /// An array of strings, each representing a model of the instrument that this driver can control.
        /// </returns>
        /// <remarks>
        /// The returned models should be consistent with the instrument models supported by the driver.
        /// </remarks>
        String[] GetSupportInstrumentModels();
    }
}
