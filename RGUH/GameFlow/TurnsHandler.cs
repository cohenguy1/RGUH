using RGUH.Enums;
using RGUH.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace RGUH
{
    public partial class Game : System.Web.UI.Page
    {

        private void IncreaseCurrentTurn()
        {
            CurrentTurnNumber++;
        }

        private void UpdateTurnsTable(ScenarioTurn currentTurn, double averagePrizePoints)
        {
            if (currentTurn.TurnNumber <= Common.NumOfTurns)
            {
                int turnRow = Math.Min(currentTurn.TurnNumber, Common.NumOfTurnsInTable);

                UpdateTurnRow(currentTurn, turnRow);
            }

            AveragePrizePointsCell.Text = "&nbsp;Average Prize Points: " + Math.Round(averagePrizePoints, 0).ToString();
        }

        private void UpdateNewRow(int turnNumber)
        {
            ClearTableRowStyle(Common.NumOfTurnsInTable);

            UpdateTurnCellTitle(turnNumber, Common.NumOfTurnsInTable);

            SetTableRowStyle(Common.NumOfTurnsInTable);
        }

        private void UpdateTurnCellTitle(int turnNumber, int turnRow)
        {
            TableCell tableCell = GetTableCell(turnRow, TableColumnType.Position);

            tableCell.Text = "&nbsp;" + ScenarioTurn.GetTurnTitle(turnNumber);
        }

        private void ShiftCells()
        {
            var currentRow = Common.NumOfTurnsInTable - 1;

            var turnsToDisplay = ScenarioTurns.Where(turn => turn.Played).OrderByDescending(turn => turn.TurnNumber);

            foreach (var scenarioTurn in turnsToDisplay)
            {
                if (currentRow < 1)
                {
                    return;
                }

                UpdateTurnRow(scenarioTurn, currentRow);

                UpdateTurnCellTitle(scenarioTurn.TurnNumber, currentRow);

                currentRow--;
            }
        }

        private void UpdateTurnRow(ScenarioTurn currentTurn, int turnRow)
        {
            var positionPrizeCell = GetPrizePointsCell(turnRow);
            positionPrizeCell.Text = "&nbsp;" + (110 - currentTurn.ChosenCandidate.CandidateRank * 10).ToString();

            var rankCell = GetRankCell(turnRow);
            rankCell.Text = "&nbsp;#" + currentTurn.ChosenCandidate.CandidateRank;
        }

        private void ClearTurnTable()
        {
            for (int turnIndex = 1; turnIndex <= Common.NumOfTurnsInTable; turnIndex++)
            {
                ClearTableRowStyle(turnIndex);
            }

            AveragePrizePointsCell.Text = "&nbsp;Average Prize Points: ";
        }

        private void ClearTableRowStyle(int turnIndex)
        {
            ClearCellStyle(turnIndex, TableColumnType.Position);
            ClearCellStyle(turnIndex, TableColumnType.Rank);
            ClearCellStyle(turnIndex, TableColumnType.PrizePoints);
        }

        private void SetSeenTableRowStyle(int turnIndex)
        {
            SetSeenCellStyle(turnIndex, TableColumnType.Position);
            SetSeenCellStyle(turnIndex, TableColumnType.Rank);
            SetSeenCellStyle(turnIndex, TableColumnType.PrizePoints);
        }

        private void SetTableRowStyle(int turnIndex)
        {
            SetTableRowStyle(turnIndex, TableColumnType.Position);
            SetTableRowStyle(turnIndex, TableColumnType.Rank);
            SetTableRowStyle(turnIndex, TableColumnType.PrizePoints);
        }

        private void ClearCellStyle(int turnRow, TableColumnType position)
        {
            var tableCell = GetTableCell(turnRow, position);

            if (position != TableColumnType.Position)
            {
                tableCell.Text = "";
            }
            tableCell.ForeColor = System.Drawing.Color.Black;
            tableCell.Font.Italic = false;
            tableCell.Font.Bold = false;
        }

        private void SetSeenCellStyle(int turnRow, TableColumnType columnType)
        {
            var tableCell = GetTableCell(turnRow, columnType);
            tableCell.ForeColor = System.Drawing.Color.Blue;
            tableCell.Font.Bold = false;
            tableCell.Font.Italic = true;
        }

        private void SetTableRowStyle(int turnRow, TableColumnType columnType)
        {
            var tableCell = GetTableCell(turnRow, columnType);
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

        private ScenarioTurn GetCurrentTurn()
        {
            return ScenarioTurns[CurrentTurnNumber - 1];
        }

        private string GetJobTitle(int currentTurnNumber)
        {
            var turnTitle = ScenarioTurn.GetTurnTitle(currentTurnNumber) + "/" + Common.NumOfTurns;
            return turnTitle;
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