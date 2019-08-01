using System;
using System.Text;

namespace Osc
{
    public class StringArgument : Argument
    {
        public override char TypeTag => 's';

        public string Value { get; }

        public StringArgument(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public override byte[] ToBytes()
        {
            var bytes = new byte[CalculatePaddedArrayLength(Value.Length)];

            Encoding.ASCII.GetBytes(Value, 0, Value.Length, bytes, 0);

            return bytes;
        }
    }
}