namespace OscPack
{
    public class OscInteger32Argument : OscArgument
    {
        public override char TypeTag => 'i';

        public int Value { get; }

        public OscInteger32Argument(int value)
        {
            Value = value;
        }
        
        public override byte[] ToBytes()
        {
           return ConvertToBigEndianBytes(Value);
        }
    }
}