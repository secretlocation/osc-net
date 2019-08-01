using System.Collections.Generic;

namespace Osc
{
    public class Message : IContents
    {
        public AddressPattern AddressPattern { get; }
        public Argument[] Arguments { get; }
    }
}
