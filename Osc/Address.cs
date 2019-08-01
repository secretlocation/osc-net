using System.Collections.Generic;

namespace Osc
{
    public class Address
    {
        public IList<string> Segments { get; } = new List<string>(); 
    }
}