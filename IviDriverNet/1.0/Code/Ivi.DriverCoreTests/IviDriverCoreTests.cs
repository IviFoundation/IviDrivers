using Xunit;
using Moq;
using Ivi.DriverCore;
using System;

namespace Ivi.DriverCore.Tests
{
    public class IviDriverCoreTests
    {
        private Mock<IIviDriverCore> mockDriver;

        // Constructor to initialize mock object
        public IviDriverCoreTests()
        {
            mockDriver = new Mock<IIviDriverCore>();
        }

        // Test to verify that the Initialize method is called with correct parameters
        [Fact]
        public void Initialize_ShouldBeCalledWithCorrectParameters()
        {
            mockDriver.Object.Initialize("GPIB0::22::INSTR", true, false, false);
            mockDriver.Verify(d => d.Initialize("GPIB0::22::INSTR", true, false, false), Times.Once);

            mockDriver.Object.Initialize("USB0::0x1234::0x5678::INSTR", false, true, true);
            mockDriver.Verify(d => d.Initialize("USB0::0x1234::0x5678::INSTR", false, true, true), Times.Once);
        }

        // Test to verify the component version
        [Fact]
        public void ComponentVersion_ShouldReturnCorrectVersion()
        {
            mockDriver.Setup(d => d.ComponentVersion).Returns("1.0.0");
            var version = mockDriver.Object.ComponentVersion;
            Assert.Equal("1.0.0", version);
        }

        // Test to verify the component vendor
        [Fact]
        public void ComponentVendor_ShouldReturnCorrectVendor()
        {
            mockDriver.Setup(d => d.ComponentVendor).Returns("Keysight Technologies");
            var vendor = mockDriver.Object.ComponentVendor;
            Assert.Equal("Keysight Technologies", vendor);
        }

        // Test to verify that ErrorQuery returns the correct error message
        [Fact]
        public void ErrorQuery_ShouldReturnCorrectErrorMessage()
        {
            var expectedError = new ErrorQueryResult(113, "Undefined header");

            mockDriver.Setup(d => d.ErrorQuery()).Returns(expectedError);

            var errorResult = mockDriver.Object.ErrorQuery();

            Assert.Equal(113, errorResult.Code);
            Assert.Equal("Undefined header", errorResult.Message);
        }

        // Test to verify the instrument manufacturer
        [Fact]
        public void InstrumentManufacturer_ShouldReturnCorrectManufacturer()
        {
            mockDriver.Setup(d => d.InstrumentManufacturer).Returns("Keysight Technologies");
            var manufacturer = mockDriver.Object.InstrumentManufacturer;
            Assert.Equal("Keysight Technologies", manufacturer);
        }

        // Test to verify the instrument model
        [Fact]
        public void InstrumentModel_ShouldReturnCorrectModel()
        {
            mockDriver.Setup(d => d.InstrumentModel).Returns("34410A");
            var model = mockDriver.Object.InstrumentModel;
            Assert.Equal("34410A", model);
        }

        // Test to verify the QueryInstrumentStatus property returns the correct status
        [Fact]
        public void QueryInstrumentStatus_ShouldReturnCorrectStatus()
        {
            mockDriver.Setup(d => d.QueryInstrumentStatus).Returns(true);

            var status = mockDriver.Object.QueryInstrumentStatus;

            Assert.True(status);
        }

        // Test to verify that the Reset method is called
        [Fact]
        public void Reset_ShouldInvokeResetCommand()
        {
            mockDriver.Setup(d => d.Reset());
            mockDriver.Object.Reset();
            mockDriver.Verify(d => d.Reset(), Times.Once);
        }

        // Test to verify that the Simulate property returns the correct status
        [Fact]
        public void Simulate_ShouldReturnCorrectSimulationStatus()
        {
            mockDriver.Setup(d => d.Simulate).Returns(true);
            var simulateStatus = mockDriver.Object.Simulate;
            Assert.True(simulateStatus);
        }

        // Test to verify that GetSupportInstrumentModels returns the correct list of models
        [Fact]
        public void GetSupportInstrumentModels_ShouldReturnCorrectList()
        {
            var expectedModels = new string[] { "34410A", "34411A", "34465A" };

            mockDriver.Setup(d => d.GetSupportInstrumentModels()).Returns(expectedModels);

            var models = mockDriver.Object.GetSupportInstrumentModels();

            Assert.Equal(expectedModels, models);
        }
    }
}
