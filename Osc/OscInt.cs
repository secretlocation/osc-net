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
           return ConvertToBigEndianBytes(Value);
        }
    }
}