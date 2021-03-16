using System;

namespace Tasks.Business
{

    public class ValidationException : Exception
    {
        public ValidationException(string message) : base(message)
        {
        }
        public ValidationException(string message, Exception innerException) : base(message, innerException)
        {
        }

        public static Func<TArg, ValidationException> WithMessage<TArg>(string message) {
            return (arg) => new ValidationException(message);
        }
    }

}