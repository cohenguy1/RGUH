using RGUH.Enums;

namespace RGUH.Logic
{
    public class Position
    {
        public int PositionNumber;

        public Candidate ChosenCandidate;

        public const string WaiterStr = "waiter";

        public const string WaiterTitleStr = "Waiter ";

        public Position(int positionNumber)
        {
            PositionNumber = positionNumber;
        }

        public string GetJobTitle()
        {
            return WaiterTitleStr + PositionNumber;
        }

        public static string GetJobTitle(int positionNumber)
        {
            return WaiterTitleStr + positionNumber;
        }
    }
}