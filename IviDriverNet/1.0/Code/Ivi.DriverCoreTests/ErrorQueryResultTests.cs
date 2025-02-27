using Xunit;
using Ivi.DriverCore;

namespace Ivi.DriverCore.Tests
{
    public class ErrorQueryResultTests
    {
        [Fact]
        public void Code_ShouldReturnCorrectValue()
        {
         
            var errorResult = new ErrorQueryResult(100, "Test Error");

            
            Assert.Equal(100, errorResult.Code);
        }

        [Fact]
        public void Message_ShouldReturnCorrectMessage()
        {
            
            var errorResult = new ErrorQueryResult(100, "Test Error");

            
            Assert.Equal("Test Error", errorResult.Message);
        }

        [Fact]
        public void Constructor_ShouldInitializeProperties()
        {
           
            var errorResult = new ErrorQueryResult(404, "Not Found");

          
            Assert.Equal(404, errorResult.Code);
            Assert.Equal("Not Found", errorResult.Message);
        }

        [Fact]
        public void DefaultConstructor_ShouldInitializeWithDefaultValues()
        {
            
            var errorResult = new ErrorQueryResult();

            
            Assert.Equal(0, errorResult.Code);  // Default value for Int32
            Assert.Null(errorResult.Message);   // Default value for String is null
        }

        [Fact]
        public void ErrorQueryResult_ShouldHandleNegativeErrorCodes()
        {
           
            var errorResult = new ErrorQueryResult(-1, "Negative Error");

            
            Assert.Equal(-1, errorResult.Code);
            Assert.Equal("Negative Error", errorResult.Message);
        }

        [Fact]
        public void ErrorQueryResult_ShouldAllowEmptyErrorMessage()
        {
           
            var errorResult = new ErrorQueryResult(500, "");

       
            Assert.Equal(500, errorResult.Code);
            Assert.Equal("", errorResult.Message);
        }

        [Fact]
        public void ErrorQueryResult_ShouldHandleNullMessageProperly()
        {
           
            var errorResult = new ErrorQueryResult(500, null);

            
            Assert.Equal(500, errorResult.Code);
            Assert.Null(errorResult.Message);
        }
    }
}
