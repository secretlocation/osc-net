using System;

namespace Osc
{
    public class OscException : Exception
    {
        public OscException(string message) : base(message)
        {
        }
    }
}