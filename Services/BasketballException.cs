using System;

namespace services
{
    public class BasketballException : Exception
    {
        public BasketballException() : base()
        {
        }

        public BasketballException(string message) : base(message)
        {
        }

        public BasketballException(string message, Exception cause) : base(message, cause)
        {
        }
    }
}