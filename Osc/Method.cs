using System;

namespace Osc
{
    public class Method
    {
        public Address Address { get; set; }
        public Action<Message> Action { get; set; }

        public void Dispatch(Message message)
        {
            Action.Invoke(message);
        }
    }
}