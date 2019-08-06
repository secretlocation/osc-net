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

            return ConvertEndianess(bytes);
        }

        public static OscFloat FromBytes(ref byte[] bytes)
        {
            return new OscFloat(GetValue(ref bytes));
        }
        
        public static float GetValue(ref byte[] bytes)
        {
            var valueBytes = bytes.Take(4).ToArray();
            valueBytes = ConvertEndianess(valueBytes);
            
            bytes = bytes.Skip(4).ToArray();

            return BitConverter.ToSingle(valueBytes, 0);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OscFloat))
                return false;

            return ((OscFloat) obj).Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}