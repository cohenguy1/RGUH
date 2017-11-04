using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RGUH
{
    public class MeqoUncertain
    {
        public const double MeqoThreshold = 1.98;

        public static bool ShouldAsk(int candidateRank)
        {
            return candidateRank < MeqoThreshold;
        }
    }
}
