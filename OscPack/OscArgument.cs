using System;
using System.Linq;

namespace OscPack
{
    public abstract class OscArgument
    {
        public abstract char TypeTag { get; }
        public abstract byte[] ToBytes();

        protected byte[] ConvertToBigEndianBytes(int value)
        {
            var bytes = BitConverter.GetBytes(value);

            return BitConverter.IsLittleEndian
                ? bytes.Reverse().ToArray()
                : bytes;
        }

        protected int CalculatePaddedArrayLength(int elementCount)
        {
            if (elementCount < 0)
                throw new ArgumentException("Array cannot have negative number of elements.", nameof(elementCount));

            var remainder = elementCount % 4;
            
            return remainder > 0
                ? elementCount - remainder + 4
                : elementCount;
        }
    }
}