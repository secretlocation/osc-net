using System.Collections.Generic;

namespace OscPack
{
    public class OscMessage : IOscContents
    {
        public OscAddressPattern AddressPattern { get; }
        public OscArgument[] Arguments { get; }
    }
}
