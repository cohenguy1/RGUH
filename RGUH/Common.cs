using RGUH.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGUH
{
    public static class Common
    {
        public const int NumOfTurns = 20;

        public const int NumOfCandidates = 10;

        public const int NumOfTurnsInTable = 10;

        public const string MovingToNextString = "Moving on to fill the next position:<br /><br />";

        public static double GetAveragePrizePoints(IEnumerable<ScenarioTurn> turns)
        {
            double averagePrizePoints = turns.Where(turn => turn.Played).
                Average(tur => 110 - tur.ChosenCandidate.CandidateRank * 10);

            // with one decimal precision
            return averagePrizePoints;
        }
    }
}