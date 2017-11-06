using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGUH
{
    public class RandomUncertain
    {
        public static bool ShouldAsk()
        {
            var random = new Random();
            var rand = random.Next(10);
            return rand == 0;
        }
    }
}
