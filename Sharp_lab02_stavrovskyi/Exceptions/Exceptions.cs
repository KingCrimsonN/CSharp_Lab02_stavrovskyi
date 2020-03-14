using System;


namespace Sharp_lab03_stavrovskyi.Exceptions
{
    class Exceptions : Exception
    {
        public Exceptions(string message) : base(message)
        {
        }
    }

    class NotBornException : Exception
    {
        public NotBornException(string message) : base(message)
        {

        }
    }

    class TooOldException : Exception
    {
        public TooOldException(string message) : base(message)
        {

        }
    }
}