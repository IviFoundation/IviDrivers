using Xunit;
using Moq;
using Ivi.DriverCore;

namespace Ivi.DriverCore.Tests
{
    public class IviDriverCoreTests
    {
        private Mock<IIviDriverCore> mockDriver;

        public IviDriverCoreTests()
        {
            mockDriver = new Mock<IIviDriverCore>();
        }

        [Fact]
        public void DriverVersion_ShouldReturnCorrectVersion()
        {
            mockDriver.Setup(d => d.DriverVersion).Returns("1.0.0");
            var version = mockDriver.Object.DriverVersion;
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void DriverVendor_ShouldReturnCorrectVendor()
        {
            mockDriver.Setup(d => d.DriverVendor).Returns("Keysight Technologies");
            var vendor = mockDriver.Object.DriverVendor;
            Assert.Equal("Keysight Technologies", vendor);
        }

        [Fact]
        public void InstrumentManufacturer_ShouldReturnCorrectManufacturer()
        {
            mockDriver.Setup(d => d.InstrumentManufacturer).Returns("Keysight Technologies");
            var manufacturer = mockDriver.Object.InstrumentManufacturer;
            Assert.Equal("Keysight Technologies", manufacturer);
        }

        [Fact]
        public void InstrumentModel_ShouldReturnCorrectModel()
        {
            mockDriver.Setup(d => d.InstrumentModel).Returns("34410A");
            var model = mockDriver.Object.InstrumentModel;
            Assert.Equal("34410A", model);
        }

        [Fact]
        public void Simulate_ShouldReturnCorrectSimulationStatus()
        {
            mockDriver.Setup(d => d.Simulate).Returns(true);
            var simulateStatus = mockDriver.Object.Simulate;
            Assert.True(simulateStatus);
        }

        [Fact]
        public void Reset_ShouldInvokeResetCommand()
        {
            mockDriver.Setup(d => d.Reset());
            mockDriver.Object.Reset();
            mockDriver.Verify(d => d.Reset(), Times.Once);
        }
    }
}
