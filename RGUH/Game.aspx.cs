using RGUH;
using RGUH.Enums;
using RGUH.Logic;
using System;
using System.Linq;
using System.Text;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {
        public const int StartTimerInterval = 2500;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                TimerGame.Interval = StartTimerInterval;
                TimerGame.Enabled = false;

                AlreadyAskedForRating = false;
                AskForRating = false;

                MultiView2.ActiveViewIndex = 0;

                ShowInterviewImages();

                CurrentTurnNumber = 1;

                StartInterviewsForPosition(1);
            }
        }

        protected void btnNextToQuiz_Click(object sender, EventArgs e)
        {
            Response.Redirect("Quiz.aspx");
        }

        private void StartInterviewsForPosition(int positionIndex)
        {
            TimerGame.Enabled = false;

            CurrentCandidate = null;

            StatusLabel.Text = "";

            SetTitle();

            ClearInterviewImages();

            PositionCandidates = GenerateCandidatesForPosition(positionIndex);

            CurrentTurnStatus = TurnStatus.Initial;

            TimerGame.Enabled = true;
        }


        private void SetTitle()
        {
            var currentTurnNumber = CurrentTurnNumber;
            string jobTitle = ScenarioTurn.GetTurnTitle(currentTurnNumber);        

            PositionHeader.Text = "Position: " + jobTitle;

            if (currentTurnNumber >= 1)
            {
                MovingToNextPositionLabel.Text = Common.MovingToNextString;
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = jobTitle + "<br /><br /><br />";
                MovingJobTitleLabel.Visible = true;

                if (currentTurnNumber > 1 && currentTurnNumber <= Common.NumOfTurnsInTable)
                {
                    SetSeenTableRowStyle(currentTurnNumber - 1);
                }
            }

            // next turn
            if (currentTurnNumber <= Common.NumOfTurnsInTable)
            {
                SetTableRowStyle(currentTurnNumber);
            }
            else if (currentTurnNumber <= Common.NumOfTurns)
            {
                ShiftCells();

                UpdateNewRow(currentTurnNumber);
            }
        }

        protected void TimerGame_Tick(object sender, EventArgs e)
        {
            if (AskForRating)
            {
                RateAdvisor();
                return;
            }

            if (CurrentTurnNumber > Common.NumOfTurns)
            {
                TimerGame.Enabled = false;
                Response.Redirect("EndGame.aspx");
            }
            else if (CurrentTurnStatus == TurnStatus.Initial)
            {
                UpdateImages(CandidateState.Interview);
                CurrentTurnStatus = TurnStatus.Interviewing;
            }
            else if (CurrentTurnStatus == TurnStatus.Interviewing)
            {
                ProcessCandidate();
            }
        }

        private void FillNextPosition()
        {
            StartInterviewsForPosition(CurrentTurnNumber);
        }

        private void ProcessCandidate()
        {
            var dm = DecisionMaker.GetInstance();
            Candidate hiredWorker = dm.ProcessNextPosition(PositionCandidates);
            hiredWorker.CandidateState = CandidateState.Completed;

            if (!TimerGame.Enabled)
            {
                TimerGame.Interval = 1000;
                TimerGame.Enabled = true;
            }

            CurrentCandidate = hiredWorker;
            GetCurrentTurn().SetPlayed();

            UpdatePositionToAcceptedCandidate(hiredWorker);
            TurnSummary();
        }

        private void TurnSummary()
        {
            TimerGame.Enabled = false;
            CurrentTurnStatus = TurnStatus.Initial;
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
            ImageHired.Visible = true;
            TurnSummaryLbl1.Visible = true;
            TurnSummaryLbl2.Visible = true;
            TurnSummaryLbl3.Visible = true;
            PrizePointsLbl1.Visible = true;
            PrizePointsLbl2.Visible = true;
            PrizePointsLbl3.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextToUniform.Visible = true;

            TurnSummaryLbl2.Text = CurrentCandidate.CandidateRank.ToString();
            PrizePointsLbl2.Text = (110 - CurrentCandidate.CandidateRank * 10).ToString();
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to pick uniform for this " + ScenarioTurn.WaiterStr + ".<br />";
        }

        private void PickUniform()
        {
            if (NeedToAskRating())
            {
                AskForRating = true;
                AlreadyAskedForRating = true;
            }

            MultiView2.ActiveViewIndex = 2;
            ShowUniforms();

            CurrentTurnStatus = TurnStatus.FillNextPosition;
        }

        private void ShowUniforms()
        {
            UniformPickForPosition.Text = " Pick the uniform for this " + ScenarioTurn.WaiterStr + ":";

            Uniform1.ImageUrl = "~/Images/" + ScenarioTurn.WaiterStr + ".Uniform1.jpg";
            Uniform2.ImageUrl = "~/Images/" + ScenarioTurn.WaiterStr + ".Uniform2.jpg";
            Uniform3.ImageUrl = "~/Images/" + ScenarioTurn.WaiterStr + ".Uniform3.jpg";
        }

        protected void btnPickUniform_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;

            IncreaseCurrentTurn();

            if (CurrentTurnNumber <= Common.NumOfTurns)
            {
                FillNextPosition();
            }
            else
            {
                if (AskForRating)
                {
                    RateAdvisor();
                }
                else
                {
                    Response.Redirect("EndGame.aspx");
                }
            }
        }

        private void UpdatePositionToAcceptedCandidate(Candidate candidate)
        {
            var currentTurn = GetCurrentTurn();

            currentTurn.ChosenCandidate = candidate;

            AcceptedCandidates[currentTurn.TurnNumber - 1] = currentTurn.ChosenCandidate.CandidateRank;

            double averagePrizePoints = Common.GetAveragePrizePoints(ScenarioTurns);
            UpdateTurnsTable(currentTurn, averagePrizePoints);
        }

        protected void btnNextToUniform_Click(object sender, EventArgs e)
        {
            ImageHired.Visible = false;
            TurnSummaryLbl1.Visible = false;
            TurnSummaryLbl2.Visible = false;
            TurnSummaryLbl3.Visible = false;
            PrizePointsLbl1.Visible = false;
            PrizePointsLbl2.Visible = false;
            PrizePointsLbl3.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextToUniform.Visible = false;
            PickUniform();
        }
    }
}