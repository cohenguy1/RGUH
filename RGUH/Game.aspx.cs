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

                CurrentPositionNumber = 1;

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

            SetTitle(positionIndex);

            ClearInterviewImages();

            PositionCandidates = GenerateCandidatesForPosition(positionIndex);

            CurrentPositionStatus = PositionStatus.Initial;

            TimerGame.Enabled = true;
        }


        private void SetTitle(int positionNumber)
        {
            string jobTitle = Position.GetJobTitle(positionNumber);        

            PositionHeader.Text = "Position: " + jobTitle;

            if (positionNumber >= 1)
            {
                MovingToNextPositionLabel.Text = Common.MovingToNextString;
                MovingToNextPositionLabel.Visible = true;

                MovingJobTitleLabel.Text = jobTitle + "<br /><br /><br />";
                MovingJobTitleLabel.Visible = true;

                if (positionNumber > 1)
                {
                    SetSeenTableRowStyle(positionNumber - 1);
                }
            }

            SetTableRowStyle(positionNumber);
        }

        protected void TimerGame_Tick(object sender, EventArgs e)
        {
            if (AskForRating)
            {
                RateAdvisor();
                return;
            }

            if (CurrentPositionNumber > Common.NumOfPositions)
            {
                TimerGame.Enabled = false;
                Response.Redirect("EndGame.aspx");
            }
            else if (CurrentPositionStatus == PositionStatus.Initial)
            {
                UpdateImages(CandidateState.Interview);
                CurrentPositionStatus = PositionStatus.Interviewing;
            }
            else if (CurrentPositionStatus == PositionStatus.Interviewing)
            {
                ProcessCandidate();
            }
        }

        private void FillNextPosition()
        {
            StartInterviewsForPosition(CurrentPositionNumber);
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
            UpdatePositionToAcceptedCandidate(hiredWorker);
            PositionSummary();
        }

        private void PositionSummary()
        {
            TimerGame.Enabled = false;
            CurrentPositionStatus = PositionStatus.Initial;
            ImageInterview.Visible = false;
            LabelInterviewing.Visible = false;
            ImageHired.Visible = true;
            PositionSummaryLbl1.Visible = true;
            PositionSummaryLbl2.Visible = true;
            PositionSummaryLbl3.Visible = true;
            PrizePointsLbl1.Visible = true;
            PrizePointsLbl2.Visible = true;
            PrizePointsLbl3.Visible = true;
            SummaryNextLbl.Visible = true;
            btnNextToUniform.Visible = true;

            PositionSummaryLbl2.Text = CurrentCandidate.CandidateRank.ToString();
            PrizePointsLbl2.Text = (110 - CurrentCandidate.CandidateRank * 10).ToString();
            SummaryNextLbl.Text = "<br /><br />Press 'Next' to pick uniform for this " + Position.WaiterStr + ".<br />";
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

            CurrentPositionStatus = PositionStatus.FillNextPosition;
        }

        private void ShowUniforms()
        {
            UniformPickForPosition.Text = " Pick the uniform for this " + Position.WaiterStr + ":";

            Uniform1.ImageUrl = "~/Images/" + Position.WaiterStr + ".Uniform1.jpg";
            Uniform2.ImageUrl = "~/Images/" + Position.WaiterStr + ".Uniform2.jpg";
            Uniform3.ImageUrl = "~/Images/" + Position.WaiterStr + ".Uniform3.jpg";
        }

        protected void btnPickUniform_Click(object sender, EventArgs e)
        {
            MultiView2.ActiveViewIndex = 0;

            IncreaseCurrentPosition();

            if (CurrentPositionNumber <= Common.NumOfPositions)
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
            var currentPosition = GetCurrentPosition();

            currentPosition.ChosenCandidate = candidate;

            AcceptedCandidates[currentPosition.PositionNumber - 1] = currentPosition.ChosenCandidate.CandidateRank;

            int totalPrizePoints = Common.GetTotalPrizePoints(Positions);
            UpdatePositionsTable(currentPosition, totalPrizePoints);
        }

        protected void btnNextToUniform_Click(object sender, EventArgs e)
        {
            ImageHired.Visible = false;
            PositionSummaryLbl1.Visible = false;
            PositionSummaryLbl2.Visible = false;
            PositionSummaryLbl3.Visible = false;
            PrizePointsLbl1.Visible = false;
            PrizePointsLbl2.Visible = false;
            PrizePointsLbl3.Visible = false;
            SummaryNextLbl.Visible = false;
            btnNextToUniform.Visible = false;
            PickUniform();
        }
    }
}