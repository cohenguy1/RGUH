using RGUH.Enums;
using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SQLite;

namespace RGUH
{
    public partial class EndGame : System.Web.UI.Page
    {
        public const double PointsPerCent = 4;

        public const double centToDollar = 1 / 100.0;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GameStopwatch.Stop();
                
                double avgPrizePoints = Common.GetAveragePrizePoints(ScenarioTurns);

                AveragePrizePointsLbl.Text = avgPrizePoints.ToString("");

                BonusLbl.Text = Math.Round(avgPrizePoints / PointsPerCent, 0) + " cents";

                dbHandler.UpdateTimesTable(GameState.EndGame);
            }
        }

        private double GetBonus()
        {
            var dollars = Common.GetAveragePrizePoints(ScenarioTurns) / PointsPerCent;
            var cents = dollars / 100;
            return Math.Round(cents, 2);
        }

        protected void rewardBtn_Click(object sender, EventArgs e)
        {
            string workerId = UserId;

            dbHandler.UpdateTimesTable(GameState.CollectedPrize);

            string assignmentId = (string)Session["turkAss"];

            NameValueCollection data = new NameValueCollection();
            data.Add("assignmentId", assignmentId);
            data.Add("workerId", workerId);
            data.Add("hitId", (string)Session["hitId"]);

            SendFeedback();

            rewardBtn.Enabled = false;

            if (workerId != "friend")
            {
                IncreaseAskPositionCount();

                DisposeSession();

                Alert.RedirectAndPOST(this.Page, "https://www.mturk.com/mturk/externalSubmit", data);
            }
        }

        public void IncreaseAskPositionCount()
        {
            string nextAskPosition = null;

            switch (AskPosition)
            {
                case AskPositionHeuristic.MEQO:
                    nextAskPosition = AskPositionHeuristic.ESB.ToString();
                    break;
                case AskPositionHeuristic.ESB:
                    nextAskPosition = AskPositionHeuristic.Random.ToString();
                    break;
                case AskPositionHeuristic.Random:
                    nextAskPosition = "Done";
                    break;
            }

            DbHandler dbHandler = new DbHandler();
            dbHandler.SetVectorNextAskPosition(nextAskPosition);
        }

        private void SendFeedback()
        {
            var connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();
            string feedback = feedbackTxtBox.Text;

			var bonusDollars = GetBonus();
            try
            {
                using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
                {
                    using (SQLiteCommand cmd = new SQLiteCommand("INSERT INTO UserFeedback (UserId, Feedback, TotalTime, AveragePrizePoints, Bonus) " + 
                        "VALUES (@UserId, @Feedback, @TotalTime, @AveragePrizePoints, @Bonus)"))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = sqlConnection1;
                        cmd.Parameters.AddWithValue("@UserId", UserId);
                        cmd.Parameters.AddWithValue("@Feedback", feedback);
                        cmd.Parameters.AddWithValue("@TotalTime", Math.Round(GameStopwatch.Elapsed.TotalMinutes, 1));
                        cmd.Parameters.AddWithValue("@AveragePrizePoints", Common.GetAveragePrizePoints(ScenarioTurns));
                        cmd.Parameters.AddWithValue("@Bonus", bonusDollars);
                        sqlConnection1.Open();
                        cmd.ExecuteNonQuery();

                        sqlConnection1.Close();
                    }
                }
            }
            catch (SQLiteException)
            {
            }
        }

        private void DisposeSession()
        {
            if (dbHandler != null)
            {
                dbHandler.Dispose();
            }
            
            if (ScenarioTurns != null)
            {
                ScenarioTurns = null;
            }

            if (PositionCandidates != null)
            {
                PositionCandidates = null;
            }
        }
    }
}