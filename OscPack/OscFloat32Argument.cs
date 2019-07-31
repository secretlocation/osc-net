using System;
using System.Linq;

namespace OscPack
{
    public class OscFloat32Argument : OscArgument
    {
        public override char TypeTag => 'f';

        public float Value { get; }

        public OscFloat32Argument(float value)
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