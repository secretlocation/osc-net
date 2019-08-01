using System;

namespace Osc
{
    public class AddressSpace
    {
        public Method CreateMethod(Address address, Action<Argument[]> action)
        {
            return null;
        }

        public Method CreateMethod(string[] addressSegments, Action<Argument[]> action)
        {
            return null;
        }

        public void Dispatch(Message message)
        {
        }
    }
}