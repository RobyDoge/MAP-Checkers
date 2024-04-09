using Checkers.Model;
using System.Collections.ObjectModel;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Controls;

namespace Checkers.Services
{
    public class GameLogic
    {
        enum Player
        {
            White,
            Black
        }
        private Player CurrentPlayer { get; set; }
        private bool MultipleJumps { get; set; }
        private bool TookAPiece { get; set; }

        private int WhitePiecesNumber { get; set; }
        private int BlackPiecesNumber { get; set; }

        private ObservableCollection<ObservableCollection<Cell>> Cells { get; set; }
        public GameLogic(ObservableCollection<ObservableCollection<Cell>> cells, bool multipleJumps)
        {
            Cells = cells;
            CurrentPlayer = Player.White;
            MultipleJumps = multipleJumps;
            TookAPiece = false;
            WhitePiecesNumber = 12;
            BlackPiecesNumber = 12;

        }

        public void ClickAction(Cell cell)
        {
            if (cell.CurrentState == State.WhitePiece && CurrentPlayer == Player.White)
            {
                if (Helper.PreviousCell != null)
                {
                    Helper.PreviousCell.CurrentImage = Helper.PreviousCell.WhitePiece;
                }
                Helper.PreviousCell = cell;
                Helper.PreviousCell.CurrentImage = cell.WhitePieceSelected;
            }
            else if (cell.CurrentState == State.BlackPiece && CurrentPlayer == Player.Black)
            {
                if (Helper.PreviousCell != null)
                {
                    Helper.PreviousCell.CurrentImage = Helper.PreviousCell.BlackPiece;
                }
                Helper.PreviousCell = cell;
                Helper.PreviousCell.CurrentImage = cell.BlackPieceSelected;
            }
            else if (Helper.PreviousCell != null)
            {
                if (cell.CurrentState != State.Empty) return;
                if(!IsMovingForward(cell)) return;

                if (PieceTakingAvailable(cell))
                {
                    TookAPiece = true;

                    switch (Helper.PreviousCell.CurrentState)
                    {
                        case State.BlackPiece:
                            WhitePiecesNumber--;
                            break;
                        case State.WhitePiece:
                            BlackPiecesNumber--;
                            break;
                    }

                    if (WhitePiecesNumber == 0)
                    {
                        throw new Exception("Black Won");
                    }
                    if (BlackPiecesNumber == 0)
                    {
                        throw new Exception("White Won");
                    }
                }
                else if (!AreNeighbours(cell)) return;

                MovePiece(cell);
            }
        }

        private bool PieceTakingAvailable(Cell cell)
        {
            if (Helper.PreviousCell?.CurrentState == State.WhitePiece)
            {
                if (JumpAvailable(cell,-2,-2))
                {
                    return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y - 1, State.BlackPiece);
                }
                if (JumpAvailable(cell,+2,-2))
                {
                    return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y - 1, State.BlackPiece);
                }
            }

            if (JumpAvailable(cell,-2,+2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X - 1, Helper.PreviousCell.Y + 1, State.WhitePiece);
            if (JumpAvailable(cell,+2,+2))
                return IsJumpedPieceCorrect(Helper.PreviousCell.X + 1, Helper.PreviousCell.Y + 1, State.WhitePiece);

            return false;
        }

        private static bool JumpAvailable(Cell cell,int xOffSet, int yOffSet)
        {
            return cell.X == Helper.PreviousCell?.X + xOffSet && cell.Y == Helper.PreviousCell?.Y + yOffSet;
        }

        private bool IsJumpedPieceCorrect(int yIndex, int xIndex, State neededState)
        {
            if (Cells[xIndex][yIndex].CurrentState != neededState) return false;

            Cells[xIndex][yIndex].CurrentImage = Cells[xIndex][yIndex].BackgroundEmptyPath;
            Cells[xIndex][yIndex].CurrentState = State.Empty;
            return true;
        }

        private void MovePiece(Cell cell)
        {
            
            cell.CurrentImage = Helper.PreviousCell.CurrentState==State.WhitePiece ? Helper.PreviousCell.WhitePiece : Helper.PreviousCell.BlackPiece;
            cell.CurrentState = Helper.PreviousCell.CurrentState;
            Helper.PreviousCell.CurrentImage = Helper.PreviousCell.BackgroundEmptyPath;
            Helper.PreviousCell.CurrentState = State.Empty;
            Helper.PreviousCell = null;

            if (MultipleJumps && TookAPiece)
            {
                TookAPiece = false;
                return;
            }

            CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
              
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
    }

}

