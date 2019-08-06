using System;
using System.Collections.Generic;
using System.Linq;

namespace Osc
{
    public class OscAddress
    {
        public string[] Segments { get; }
        
        public OscAddress(string address)
        {
            if (address == null)
                throw new ArgumentNullException(nameof(address));
            
            if (!address.StartsWith("/"))
                throw new ArgumentException(nameof(address), "An OSC oscAddress must start with '/'.");

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

                foreach (var illegalChar in IllegalChars)
                {
                    if (Segments[i].Contains(illegalChar))
                        messageList.Add($"{i}: Segment may not contain '{illegalChar}'.");
                }
            }

            messages = messageList.ToArray();
            
            return messages.Length == 0;
        }
        
        public override bool Equals(object obj)
        {
            if (obj == null || obj.GetType() != typeof(OscAddress))
                return false;

            return ((OscAddress) obj).Segments.SequenceEqual(Segments);
        }

        public override int GetHashCode()
        {
            return Segments.GetHashCode();
        }
        
        private static readonly char[] IllegalChars = new char[] { ' ', '#', '*', ',', '/', '?', '[', ']', '{', '}' };
    }
}