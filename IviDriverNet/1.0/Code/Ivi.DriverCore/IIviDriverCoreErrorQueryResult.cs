namespace Ivi.DriverCore
{
    /// <summary>
    /// Result of an error query operation.
    /// </summary>
    public struct ErrorQueryResult
	{
		private Int32 _code;
		private String _message;

		/// <summary>
		/// Constructor.
		/// </summary>
		/// <param name="code">Instrument error code.</param>
		/// <param name="message">Instrument error message.</param>
		public ErrorQueryResult(Int32 code, String message)
		{
			_code = code;
			_message = message;
		}

		/// <summary>
		/// Instrument error code.
		/// </summary>
		public Int32 Code
		{
			get { return _code; }
		}

		/// <summary>
		/// Instrument error message.
		/// </summary>
		public String Message
		{
			get { return _message; }
		}

		/// <summary>
		/// Compares two <see cref="T:ErrorQueryResult"></see> instances for equality.
		/// </summary>
		/// <param name="obj">The <see cref="T:ErrorQueryResult"></see> instance to compare with the current instance.</param>
		/// <returns>true if the two instances represent the same result; otherwise, false.</returns>
		public override bool Equals(object? obj)
		{
			if (!(obj is ErrorQueryResult))
				return false;

			ErrorQueryResult result = (ErrorQueryResult)obj;

			return this.Code == result.Code &&
					this.Message == result.Message;
		}

		/// <summary>
		/// Returns the hash code for the result.
		/// </summary>
		/// <returns>An <see cref="T:System.Int32"></see> containing the hash value generated for this result.</returns>
		public override int GetHashCode()
		{
			return this.Code.GetHashCode() ^
					this.Message.GetHashCode();
		}

		/// <summary>
		/// Determines whether two <see cref="T:ErrorQueryResult"></see> instances have the same value.
		/// </summary>
		/// <param name="result1">A <see cref="T:ErrorQueryResult"></see> instance to compare with result2.</param>
		/// <param name="result2">A <see cref="T:ErrorQueryResult"></see> instance to compare with result1.</param>
		/// <returns>true if the <see cref="T:ErrorQueryResult"></see> instances are equivalent; otherwise, false.</returns>
		public static bool operator ==(ErrorQueryResult result1, ErrorQueryResult result2)
		{
			return result1.Equals(result2);
		}

		/// <summary>
		/// Determines whether two <see cref="T:ErrorQueryResult"></see> instances do not have the same value.
		/// </summary>
		/// <param name="result1">A <see cref="T:ErrorQueryResult"></see> instance to compare with result2.</param>
		/// <param name="result2">A <see cref="T:ErrorQueryResult"></see> instance to compare with result1.</param>
		/// <returns>true if the two <see cref="T:ErrorQueryResult"></see> instances are not equal; otherwise, false. If either parameter is null, this method returns true.</returns>
		public static bool operator !=(ErrorQueryResult result1, ErrorQueryResult result2)
		{
			return !(result1 == result2);
		}
	}
}

