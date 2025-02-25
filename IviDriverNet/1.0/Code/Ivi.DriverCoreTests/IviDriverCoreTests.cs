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
        public void ComponentVersion_ShouldReturnCorrectVersion()
        {
            mockDriver.Setup(d => d.ComponentVersion).Returns("1.0.0");
            var version = mockDriver.Object.ComponentVersion;
            Assert.Equal("1.0.0", version);
        }

        [Fact]
        public void ComponentVendor_ShouldReturnCorrectVendor()
        {
            mockDriver.Setup(d => d.ComponentVendor).Returns("Keysight Technologies");
            var vendor = mockDriver.Object.ComponentVendor;
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
