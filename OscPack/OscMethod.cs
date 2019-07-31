using System;

namespace OscPack
{
    public class OscMethod
    {
        public OscAddress Address { get; set; }
        public Action<OscMessage> Action { get; set; }

        public void Dispatch(OscMessage message)
        {
            Action.Invoke(message);
        }
    }
}