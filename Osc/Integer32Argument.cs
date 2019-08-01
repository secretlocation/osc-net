namespace Osc
{
    public class Integer32Argument : Argument
    {
        public override char TypeTag => 'i';

        public int Value { get; }

        public Integer32Argument(int value)
        {
            Value = value;
        }
        
        public override byte[] ToBytes()
        {
           return ConvertToBigEndianBytes(Value);
        }
    }
}