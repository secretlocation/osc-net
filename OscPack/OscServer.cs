using System.Collections.Generic;

namespace OscPack.Server
{
    public class OscServer
    {
        public IList<OscMethod> Methods { get; } = new List<OscMethod>();
    }
}
