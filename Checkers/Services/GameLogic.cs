using System.Collections.ObjectModel;
using Checkers.Model;
using Checkers.View;
using Checkers.XMLHandlers;

namespace Checkers.Services;

public class GameLogic : BaseNotification
{
    public enum Player
    {
        White,
        Black
    }

    public ObservableCollection<Player> CurrentPlayer { get; set; }
    private bool MultipleJumps { get; }
    private bool TookAPiece { get; set; }
    private bool IsGameOver { get; set; }
    private bool IsTakeAvailable { get; set; }

    public ObservableCollection<int> WhitePiecesNumber { get; set; }

    public ObservableCollection<int> BlackPiecesNumber { get; set; }

    private ObservableCollection<ObservableCollection<Cell>> Cells { get; }

    public GameLogic(ObservableCollection<ObservableCollection<Cell>> cells, bool multipleJumps)
    {
        Cells = cells;
        CurrentPlayer = [Player.White];
        MultipleJumps = multipleJumps;
        TookAPiece = false;
        WhitePiecesNumber = [12];
        BlackPiecesNumber = [12];
    }

    public void ClickAction(Cell cell)
    {
        if (cell.CurrentState == State.WhitePiece && CurrentPlayer[0] == Player.White)
        {
            if (Helper.PreviousCell != null) Helper.PreviousCell.CurrentImage = Helper.PreviousCell.WhitePiece;

            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.WhitePieceSelected;
            return;
        }
        if (cell.CurrentState == State.BlackPiece && CurrentPlayer[0] == Player.Black)
        {
            if (Helper.PreviousCell != null) Helper.PreviousCell.CurrentImage = Helper.PreviousCell.BlackPiece;

            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.BlackPieceSelected;
            return;
        }
        if (cell.CurrentState == State.WhitePieceKing && CurrentPlayer[0] == Player.White)
        {
            if (Helper.PreviousCell != null) Helper.PreviousCell.CurrentImage = Helper.PreviousCell.WhitePieceKing;
            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.WhitePieceKingSelected;
            return;
        }
        if (cell.CurrentState == State.BlackPieceKing && CurrentPlayer[0] == Player.Black)
        {
            if (Helper.PreviousCell != null) Helper.PreviousCell.CurrentImage = Helper.PreviousCell.BlackPieceKing;
            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.BlackPieceKingSelected;
            return;
        }
        if (Helper.PreviousCell != null)
        {
            if (cell.CurrentState != State.Empty) return;
            if (Helper.PreviousCell.CurrentState is not (State.WhitePieceKing or State.BlackPieceKing))
                if (!IsMovingForward(cell))
                    return;
            if (PieceTakingAvailable(cell))
            {
                TookAPiece = true;
                switch (Helper.PreviousCell.CurrentState)
                {
                    case State.BlackPiece:
                        WhitePiecesNumber[0]--;
                        break;
                    case State.BlackPieceKing:
                        WhitePiecesNumber[0]--;
                        break;
                    case State.WhitePiece:
                        BlackPiecesNumber[0]--;
                        break;
                    case State.WhitePieceKing:
                        BlackPiecesNumber[0]--;
                        break;
                }

                if (WhitePiecesNumber[0] == 0) throw new Exception("Black Won");

                if (BlackPiecesNumber[0] == 0) throw new Exception("White Won");
            }
            else if (!AreNeighbours(cell))
            {
                return;
            }

            MovePiece(cell);
        }
    }

    private bool PieceTakingAvailable(Cell cell)
    {
        if (Helper.PreviousCell?.CurrentState == State.WhitePiece)
        {
            if (JumpAvailable(cell, -2, -2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(cell, +2, -2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
        }

        if (Helper.PreviousCell?.CurrentState == State.BlackPiece)
        {
            if (JumpAvailable(cell, -2, +2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(cell, +2, +2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
        }

        if (Helper.PreviousCell?.CurrentState == State.WhitePieceKing)
        {
            if (JumpAvailable(cell, -2, -2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(cell, +2, -2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(cell, -2, +2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y + 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(cell, +2, +2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y + 1, State.BlackPiece,
                    State.BlackPieceKing);
        }

        if (Helper.PreviousCell?.CurrentState == State.BlackPieceKing)
        {
            if (JumpAvailable(cell, -2, -2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y - 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(cell, +2, -2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y - 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(cell, -2, +2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(cell, +2, +2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
        }


        return false;
    }

    private static bool JumpAvailable(Cell cell, int xOffSet, int yOffSet)
    {
        return cell.X == Helper.PreviousCell?.X + xOffSet && cell.Y == Helper.PreviousCell?.Y + yOffSet;
    }

    private bool IsJumpedPieceCorrect(int yIndex, int xIndex, params State[] neededState)
    {
        if (neededState.Length == 2)
        {
            if (Cells[xIndex][yIndex].CurrentState != neededState[0] &&
                Cells[xIndex][yIndex].CurrentState != neededState[1])
                return false;
        }
        else if (Cells[xIndex][yIndex].CurrentState != neededState[0]) return false;

        Cells[xIndex][yIndex].CurrentImage = Cells[xIndex][yIndex].BackgroundEmptyPath;
        Cells[xIndex][yIndex].CurrentState = State.Empty;
        return true;
    }

    private void MovePiece(Cell cell)
    {
        if (Helper.PreviousCell.CurrentState is (State.BlackPieceKing or State.WhitePieceKing))
        {
            cell.CurrentState = Helper.PreviousCell.CurrentState;
            cell.CurrentImage = Helper.PreviousCell.CurrentState == State.WhitePieceKing
                ? Helper.PreviousCell.WhitePieceKing
                : Helper.PreviousCell.BlackPieceKing;
        }
        else
        {
            cell.CurrentImage = Helper.PreviousCell.CurrentState == State.WhitePiece
                ? Helper.PreviousCell.WhitePiece
                : Helper.PreviousCell.BlackPiece;
            cell.CurrentState = Helper.PreviousCell.CurrentState;
        }

        Helper.PreviousCell.CurrentImage = Helper.PreviousCell.BackgroundEmptyPath;
        Helper.PreviousCell.CurrentState = State.Empty;
        Helper.PreviousCell = null;

        if (cell is { CurrentState: State.WhitePiece, Y: 0 })
        {
            cell.CurrentState = State.WhitePieceKing;
            cell.CurrentImage = cell.WhitePieceKing;
        }
        else if (cell is { CurrentState: State.BlackPiece, Y: 7 })
        {
            cell.CurrentState = State.BlackPieceKing;
            cell.CurrentImage = cell.BlackPieceKing;
        }

        if (MultipleJumps && TookAPiece)
        {
            TookAPiece = false;
            return;
        }

        CurrentPlayer[0] = CurrentPlayer[0] == Player.White ? Player.Black : Player.White;

        //TODO: check if there are any jumps left
        //if (TookAPiece)
        //{
        //    TookAPiece = false;
        //}
        //else CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
    }

    private static bool IsMovingForward(Cell cell)
    {
        if (Helper.PreviousCell.CurrentState == State.BlackPiece)
        {
            if (cell.X > Helper.PreviousCell.X && cell.Y > Helper.PreviousCell.Y) return true;
            if (cell.X < Helper.PreviousCell.X && cell.Y > Helper.PreviousCell.Y) return true;
        }
        else if (Helper.PreviousCell.CurrentState == State.WhitePiece)
        {
            if (cell.X > Helper.PreviousCell.X && cell.Y < Helper.PreviousCell.Y) return true;
            if (cell.X < Helper.PreviousCell.X && cell.Y < Helper.PreviousCell.Y) return true;
        }

        return false;
    }

    private static bool AreNeighbours(Cell cell)
    {
        if (cell.X == Helper.PreviousCell.X + 1 && cell.Y == Helper.PreviousCell.Y + 1) return true;
        if (cell.X == Helper.PreviousCell.X - 1 && cell.Y == Helper.PreviousCell.Y - 1) return true;
        if (cell.X == Helper.PreviousCell.X + 1 && cell.Y == Helper.PreviousCell.Y - 1) return true;
        if (cell.X == Helper.PreviousCell.X - 1 && cell.Y == Helper.PreviousCell.Y + 1) return true;
        return false;
    }

    public void SaveGameAction(string notUsed)
    {
        var dialog = new InputGameName();
        dialog.ShowDialog();
        var gameName = dialog.GameName;
        if (gameName == null) return;
        SavedGamesHandler.SaveCurrentGame(Cells, CurrentPlayer[0], MultipleJumps, gameName);
    }
}