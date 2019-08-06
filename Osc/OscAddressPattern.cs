using System;
using System.Linq;

namespace Osc
{
    public class OscAddressPattern
    {
        public string[] Segments { get; }

        public OscAddressPattern(string pattern)
        {
            if (pattern == null)
                throw new ArgumentNullException(nameof(pattern));
            
            if (!pattern.StartsWith("/"))
                throw new ArgumentException(nameof(pattern), "An OSC oscAddress pattern must start with '/'.");

            pattern = pattern.Substring(1, pattern.Length - 1);

            Segments = pattern.Split('/');
        }

        public override string ToString()
        {
            return $"/{string.Join("/", Segments)}";
        }

        public byte[] ToBytes()
        {
            return new OscString(ToString()).ToBytes();
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OscAddressPattern))
                return false;

            return ((OscAddressPattern) obj).Segments.SequenceEqual(Segments);
        }

        public override int GetHashCode()
        {
            return Segments.GetHashCode();
        }
    }
}