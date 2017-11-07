using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RGUH
{
    public class RandomUncertain
    {
        public static bool ShouldAsk(Random randSeed)
        {
            Thread.Sleep(50);
            var rand = randSeed.Next(10);
            return rand == 0;
        }
    }
}
