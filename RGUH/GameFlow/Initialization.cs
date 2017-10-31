using RGUH.Enums;
using RGUH.Logic;
using System;
using System.Collections.Generic;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {
        private List<Candidate> GenerateCandidatesForPosition(int positionNumber)
        {
            var positionCandidates = new List<Candidate>();

            int[] candidateRanks = dbHandler.GetCandidateRanksForPosition(positionNumber - 1);

            for (var candidateIndex = 0; candidateIndex < Common.NumOfCandidates; candidateIndex++)
            {
                var newCandidate = new Candidate()
                {
                    CandidateState = CandidateState.Interview,
                    CandidateNumber = candidateIndex,
                    CandidateAccepted = false,
                    CandidateRank = candidateRanks[candidateIndex]
                };

                positionCandidates.Add(newCandidate);
            }

            return positionCandidates;
        }

        public void InitCandidatesForPosition(List<Candidate> positionCandidates, Random randomGenerator)
        {
            List<int> ranks = new List<int>();
            for (var index = 1; index <= Common.NumOfCandidates; index++)
            {
                ranks.Add(index);
            }

            var ranksRemaining = Common.NumOfCandidates;
            int position;

            for (var index = 0; index < Common.NumOfCandidates; index++)
            {
                if (ranksRemaining > 1)
                {
                    position = randomGenerator.Next(1, ranksRemaining + 1) - 1;
                }
                else
                {
                    position = 0;
                }
                positionCandidates[index].CandidateRank = ranks[position];
                positionCandidates[index].CandidateAccepted = false;

                ranks.RemoveAt(position);
                ranksRemaining--;
            }
        }
    }
}