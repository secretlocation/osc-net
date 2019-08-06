using System;
using System.Linq;
using System.Text;

namespace Osc
{
    public class OscString : OscValue
    {
        public override char TypeTag => 's';

        public string Value { get; }

        public OscString(string value)
        {
            Value = value ?? throw new ArgumentNullException(nameof(value));
        }
        
        public override byte[] ToBytes()
        {
            var bytes = new byte[CalculatePaddedArrayLength(Value.Length, true)];

            Encoding.ASCII.GetBytes(Value, 0, Value.Length, bytes, 0);

            return bytes;
        }

        public static OscString FromBytes(ref byte[] bytes)
        {
            return new OscString(GetValue(ref bytes));
        }

        public static string GetValue(ref byte[] bytes)
        {
            var valueBytes = bytes.TakeWhile(b => b != '\0').ToArray();

            var nullCount = valueBytes.Length % 4 > 0
                ? 4 - valueBytes.Length % 4
                : 4;

            bytes = bytes.Skip(valueBytes.Length + nullCount).ToArray();

            return Encoding.ASCII.GetString(valueBytes);
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OscString))
                return false;

            return ((OscString) obj).Value.Equals(Value);
        }

        public override int GetHashCode()
        {
            return Value.GetHashCode();
        }
    }
}