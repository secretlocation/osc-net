using System;

namespace Osc
{
    public class OscMethod
    {
        private readonly Action<OscMessage> action;
        
        public OscMethod(OscAddress oscAddress, Action<OscMessage> action)
        {
            OscAddress = oscAddress ?? throw new ArgumentNullException(nameof(oscAddress));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }
        
        public OscAddress OscAddress { get; }

        public void Dispatch(OscMessage oscMessage)
        {
            action.Invoke(oscMessage);
        }
    }
}