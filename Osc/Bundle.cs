using System;
using System.Collections.Generic;

namespace Osc
{
    public class Bundle : IContents
    {
        public DateTimeOffset Timestamp { get; set; }
        public IList<IContents> Elements { get; } = new List<IContents>();
    }
}
