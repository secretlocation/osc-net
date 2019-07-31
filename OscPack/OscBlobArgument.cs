using System;

namespace OscPack
{
    public class OscBlobArgument : OscArgument
    {
        public override char TypeTag => 'b';

        public byte[] Value { get; }

        public OscBlobArgument(byte[] value)
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