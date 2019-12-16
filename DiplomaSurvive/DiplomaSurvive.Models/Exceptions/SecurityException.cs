using System;
using System.Collections.Generic;

namespace DiplomaSurvive.Models
{
    /// <summary>
    /// Security exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class SecurityException : Exception
    {
        /// <summary>
        /// The unexpected error.
        /// </summary>
        private const string Unexpected = "Unexpected error.";

        /// <summary>
        /// Mapping the error codes to the default error messages.
        /// </summary>
        private static Dictionary<SecurityErrorCode, string> errorCodeToMessage = new Dictionary<SecurityErrorCode, string>
        {
            { SecurityErrorCode.UnrecognizedUser, "Something strange, I can't recognize you, try to reauthorize, please." }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="SecurityException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public SecurityException(SecurityErrorCode errorCode)
            : base(GetErrorMessage(errorCode))
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public SecurityErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>
        /// The error message text.
        /// </returns>
        private static string GetErrorMessage(SecurityErrorCode errorCode)
        {
            string message = errorCodeToMessage.ContainsKey(errorCode)
                ? errorCodeToMessage[errorCode] : Unexpected;

            return message;
        }
    }
}