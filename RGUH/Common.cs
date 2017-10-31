using RGUH.Logic;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RGUH
{
    public static class Common
    {
        public const int NumOfPositions = 10;

        public const int NumOfCandidates = 10;

        public const string MovingToNextString = "Moving on to fill the next position:<br /><br />";

        public static int GetTotalPrizePoints(IEnumerable<Position> positions)
        {
            int totalPrizePoints = positions.Where(position => position.ChosenCandidate != null).
                Sum(pos => 110 - pos.ChosenCandidate.CandidateRank * 10);

            // with one decimal precision
            return totalPrizePoints;
        }
    }
}