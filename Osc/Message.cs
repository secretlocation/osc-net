using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Osc
{
    public class Message : IContents
    {
        public Message(AddressPattern addressPattern, IEnumerable<OscValue> arguments)
        {
            AddressPattern = addressPattern ?? throw new ArgumentNullException(nameof(addressPattern));
            Arguments = arguments?.ToArray() ?? throw new ArgumentNullException(nameof(arguments));
        }

        public AddressPattern AddressPattern { get; }
        public OscValue[] Arguments { get; }

        public byte[] ToBytes()
        {
            var bytes = AddressPattern.ToBytes();

            bytes = bytes.Concat(GetTypeTags()).ToArray();
            bytes = Arguments.Aggregate(bytes, (current, argument) => current.Concat(argument.ToBytes()).ToArray());

            return bytes;
        }

        private byte[] GetTypeTags()
        {
            var s = $",{ string.Join("", Arguments.Select(a => a.TypeTag)) }" ;
            
            return new OscString(s).ToBytes();
       }

        public static Message FromBytes(byte[] bytes)
        {
            // Todo
            var addressPattern= new AddressPattern("/abc");
            var arguments = new OscValue[] { new OscString("Hello there.")} ;            
            
            return new Message(addressPattern, arguments);
        }
    }
}
