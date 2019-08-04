using System;
using System.Text;

namespace Osc
{
    public class OscString : OscValue
    {
        public override char TypeTag => 's';

        public string Value { get; }

        public OscString(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public override byte[] ToBytes()
        {
            var bytes = new byte[CalculatePaddedArrayLength(Value.Length, true)];

            Encoding.ASCII.GetBytes(Value, 0, Value.Length, bytes, 0);

            return bytes;
        }
    }
}