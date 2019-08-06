using System;
using System.Linq;

namespace Osc
{
    public class OscInt : OscValue
    {
        public override char TypeTag => 'i';

        public int Value { get; }

        public OscInt(int value)
        {
            Value = value;
        }
        
        public override byte[] ToBytes()
        {
            var bytes = BitConverter.GetBytes(Value);
            
           return ConvertEndianess(bytes);
        }
        
        public static OscInt FromBytes(ref byte[] bytes)
        {
            return new OscInt(GetValue(ref bytes));
        }
        
        public static int GetValue(ref byte[] bytes)
        {
            var valueBytes = bytes.Take(4).ToArray();
            valueBytes = ConvertEndianess(valueBytes);
            
            bytes = bytes.Skip(4).ToArray();

            return BitConverter.ToInt32(valueBytes, 0);
        }

        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OscInt))
              return false;

            return ((OscInt) obj).Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}