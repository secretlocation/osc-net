using System;
using System.Linq;

namespace Osc
{
    public class OscBlob : OscValue
    {
        public override char TypeTag => 'b';

        public byte[] Value { get; }

        public OscBlob(byte[] value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public override byte[] ToBytes()
        {
            var bytes = new byte[4 + CalculatePaddedArrayLength(Value.Length)];
            var valueLengthBytes = new OscInt(Value.Length).ToBytes();

            Array.Copy(valueLengthBytes, 0, bytes, 0, 4);
            Array.Copy(Value, 0, bytes, 4, Value.Length);

            return bytes;
        }
        
        public static OscBlob FromBytes(ref byte[] bytes)
        {
            return new OscBlob(GetValue(ref bytes));
        }
        
        public static byte[] GetValue(ref byte[] bytes)
        {
            var length = OscInt.GetValue(ref bytes);
            var valueBytes = bytes.Take(length).ToArray();
            var paddedLength = CalculatePaddedArrayLength(length, false);
            
            bytes = bytes.Skip(paddedLength).ToArray();

            return valueBytes;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OscBlob))
                return false;

            return ((OscBlob) obj).Value.SequenceEqual(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}