using RGUH.Logic;
using System;
using System.Collections.Generic;

namespace RGUH
{
    public class EsbUncertain
    {
        public const double Alpha = 0.312;

        public const double EsbThreshold = 2.96;

        public static bool ShouldAsk(int[] accepted, int stoppingDecision)
        {
            double exponentialSmoothing = accepted[0];
            for (int i = 1; i <= stoppingDecision; i++)
            {
                exponentialSmoothing = Alpha * accepted[i] + (1 - Alpha) * exponentialSmoothing;
            }

            var ask = exponentialSmoothing < EsbThreshold;
            return ask;
        }
    }
}
