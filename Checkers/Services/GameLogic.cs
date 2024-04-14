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
    private bool AnotherTakeIsAvailable { get; set; }

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
        AnotherTakeIsAvailable = false;
    }

    public void ClickAction(Cell cell)
    {
        if (cell.CurrentState == State.WhitePiece && CurrentPlayer[0] == Player.White)
        {
            if (Helper.PreviousCell != null) 
                Helper.PreviousCell.CurrentImage = Helper.PreviousCell.CurrentState == State.WhitePiece?
                    Helper.PreviousCell.WhitePiece : Helper.PreviousCell.WhitePieceKing;

            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.WhitePieceSelected;
            return;
        }
        if (cell.CurrentState == State.BlackPiece && CurrentPlayer[0] == Player.Black)
        {
            if (Helper.PreviousCell != null)
                Helper.PreviousCell.CurrentImage = Helper.PreviousCell.CurrentState == State.BlackPiece ?
                    Helper.PreviousCell.BlackPiece : Helper.PreviousCell.BlackPieceKing;

            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.BlackPieceSelected;
            return;
        }
        if (cell.CurrentState == State.WhitePieceKing && CurrentPlayer[0] == Player.White)
        {
            if (Helper.PreviousCell != null)
                Helper.PreviousCell.CurrentImage = Helper.PreviousCell.CurrentState == State.WhitePiece ?
                    Helper.PreviousCell.WhitePiece : Helper.PreviousCell.WhitePieceKing;

            Helper.PreviousCell = cell;
            Helper.PreviousCell.CurrentImage = cell.WhitePieceKingSelected;
            return;
        }
        if (cell.CurrentState == State.BlackPieceKing && CurrentPlayer[0] == Player.Black)
        {
            if (Helper.PreviousCell != null)
                Helper.PreviousCell.CurrentImage = Helper.PreviousCell.CurrentState == State.BlackPiece ?
                    Helper.PreviousCell.BlackPiece : Helper.PreviousCell.BlackPieceKing;

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
            if (PieceTakingAvailable(Helper.PreviousCell,cell))
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

    private bool CheckForAnotherTakeAvailable(Cell cell)
    {
        var fromCell = cell;
        var toCells = GetToCells(cell);

        foreach (var toCell in toCells)
        {
            if (toCell.CurrentState != State.Empty) continue;
            if (PieceTakingAvailable(fromCell, toCell))
                return true;
        }
        return false;
    }

    private IEnumerable<Cell> GetToCells(Cell cell)
    {
        var neighbours = CorrectNeighbors(AllNeighbors(cell), cell);
        var updatedNeighbours = new List<Cell>();
        foreach (var neighbour in neighbours)
        {
            var xIndex = (neighbour.X - cell.X) * 2;
            var yIndex = (neighbour.Y - cell.Y) * 2;
            updatedNeighbours.Add(Cells[cell.Y+yIndex][cell.X + xIndex]);
        }
        return updatedNeighbours;
    }

    private static IEnumerable<Cell> CorrectNeighbors(IEnumerable<Cell> allNeighbors, Cell cell)
    {
        
        List<Cell> neighbours = [];
        foreach (var neighbour in allNeighbors)
        {
            if (cell.CurrentState == State.WhitePiece)
            {
                if (neighbour.X < cell.X && neighbour.Y < cell.Y)
                {
                    neighbours.Add(neighbour);
                }

                if (neighbour.X > cell.X && neighbour.Y < cell.Y)
                {
                    neighbours.Add(neighbour);
                }
            } 
            else if (cell.CurrentState == State.BlackPiece)
            {
                if (neighbour.X < cell.X && neighbour.Y > cell.Y)
                {
                    neighbours.Add(neighbour);
                }

                if (neighbour.X > cell.X && neighbour.Y > cell.Y)
                {
                    neighbours.Add(neighbour);
                }
            }
            else
            {
                neighbours.Add(neighbour);
            }
        }
        return neighbours;
    }

    private IEnumerable<Cell> AllNeighbors(Cell cell)
    {
        List<Cell> neighbours = [];
        var y = cell.X;
        var x = cell.Y;
        switch (x)
        {
            case <2 when y <2:
                neighbours.Add(Cells[x + 1][y + 1]);
                break;
            case <2 when y >5:
                neighbours.Add(Cells[x + 1][y - 1]);
                break;
            case >5 when y <2:
                neighbours.Add(Cells[x - 1][y + 1]);
                break;
            case >5 when y >5:
                neighbours.Add(Cells[x - 1][y - 1]);
                break;
            case <2:
                neighbours.Add(Cells[x + 1][y + 1]);
                neighbours.Add(Cells[x + 1][y - 1]);
                break;
            case >5:
                neighbours.Add(Cells[x - 1][y + 1]);
                neighbours.Add(Cells[x - 1][y - 1]);
                break;
            default:
            {
                switch (y)
                {
                    case <2:
                        neighbours.Add(Cells[x + 1][y + 1]);
                        neighbours.Add(Cells[x - 1][y + 1]);
                        break;
                    case >5:
                        neighbours.Add(Cells[x + 1][y - 1]);
                        neighbours.Add(Cells[x - 1][y - 1]);
                        break;
                    default:
                        neighbours.Add(Cells[x + 1][y + 1]);
                        neighbours.Add(Cells[x - 1][y + 1]);
                        neighbours.Add(Cells[x + 1][y - 1]);
                        neighbours.Add(Cells[x - 1][y - 1]);
                        break;
                }
                break;
            }
        }
        return neighbours;
    }

    private bool PieceTakingAvailable(Cell fromCell,Cell toCell)
    {
        if (fromCell.CurrentState == State.WhitePiece)
        {
            if (JumpAvailable(fromCell,toCell, -2, -2))
                return IsJumpedPieceCorrect(fromCell.X - 1, fromCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(fromCell, toCell, +2, -2))
                return IsJumpedPieceCorrect(fromCell.X + 1, fromCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
        }

        if (fromCell.CurrentState == State.BlackPiece)
        {
            if (JumpAvailable(fromCell, toCell, -2, +2))
                return IsJumpedPieceCorrect(fromCell.X - 1, fromCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(fromCell, toCell, +2, +2))
                return IsJumpedPieceCorrect(fromCell.X + 1, fromCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
        }

        if (fromCell.CurrentState == State.WhitePieceKing)
        {
            if (JumpAvailable(fromCell, toCell, -2, -2))
                return IsJumpedPieceCorrect(fromCell.X - 1, fromCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(fromCell, toCell, +2, -2))
                return IsJumpedPieceCorrect(fromCell.X + 1, fromCell.Y - 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(fromCell, toCell, -2, +2))
                return IsJumpedPieceCorrect(fromCell.X - 1, fromCell.Y + 1, State.BlackPiece,
                    State.BlackPieceKing);
            if (JumpAvailable(fromCell, toCell, +2, +2))
                return IsJumpedPieceCorrect(fromCell.X + 1, fromCell.Y + 1, State.BlackPiece,
                    State.BlackPieceKing);
        }

        if (fromCell.CurrentState == State.BlackPieceKing)
        {
            if (JumpAvailable(fromCell, toCell, -2, -2))
                return IsJumpedPieceCorrect(fromCell.X - 1, fromCell.Y - 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(fromCell, toCell, +2, -2))
                return IsJumpedPieceCorrect(fromCell.X + 1, fromCell.Y - 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(fromCell, toCell, -2, +2))
                return IsJumpedPieceCorrect(fromCell.X - 1, fromCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
            if (JumpAvailable(fromCell, toCell, +2, +2))
                return IsJumpedPieceCorrect(fromCell.X + 1, fromCell.Y + 1, State.WhitePiece,
                    State.WhitePieceKing);
        }


        return false;
    }

    private static bool JumpAvailable(Cell fromCell,Cell toCell, int xOffSet, int yOffSet)
    {
        return toCell.X == fromCell.X + xOffSet && toCell.Y == fromCell?.Y + yOffSet;
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

        if (TookAPiece) return true;
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
            AnotherTakeIsAvailable = CheckForAnotherTakeAvailable(cell);
            TookAPiece = false;

            if (AnotherTakeIsAvailable)
            {
                AnotherTakeIsAvailable = false;
                return;
            }
        }

        CurrentPlayer[0] = CurrentPlayer[0] == Player.White ? Player.Black : Player.White;
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
        if (gameName == "") return;
        SavedGamesHandler.SaveCurrentGame(Cells, CurrentPlayer[0], MultipleJumps, gameName);
    }
}