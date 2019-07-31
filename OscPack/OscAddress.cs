using System.Collections.Generic;

namespace OscPack
{
    public class OscAddress
    {
        public IList<string> Segments { get; } = new List<string>(); 
    }
}