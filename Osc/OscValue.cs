using System;
using System.Linq;

namespace Osc
{
    public abstract class OscValue
    {
        public abstract char TypeTag { get; }
        public abstract byte[] ToBytes();
        
        public static OscValue FromBytes(char tag, ref byte[] bytes)
        {
            switch (tag)
            {
                case 'i': return OscInt.FromBytes(ref bytes);
                case 'f': return OscFloat.FromBytes(ref bytes);
                case 's': return OscString.FromBytes(ref bytes);
                case 'b': return OscBlob.FromBytes(ref bytes);
                default: throw new OscException($"Unable to read OSC argument with tag '{tag}'.");
            }
        }

        protected static byte[] ConvertEndianess(byte[] bytes)
        {
            return BitConverter.IsLittleEndian
                ? bytes.Reverse().ToArray()
                : bytes;
        }

        protected static int CalculatePaddedArrayLength(int elementCount, bool nullTerminated = false)
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