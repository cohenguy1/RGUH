using RGUH.Enums;

namespace RGUH.Logic
{
    public class ScenarioTurn
    {
        public int TurnNumber;

        public bool Played;

        public Candidate ChosenCandidate;

        public const string WaiterStr = "waiter";

        public const string WaiterTitleStr = "Waiter ";

        public ScenarioTurn(int turnNumber)
        {
            TurnNumber = turnNumber;
            Played = false;
        }

        public string GetTurnTitle()
        {
            return WaiterTitleStr + TurnNumber;
        }

        public static string GetTurnTitle(int turnNumber)
        {
            return WaiterTitleStr + turnNumber;
        }

        public void SetPlayed()
        {
            Played = true;
        }
    }
}