using System;
using System.Collections.Generic;

namespace OscPack
{
    public class OscBundle : IOscContents
    {
        public DateTimeOffset Timestamp { get; set; }
        public IList<IOscContents> Elements { get; } = new List<IOscContents>();
    }
}
