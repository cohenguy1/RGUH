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

            if (CurrentPositionNumber == Common.NumOfPositions)
            {
                return true;
            }

            if (AskPosition == AskPositionHeuristic.First)
            {
                return (CurrentPositionNumber == 1);
            }

            if (AskPosition == AskPositionHeuristic.Last)
            {
                return (CurrentPositionNumber == Common.NumOfPositions);
            }

            if (AskPosition == AskPositionHeuristic.Random)
            {
                return (CurrentPositionNumber == RandomHuristicAskPosition);
            }

            if (AskPosition == AskPositionHeuristic.Optimal)
            {
                return Optimal.ShouldAsk(CurrentPositionNumber - 1, CurrentCandidate.CandidateRank);
            }
            
            if (AskPosition == AskPositionHeuristic.MonteCarlo)
            {
                int[] accepted = new int[10];

                for (var index = 0; index < CurrentPositionNumber; index++)
                {
                    if (Positions[index].ChosenCandidate != null)
                    {
                        accepted[index] = Positions[index].ChosenCandidate.CandidateRank;
                    } else
                    {
                        Alert.Show("Problem occured");
                    }
                }

                return MonteCarlo.ShouldAsk(accepted, CurrentPositionNumber - 1);
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

            MultiView2.ActiveViewIndex = 0;

            dbHandler.UpdateTimesTable(GameState.AfterRate);

            AskForRating = false;
            AlreadyAskedForRating = true;

            TimerGame.Enabled = true;

            if (CurrentPositionNumber > Common.NumOfPositions)
            {
                Response.Redirect("EndGame.aspx");
            }
        }

        private void SaveRatingToDB(int adviserRating)
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString"].ToString();

            using (SQLiteConnection sqlConnection1 = new SQLiteConnection(connectionString))
            {
                sqlConnection1.Open();

                StringBuilder command = new StringBuilder();
                command.Append("INSERT INTO UserRatings (UserId, AdviserRating, ");

                for (int i = 1; i <= Common.NumOfPositions; i++)
                {
                    command.Append("Position" + i + "Rank, ");
                }

                command.Append("RatingPosition, AskPosition, VectorNum, Reason) ");
                command.Append("VALUES (@UserId, @AdviserRating,");

                for (int i = 1; i <= Common.NumOfPositions; i++)
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

                    for (int i = 1; i <= Common.NumOfPositions; i++)
                    {
                        cmd.Parameters.AddWithValue("@Position" + i + "Rank", GetChosenCandidateRankToInsertToDb(i));
                    }

                    cmd.Parameters.AddWithValue("@RatingPosition", CurrentPositionNumber - 1);
                    cmd.Parameters.AddWithValue("@AskPosition", AskPosition.ToString());
                    cmd.Parameters.AddWithValue("@VectorNum", VectorNum);
                    cmd.Parameters.AddWithValue("@Reason", reasonTxtBox.Text);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        private string GetChosenCandidateRankToInsertToDb(int positionIndex)
        {
            var position = Positions[positionIndex - 1];

            if (position.ChosenCandidate == null)
            {
                return string.Empty;
            }

            return position.ChosenCandidate.CandidateRank.ToString();
        }
    }
}
