using System;
using System.Linq;

namespace Osc
{
    public class OscFloat : OscValue
    {
        public override char TypeTag => 'f';

        public float Value { get; }

        public OscFloat(float value)
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