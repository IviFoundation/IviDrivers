using Xunit;
using Ivi.DriverCore;

namespace Ivi.DriverCore.Tests
{
    public class ErrorQueryResultTests
    {
        [Fact]
        public void Code_ShouldReturnCorrectValue()
        {
            // Arrange
            var errorResult = new ErrorQueryResult(100, "Test Error");

            // Assert
            Assert.Equal(100, errorResult.Code);
        }

        [Fact]
        public void Message_ShouldReturnCorrectMessage()
        {
            // Arrange
            var errorResult = new ErrorQueryResult(100, "Test Error");

            // Assert
            Assert.Equal("Test Error", errorResult.Message);
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange
            var errorResult = new ErrorQueryResult(404, "Not Found");

            // Assert
            Assert.Equal(404, errorResult.Code);
            Assert.Equal("Not Found", errorResult.Message);
        }

        [Fact]
        public void DefaultConstructor_ShouldNotThrow()
        {
            // Act
            var errorResult = new ErrorQueryResult();

            // Assert
            Assert.Equal(0, errorResult.Code);  // Default value for Int32
            Assert.Null(errorResult.Message);   // Default value for String is null
        }
    }
}
