using System;

namespace OscPack
{
    public class OscAddressSpace
    {
        public OscMethod CreateMethod(OscAddress address, Action<OscArgument[]> action)
        {
            return null;
        }

        public OscMethod CreateMethod(string[] addressSegments, Action<OscArgument[]> action)
        {
            return null;
        }

        public void Dispatch(OscMessage message)
        {
        }
    }
}