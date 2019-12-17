using System;
using System.Collections.Generic;

namespace DiplomaSurvive.Models
{
    /// <summary>
    /// Event exception
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class EventException : Exception
    {
        /// <summary>
        /// The unexpected error.
        /// </summary>
        private const string Unexpected = "Unexpected error.";

        /// <summary>
        /// Mapping the error codes to the default error messages.
        /// </summary>
        private static Dictionary<EventErrorCode, string> errorCodeToMessage = new Dictionary<EventErrorCode, string>
        {
            { EventErrorCode.NoEvents, "There is no competitions for you now:( See you soon!" }
        };

        /// <summary>
        /// Initializes a new instance of the <see cref="EventException"/> class.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        public EventException(EventErrorCode errorCode)
            : base(GetErrorMessage(errorCode))
        {
            this.ErrorCode = errorCode;
        }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public EventErrorCode ErrorCode { get; private set; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <returns>
        /// The error message text.
        /// </returns>
        private static string GetErrorMessage(EventErrorCode errorCode)
        {
            string message = errorCodeToMessage.ContainsKey(errorCode)
                ? errorCodeToMessage[errorCode] : Unexpected;

            return message;
        }
    }
}