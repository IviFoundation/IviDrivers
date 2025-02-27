using Xunit;
using Moq;
using Ivi.DriverCore;
using System;

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
        public void Initialize_ShouldBeCalledWithCorrectParameters()
        {
            mockDriver.Object.Initialize("GPIB0::22::INSTR", true, false, false);
            mockDriver.Verify(d => d.Initialize("GPIB0::22::INSTR", true, false, false), Times.Once);

            mockDriver.Object.Initialize("USB0::0x1234::0x5678::INSTR", false, true, true);
            mockDriver.Verify(d => d.Initialize("USB0::0x1234::0x5678::INSTR", false, true, true), Times.Once);
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
        public void ErrorQuery_ShouldReturnCorrectErrorMessage()
        {
            var expectedError = new ErrorQueryResult(113, "Undefined header");

            mockDriver.Setup(d => d.ErrorQuery()).Returns(expectedError);

            var errorResult = mockDriver.Object.ErrorQuery();

            Assert.Equal(113, errorResult.Code);
            Assert.Equal("Undefined header", errorResult.Message);
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
        public void QueryInstrumentStatus_ShouldReturnCorrectStatus()
        {
            mockDriver.Setup(d => d.QueryInstrumentStatus).Returns(true);

            var status = mockDriver.Object.QueryInstrumentStatus;

            Assert.True(status);
        }

      
        [Fact]
        public void Reset_ShouldInvokeResetCommand()
        {
            mockDriver.Setup(d => d.Reset());
            mockDriver.Object.Reset();
            mockDriver.Verify(d => d.Reset(), Times.Once);
        }

     
        [Fact]
        public void Simulate_ShouldReturnCorrectSimulationStatus()
        {
            mockDriver.Setup(d => d.Simulate).Returns(true);
            var simulateStatus = mockDriver.Object.Simulate;
            Assert.True(simulateStatus);
        }

       
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
