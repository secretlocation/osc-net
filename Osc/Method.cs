using System;

namespace Osc
{
    public class Method
    {
        private readonly Action<Message> action;
        
        public Method(Address address, Action<Message> action)
        {
            Address = address ?? throw new ArgumentNullException(nameof(address));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
        }
        
        public Address Address { get; }

        public void Dispatch(Message message)
        {
            action.Invoke(message);
        }
    }
}