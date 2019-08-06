using System;
using System.Collections.Generic;
using System.Linq;

namespace Osc
{
    public class OscMessage
    {
        public OscMessage(OscAddressPattern oscAddressPattern, IEnumerable<OscValue> arguments)
        {
            AddressPattern = oscAddressPattern ?? throw new ArgumentNullException(nameof(oscAddressPattern));
            Arguments = arguments?.ToArray() ?? throw new ArgumentNullException(nameof(arguments));
        }

        public OscAddressPattern AddressPattern { get; }
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

        public static OscMessage FromBytes(byte[] bytes)
        {
            var addressPattern = OscString.GetValue(ref bytes);
            var argumentTags = OscString.GetValue(ref bytes).Substring(1);
            var arguments = argumentTags.Select(t => OscValue.FromBytes(t, ref bytes));
          
            return new OscMessage(new OscAddressPattern(addressPattern), arguments);
        }
    }
}
