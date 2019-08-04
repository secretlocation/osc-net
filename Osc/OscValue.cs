using System;
using System.Linq;

namespace Osc
{
    public abstract class OscValue
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

        protected int CalculatePaddedArrayLength(int elementCount, bool nullTerminated = false)
        {
            if (elementCount < 0)
                throw new ArgumentException("Array cannot have negative number of elements.", nameof(elementCount));

            var remainder = elementCount % 4;
            
            return remainder > 0
                ? elementCount - remainder + 4
                : elementCount + (nullTerminated ? 4 : 0);
        }
    }
}