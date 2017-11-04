using RGUH.Enums;
using RGUH.Logic;
using System.Collections.Generic;
using System.Diagnostics;

namespace RGUH
{
    public partial class Default : System.Web.UI.Page
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

        public int NumOfWrongQuizAnswers
        {
            get { return (int)Session[SessionMap.NumOfWrongQuizAnswersStr]; }
            set { Session[SessionMap.NumOfWrongQuizAnswersStr] = value; }
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

        public AskPositionHeuristic AskPosition
        {
            get { return (AskPositionHeuristic)Session[SessionMap.AskPositionStr]; }
            set { Session[SessionMap.AskPositionStr] = value; }
        }

        public Stopwatch GameStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.GameStopwatchStr]; }
            set { Session[SessionMap.GameStopwatchStr] = value; }
        }

        public Stopwatch GameStateStopwatch
        {
            get { return (Stopwatch)Session[SessionMap.GameStateStopwatchStr]; }
            set { Session[SessionMap.GameStateStopwatchStr] = value; }
        }

        public DbHandler dbHandler
        {
            get { return (DbHandler)Session[SessionMap.DbHandlerStr]; }
            set { Session[SessionMap.DbHandlerStr] = value; }
        }
    }
}