using RGUH.Enums;
using RGUH.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {
        public GameMode GameMode
        {
            get
            {
                if (Session[SessionMap.GameModeStr] == null)
                {
                    return GameMode.Initial;
                }
                else
                {
                    return (GameMode)Session[SessionMap.GameModeStr];
                }
            }
            set { Session[SessionMap.GameModeStr] = value; }
        }

        public string UserId
        {
            get { var userId = Session[SessionMap.UserIdStr] == null ? string.Empty : (string)Session[SessionMap.UserIdStr]; return userId; }
            set { Session[SessionMap.UserIdStr] = value; }
        }

        public bool AlreadyAskedForRating
        {
            get { return (bool)Session[SessionMap.AlreadyAskedForRatingStr]; }
            set { Session[SessionMap.AlreadyAskedForRatingStr] = value; }
        }

        public bool AskForRating
        {
            get { return (bool)Session[SessionMap.AskForRatingStr]; }
            set { Session[SessionMap.AskForRatingStr] = value; }
        }

        public int CurrentTurnNumber
        {
            get { return (int)Session[SessionMap.CurrentTurnNumberStr]; }
            set { Session[SessionMap.CurrentTurnNumberStr] = value; }
        }

        public ScenarioTurn[] ScenarioTurns
        {
            get { return (ScenarioTurn[])Session[SessionMap.ScenarioTurnsStr]; }
            set { Session[SessionMap.ScenarioTurnsStr] = value; }
        }

        public int[] AcceptedCandidates
        {
            get { return (int[])Session[SessionMap.AcceptedCandidatesStr]; }
            set { Session[SessionMap.AcceptedCandidatesStr] = value; }
        }

        public TurnStatus CurrentTurnStatus
        {
            get { return (TurnStatus)Session[SessionMap.CurrentTurnStatusStr]; }
            set { Session[SessionMap.CurrentTurnStatusStr] = value; }
        }

        public List<Candidate> PositionCandidates
        {
            get { return (List<Candidate>)Session[SessionMap.PositionCandidiatesStr]; }
            set { Session[SessionMap.PositionCandidiatesStr] = value; }
        }

        public Candidate CurrentCandidate
        {
            get { return (Candidate)Session[SessionMap.CurrentCandidateStr]; }
            set { Session[SessionMap.CurrentCandidateStr] = value; }
        }

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[SessionMap.AskPositionStr]; }
            set { Session[SessionMap.AskPositionStr] = value; }
        }

        public int RandomHuristicAskPosition
        {
            get { return (int)Session[SessionMap.RandomHuristicAskPositionStr]; }
            set { Session[SessionMap.RandomHuristicAskPositionStr] = value; }
        }

        public Stopwatch InstructionsStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.InstructionsStopwatchStr]; }
            set { Session[SessionMap.InstructionsStopwatchStr] = value; }
        }

        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }

        public int? VectorNum
        {
            get { return (int?)Session[SessionMap.VectorNumStr]; }
            set { Session[SessionMap.VectorNumStr] = value; }
        }
    }
}