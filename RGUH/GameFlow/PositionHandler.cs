using RGUH.Enums;
using RGUH.Logic;
using System.Web.UI.WebControls;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {

        private void IncreaseCurrentPosition()
        {
            CurrentPositionNumber++;
        }

        private void UpdatePositionsTable(Position currentPosition, int totalPrizePoints)
        {
            var positionPrizeCell = GetPrizePointsCell(currentPosition.PositionNumber);
            positionPrizeCell.Text = "&nbsp;" + (110 - currentPosition.ChosenCandidate.CandidateRank * 10).ToString();

            var rankCell = GetRankCell(currentPosition.PositionNumber);
            rankCell.Text = "&nbsp;#" + currentPosition.ChosenCandidate.CandidateRank;

            TotalPrizePointsCell.Text = "&nbsp;Total Prize Points: " + totalPrizePoints.ToString();
        }

        private void SetSeenTableRowStyle(int positionIndex)
        {
            SetSeenCellStyle(positionIndex, TableColumnType.Position);
            SetSeenCellStyle(positionIndex, TableColumnType.Rank);
            SetSeenCellStyle(positionIndex, TableColumnType.PrizePoints);
        }

        private void SetTableRowStyle(int positionIndex)
        {
            SetTableRowStyle(positionIndex, TableColumnType.Position);
            SetTableRowStyle(positionIndex, TableColumnType.Rank);
            SetTableRowStyle(positionIndex, TableColumnType.PrizePoints);
        }

        private void SetSeenCellStyle(int positionIndex, TableColumnType columnType)
        {
            var tableCell = GetTableCell(positionIndex, columnType);
            tableCell.ForeColor = System.Drawing.Color.Blue;
            tableCell.Font.Bold = false;
            tableCell.Font.Italic = true;
        }

        private void SetTableRowStyle(int positionIndex, TableColumnType columnType)
        {
            var tableCell = GetTableCell(positionIndex, columnType);
            tableCell.ForeColor = System.Drawing.Color.Green;
            tableCell.Font.Bold = true;
        }

        private TableCell GetTableCell(int rowNumber, TableColumnType position)
        {
            switch (position)
            {
                case TableColumnType.Position:
                    return GetPositionCell(rowNumber);
                case TableColumnType.Rank:
                    return GetRankCell(rowNumber);
                case TableColumnType.PrizePoints:
                    return GetPrizePointsCell(rowNumber);
            }

            return null;
        }

        private Position GetCurrentPosition()
        {
            return Positions[CurrentPositionNumber - 1];
        }

        private TableCell GetPositionCell(int row)
        {
            switch (row)
            {
                case 1:
                    return Waiter1Cell;
                case 2:
                    return Waiter2Cell;
                case 3:
                    return Waiter3Cell;
                case 4:
                    return Waiter4Cell;
                case 5:
                    return Waiter5Cell;
                case 6:
                    return Waiter6Cell;
                case 7:
                    return Waiter7Cell;
                case 8:
                    return Waiter8Cell;
                case 9:
                    return Waiter9Cell;
                case 10:
                    return Waiter10Cell;
                default:
                    return null;
            }
        }

        private TableCell GetPrizePointsCell(int row)
        {
            switch (row)
            {
                case 1:
                    return Waiter1PrizeCell;
                case 2:
                    return Waiter2PrizeCell;
                case 3:
                    return Waiter3PrizeCell;
                case 4:
                    return Waiter4PrizeCell;
                case 5:
                    return Waiter5PrizeCell;
                case 6:
                    return Waiter6PrizeCell;
                case 7:
                    return Waiter7PrizeCell;
                case 8:
                    return Waiter8PrizeCell;
                case 9:
                    return Waiter9PrizeCell;
                case 10:
                    return Waiter10PrizeCell;
                default:
                    return null;
            }
        }

        private TableCell GetRankCell(int row)
        {
            switch (row)
            {
                case 1:
                    return Waiter1RankCell;
                case 2:
                    return Waiter2RankCell;
                case 3:
                    return Waiter3RankCell;
                case 4:
                    return Waiter4RankCell;
                case 5:
                    return Waiter5RankCell;
                case 6:
                    return Waiter6RankCell;
                case 7:
                    return Waiter7RankCell;
                case 8:
                    return Waiter8RankCell;
                case 9:
                    return Waiter9RankCell;
                case 10:
                    return Waiter10RankCell;
                default:
                    return null;
            }
        }
    }
}