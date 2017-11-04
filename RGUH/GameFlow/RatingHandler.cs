using RGUH.Enums;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Web;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {
        private bool NeedToAskRating()
        {
            if (AlreadyAskedForRating)
            {
                return false;
            }

            if (CurrentTurnNumber == Common.NumOfTurns)
            {
                return true;
            }

            if (AskPosition == AskPositionHeuristic.MEQO)
            {
                return MeqoUncertain.ShouldAsk(CurrentCandidate.CandidateRank);
            }
            
            if (AskPosition == AskPositionHeuristic.ESB)
            {
                int[] accepted = new int[Common.NumOfTurns];

                for (var index = 0; index < CurrentTurnNumber; index++)
                {
                    if (ScenarioTurns[index].Played)
                    {
                        accepted[index] = ScenarioTurns[index].ChosenCandidate.CandidateRank;
                    }
                    else
                    {
                        Alert.Show("Problem occured");
                    }
                }

                return EsbUncertain.ShouldAsk(accepted, CurrentTurnNumber - 1);
            }

            return false;
        }

        protected void RateAdvisor()
        {
            TimerGame.Enabled = false;

            MultiView2.ActiveViewIndex = 1;

            dbHandler.UpdateTimesTable(GameState.Rate);
        }

        protected void btnRate_Click(object sender, EventArgs e)
        {
            string userRating = ratingHdnValue.Value;

            if (string.IsNullOrEmpty(userRating) || userRating == "0")
            {
                return;
            }

            string reason = reasonTxtBox.Text;
            if (string.IsNullOrEmpty(reason.Trim()))
            {
                Alert.Show("Please explain your rating selection in the text box.");
                return;
            }

            int agentRating = int.Parse(userRating);

            SaveRatingToDB(agentRating);

            AskForRating = false;
            AlreadyAskedForRating = true;

            Response.Redirect("EndGame.aspx");
        }

        private void SaveRatingToDB(int adviserRating)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                sqlConnection1.Open();

                StringBuilder command = new StringBuilder();
                command.Append("INSERT INTO UserRatings (UserId, AdviserRating, ");

                for (int i = 1; i <= Common.NumOfTurns; i++)
                {
                    command.Append("Position" + i + "Rank, ");
                }

                command.Append("RatingPosition, AskPosition, VectorNum, Reason) ");
                command.Append("VALUES (@UserId, @AdviserRating,");

                for (int i = 1; i <= Common.NumOfTurns; i++)
                {
                    command.Append("@Position" + i + "Rank, ");
                }

                command.Append("@RatingPosition, @AskPosition, @VectorNum, @Reason) ");

                using (SQLiteCommand cmd = new SQLiteCommand(command.ToString()))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = sqlConnection1;
                    cmd.Parameters.AddWithValue("@UserId", UserId);
                    cmd.Parameters.AddWithValue("@AdviserRating", adviserRating.ToString());

                    for (int i = 1; i <= Common.NumOfTurns; i++)
                    {
                        cmd.Parameters.AddWithValue("@Position" + i + "Rank", GetChosenCandidateRankToInsertToDb(i));
                    }

                    cmd.Parameters.AddWithValue("@RatingPosition", CurrentTurnNumber - 1);
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    cmd.Parameters.AddWithValue("@VectorNum", VectorNum);
                    cmd.Parameters.AddWithValue("@Reason", reasonTxtBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetChosenCandidateRankToInsertToDb(int turnIndex)
        {
            var turn = ScenarioTurns[turnIndex - 1];

            if (!turn.Played)
            {
                return string.Empty;
            }

            return turn.ChosenCandidate.CandidateRank.ToString();
        }
    }
}
