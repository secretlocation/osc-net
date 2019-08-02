using System;

namespace Osc
{
    public class AddressPattern
    {
        public string[] Segments { get; }

        public AddressPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            
            if (!pattern.StartsWith("/"))
                throw new ArgumentException(nameof(pattern), "An OSC address pattern must start with '/'.");

            pattern = pattern.Substring(1, pattern.Length - 1);

            Segments = pattern.Split('/');
        }
    }
}