using System;
using System.Collections.Generic;
using System.Linq;

namespace RGUH.Logic
{
    public class DecisionMaker
    {
        private static DecisionMaker _instance = null;


        private static double[] c = new double[Common.NumOfCandidates + 1];

        public static int[] StoppingRule = new int[Common.NumOfCandidates + 1];

        public static DecisionMaker GetInstance()
        {
            if (_instance == null)
            {
                _instance = new DecisionMaker();
            }

            return _instance;
        }

        private DecisionMaker()
        {
            int n = Common.NumOfCandidates;

            c[n - 1] = (n + 1) / 2.0;

            StoppingRule[n] = n;
            StoppingRule[n - 1] = (int)Math.Floor((n - 1 + 1) / ((double)n + 1) * c[n - 1]);

            for (var i = n - 1; i >= 1; i--)
            {
                c[i - 1] = 1 / (double)(i) * (((n + 1) / (double)(i + 1)) * (StoppingRule[i] * (StoppingRule[i] + 1)) / 2.0 + (i - StoppingRule[i]) * c[i]);
                StoppingRule[i - 1] = (int)Math.Floor((i) / ((double)n + 1) * c[i - 1]);
            }

            for (var i = 2; i <= Common.NumOfCandidates; i++)
            {
                StoppingRule[i] = Math.Min(Common.NumOfCandidates, StoppingRule[i] + 1);

                if (i >= 4)
                {
                    StoppingRule[i] = Math.Min(Common.NumOfCandidates, StoppingRule[i] + 1);
                }
            }
        }

        public void DetermineCandidateRank(List<Candidate> candidatesByNow, Candidate newCandidate)
        {
            int newCandidateIndex = InsertNewCandidate(candidatesByNow, newCandidate);

            var accepted = Decide(candidatesByNow, newCandidateIndex);

            newCandidate.CandidateAccepted = accepted;
        }

        public int GetCandidateRelativePosition(List<Candidate> candidatesByNow, Candidate newCandidate)
        {
            int newCandidateIndex = 0;
            foreach (var candidate in candidatesByNow)
            {
                if (candidate.CandidateRank > newCandidate.CandidateRank)
                {
                    break;
                }

                newCandidateIndex++;
            }

            return newCandidateIndex;
        }

        internal Candidate ProcessNextPosition(List<Candidate> positionCandidates)
        {
            var candidatesByNow = new List<Candidate>();
            Candidate acceptedCandidate = null;

            foreach (var newCandidate in positionCandidates)
            {
                // Impossible
                if (candidatesByNow.Contains(newCandidate))
                {
                    Alert.Show("Something went wrong");
                    return null;
                }

                var newCandidateIndex = GetCandidateRelativePosition(candidatesByNow, newCandidate);
                candidatesByNow.Insert(newCandidateIndex, newCandidate);

                var accepted = Decide(candidatesByNow, newCandidateIndex);
                newCandidate.CandidateAccepted = accepted;

                if (accepted)
                {
                    acceptedCandidate = newCandidate;
                    break;
                }
            }

            return acceptedCandidate;
        }

        private int InsertNewCandidate(List<Candidate> candidatesByNow, Candidate newCandidate)
        {
            int newCandidateIndex = 0;
            foreach (var candidate in candidatesByNow)
            {
                if (candidate.CandidateRank > newCandidate.CandidateRank)
                {
                    break;
                }

                newCandidateIndex++;
            }

            candidatesByNow.Insert(newCandidateIndex, newCandidate);
            return newCandidateIndex;
        }

        public bool Decide(List<Candidate> candidatesByNow, int newCandidateIndex)
        {
            return (newCandidateIndex + 1 <= StoppingRule[candidatesByNow.Count]);
        }
    }
}