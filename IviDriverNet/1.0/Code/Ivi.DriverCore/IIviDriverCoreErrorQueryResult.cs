namespace Ivi.DriverCore
{
    /// <summary>
    /// Result of an error query operation.
    /// </summary>
    public struct ErrorQueryResult : IEquatable<ErrorQueryResult> 
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="code">Instrument error code.</param>
        /// <param name="message">Instrument error message.</param>
        public ErrorQueryResult(int code, string message) 
        {
            Code = code;
            Message = message;
        }

        /// <summary>
        /// Instrument error code.
        /// </summary>
        public int Code { get; } 

        /// <summary>
        /// Instrument error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Compares two <see cref="ErrorQueryResult"/> instances for equality.
        /// </summary>
        /// <param name="result">The <see cref="ErrorQueryResult"/> instance to compare with the current instance.</param>
        /// <returns>true if the two instances represent the same result; otherwise, false.</returns>
        public bool Equals(ErrorQueryResult result) => result.Code == Code && result.Message == Message;

        /// <summary>
        /// Overrides Equals for object.
        /// </summary>
        public override bool Equals(object? obj) => obj is ErrorQueryResult result && Equals(result);

        /// <summary>
        /// Returns the hash code for the result.
        /// </summary>
        public override int GetHashCode()
        {
            return Code.GetHashCode() ^ Message.GetHashCode();
        }


        /// <summary>
        /// Determines whether two <see cref="ErrorQueryResult"/> instances have the same value.
        /// </summary>
        /// <param name="left">A <see cref="ErrorQueryResult"/> instance to compare with right.</param> 
        /// <param name="right">A <see cref="ErrorQueryResult"/> instance to compare with left.</param> 
        /// <returns>true if the <see cref="ErrorQueryResult"/> instances are equivalent; otherwise, false.</returns>
        public static bool operator ==(ErrorQueryResult left, ErrorQueryResult right) 
        {
            return left.Equals(right); 
        }

        /// <summary>
        /// Determines whether two <see cref="ErrorQueryResult"/> instances do not have the same value.
        /// </summary>
        /// <param name="left">A <see cref="ErrorQueryResult"/> instance to compare with right.</param> 
        /// <param name="right">A <see cref="ErrorQueryResult"/> instance to compare with left.</param> 
        /// <returns>true if the two <see cref="ErrorQueryResult"/> instances are not equal; otherwise, false.</returns>
        public static bool operator !=(ErrorQueryResult left, ErrorQueryResult right) 
        {
            return !(left == right); 
        }
    }
}