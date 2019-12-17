using System;
using System.Collections.Generic;

namespace DiplomaSurvive.Models
{
    /// <summary>
    /// Player exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class PlayerException : Exception
    {
        /// <summary>
        /// The unexpected error.
        /// </summary>
        private const string Unexpected = "Unexpected error.";

        /// <summary>
        /// Mapping the error codes to the default error messages.
        /// </summary>
        private static Dictionary<PlayerErrorCode, string> errorCodeToMessage = new Dictionary<PlayerErrorCode, string>
        {
            { PlayerErrorCode.UnrecognizedUser, "Oops, I can't find you... Did you register with this name?" },
            { PlayerErrorCode.AlreadyExists, "Sorryyy, someone already has this name, choose something else for yourself, please:)" }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="PlayerException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public PlayerException(PlayerErrorCode errorCode)
            : base(GetErrorMessage(errorCode))
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public PlayerErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>
        /// The error message text.
        /// </returns>
        private static string GetErrorMessage(PlayerErrorCode errorCode)
        {
            string message = errorCodeToMessage.ContainsKey(errorCode)
                ? errorCodeToMessage[errorCode] : Unexpected;

            return message;
        }
    }
}