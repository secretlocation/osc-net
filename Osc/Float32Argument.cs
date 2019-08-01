using System;
using System.Linq;

namespace Osc
{
    public class Float32Argument : Argument
    {
        public override char TypeTag => 'f';

        public float Value { get; }

        public Float32Argument(float value)
        {
            Value = value;
        }
        
        public override byte[] ToBytes()
        {
            var bytes = BitConverter.GetBytes(Value);

            return BitConverter.IsLittleEndian
                ? bytes.Reverse().ToArray()
                : bytes;
        }
    }
}