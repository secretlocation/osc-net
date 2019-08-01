using System.Collections.Generic;

namespace Osc
{
    public class Server
    {
        public IList<Method> Methods { get; } = new List<Method>();
    }
}
