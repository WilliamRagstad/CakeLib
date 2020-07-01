using System;
using System.Collections.Generic;
using System.Text;

namespace CakeLang.Cake
{
    /// <summary>
    /// Holds a universally unique identifier.
    /// </summary>
    public class UUID
    {
        public UUID(long mostSigBits, long leastSigBits)
        {
            MostSigBits = mostSigBits;
            LeastSigBits = leastSigBits;
        }

        public long MostSigBits { get; }
        public long LeastSigBits { get; }

        public static UUID RandomUUID() => new UUID((long)new Random().NextDouble(), (long)new Random().NextDouble());

    }
}
