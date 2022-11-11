using System;


namespace StudentRegistryApp.Helper
{
    [Serializable] // ASK: Why Serializable?
    public class KnownException : Exception
    {
        public KnownException(string message)
            : base(message)
        {
        }
    }
}
