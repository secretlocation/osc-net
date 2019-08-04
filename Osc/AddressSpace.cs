using System;

namespace Osc
{
    public class AddressSpace
    {
        public Method CreateMethod(Address address, Action<OscValue[]> action)
        {
            return null;
        }

        public Method CreateMethod(string[] addressSegments, Action<OscValue[]> action)
        {
            return null;
        }

        public void Dispatch(Message message)
        {
        }
    }
}