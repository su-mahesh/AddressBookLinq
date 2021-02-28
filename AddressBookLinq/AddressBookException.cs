using System;
using System.Collections.Generic;
using System.Text;

namespace AddressBookLinq
{
    public class AddressBookException : Exception
    {
        /// <summary>
        /// custom Exception Types
        /// </summary>
        public enum ExceptionType 
        {
            NO_DATA_FOUND,
            INSERTION_ERROR,
            NO_SUCH_SQL_PROCEDURE,
            CONNECTION_FAILED
        }
        ExceptionType exceptionType;
        /// <summary>
        /// Initializes a new instance of the <see cref="AddressBookException "/> class.
        /// </summary>
        /// <param name="exceptionType">Type of the exception.</param>
        /// <param name="message">The message.</param>
        public AddressBookException (ExceptionType exceptionType, string message) : base(message)
        {
            this.exceptionType = exceptionType;
        }
    }
}
