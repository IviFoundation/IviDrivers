using Xunit;
using Ivi.DriverCore;

namespace Ivi.DriverCore.Tests
{
    public class ErrorQueryResultTests
    {
        // Test to verify that the Code property returns the correct value
        [Fact]
        public void Code_ShouldReturnCorrectValue()
        {
            // Arrange: Create an ErrorQueryResult instance with a specific error code
            var errorResult = new ErrorQueryResult(100, "Test Error");

            // Assert: Verify that the Code property returns the expected value
            Assert.Equal(100, errorResult.Code);
        }

        // Test to verify that the Message property returns the correct message
        [Fact]
        public void Message_ShouldReturnCorrectMessage()
        {
            // Arrange: Create an ErrorQueryResult instance with a specific error message
            var errorResult = new ErrorQueryResult(100, "Test Error");

            // Assert: Verify that the Message property returns the expected message
            Assert.Equal("Test Error", errorResult.Message);
        }

        // Test to verify that the constructor initializes properties correctly
        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
            // Arrange: Create an instance with specific error code and message
            var errorResult = new ErrorQueryResult(404, "Not Found");

            // Assert: Verify that both properties are initialized correctly
            Assert.Equal(404, errorResult.Code);
            Assert.Equal("Not Found", errorResult.Message);
        }

        // Test to verify the default constructor initializes properties with default values
        [Fact]
        public void DefaultConstructor_ShouldInitializeWithDefaultValues()
        {
            // Act: Create an instance using the default constructor
            var errorResult = new ErrorQueryResult();

            // Assert: Verify that default values are correctly set
            Assert.Equal(0, errorResult.Code);  // Default value for Int32
            Assert.Null(errorResult.Message);   // Default value for String is null
        }

        // Test to verify that the class correctly handles negative error codes
        [Fact]
        public void ErrorQueryResult_ShouldHandleNegativeErrorCodes()
        {
            // Arrange: Create an instance with a negative error code
            var errorResult = new ErrorQueryResult(-1, "Negative Error");

            // Assert: Verify that the negative error code is handled properly
            Assert.Equal(-1, errorResult.Code);
            Assert.Equal("Negative Error", errorResult.Message);
        }

        // Test to verify that an empty error message is allowed
        [Fact]
        public void ErrorQueryResult_ShouldAllowEmptyErrorMessage()
        {
            // Arrange: Create an instance with an empty message
            var errorResult = new ErrorQueryResult(500, "");

            // Assert: Verify that the empty message is correctly stored
            Assert.Equal(500, errorResult.Code);
            Assert.Equal("", errorResult.Message);
        }

        // Test to verify that a null message is handled properly
        [Fact]
        public void ErrorQueryResult_ShouldHandleNullMessageProperly()
        {
            // Arrange: Create an instance with a null message
            var errorResult = new ErrorQueryResult(500, null);

            // Assert: Verify that the null message is correctly assigned
            Assert.Equal(500, errorResult.Code);
            Assert.Null(errorResult.Message);
        }
    }
}
