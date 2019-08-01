using System;
using System.Collections.Generic;
using System.Linq;

namespace Osc
{
    public class Address
    {
        public string[] Segments { get; }
        
        public Address(string address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            
            if (!address.StartsWith("/"))
                throw new ArgumentException(nameof(address), "An OSC address must start with '/'.");

            address = address.Substring(1, address.Length - 1);

            Segments = address.Split('/');
        }

        public bool IsValid()
        {
            return IsValid(out var messages);
        }

        public bool IsValid(out string[] messages)
        {
            var messageList = new List<string>();
            
            for (int i = 0; i < Segments.Length; i++)
            {
                if (Segments[i] == string.Empty)
                    messageList.Add($"{i}: Segment cannot be empty.");

                foreach (var illegalChar in illegalChars)
                {
                    if (Segments[i].Contains(illegalChar))
                        messageList.Add($"{i}: Segment may not contain '{illegalChar}'.");
                }
            }

            messages = messageList.ToArray();
            
            return messages.Length == 0;
        }
        
        private static readonly char[] illegalChars = new char[] { ' ', '#', '*', ',', '/', '?', '[', ']', '{', '}' };
    }
}