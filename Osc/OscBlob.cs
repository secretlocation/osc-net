using System;

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
            var valueLengthBytes = ConvertToBigEndianBytes(Value.Length);
            
            Array.Copy(valueLengthBytes, 0, bytes, 0, 4);
            Array.Copy(Value, 0, bytes, 4, Value.Length);

            return bytes;
        }
    }
}