﻿using Checkers.Model;
using System.Collections.ObjectModel;

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
        private ObservableCollection<ObservableCollection<Cell>> Cells { get; set; }
        public GameLogic(ObservableCollection<ObservableCollection<Cell>> cells, bool multipleJumps)
        {
            Cells = cells;
            CurrentPlayer = Player.White;
            MultipleJumps = multipleJumps;
            TookAPiece = false;
        }
        public void ClickAction(Cell cell)
        {
            if (cell.CurrentState == State.WhitePiece && CurrentPlayer == Player.White)
            {
                Helper.PreviousCell = cell;
            }
            else if (cell.CurrentState == State.BlackPiece && CurrentPlayer == Player.Black)
            {
                Helper.PreviousCell = cell;
            }
            else if (Helper.PreviousCell != null)
            {
                if (cell.CurrentState != State.Empty) return;
                if(!IsMovingForward(cell, Helper.PreviousCell)) return;
                if (PieceTakingAvailable(cell, Helper.PreviousCell))
                {
                    MovePiece(cell, Helper.PreviousCell);
                    TookAPiece = true;
                    return;
                }
                if (!AreNeighbours(cell, Helper.PreviousCell)) return;

                MovePiece(cell, Helper.PreviousCell);
            }
        }

        private bool PieceTakingAvailable(Cell cell, Cell previousCell)
        {
            if (AvailableJump(cell, previousCell, 2, 2)) return true;
            if (AvailableJump(cell, previousCell, -2, -2)) return true;
            if (AvailableJump(cell, previousCell, +2, -2)) return true;
            if (AvailableJump(cell, previousCell, -2, 2)) return true;
            return false;
        }

        private bool AvailableJump(Cell cell, Cell previousCell, int X, int Y)
        {
            int targetX = previousCell.X + X;
            int targetY = previousCell.Y + Y;
            if (targetX < 1 || targetX >= Cells.Count || targetY < 1 || targetY >= Cells[targetX].Count)
            {
                return false;
            }

            return Cells[targetX][targetY].CurrentState == State.Empty &&
                   Cells[targetX - 1][targetY - 1].CurrentState != previousCell.CurrentState &&
                   Cells[targetX - 1][targetY - 1].CurrentState != State.Empty &&
                   cell.X == targetX &&
                   cell.Y == targetY
                   ;
        }

        private void MovePiece(Cell cell, Cell previousCell)
        {
          
            cell.CurrentImage = previousCell.CurrentImage;
            cell.CurrentState = previousCell.CurrentState;
            previousCell.CurrentImage = previousCell.BackgroundEmptyPath;
            previousCell.CurrentState = State.Empty;
            previousCell = null;
            if (!MultipleJumps)
            {
                CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;
                return;
            }
            //TODO: check if there are any jumps left
            //if (TookAPiece)
            //{
            //    TookAPiece = false;
            //}
            //else CurrentPlayer = CurrentPlayer == Player.White ? Player.Black : Player.White;

        }

        private static bool IsMovingForward(Cell cell, Cell previousCell)
        {
            if (previousCell.CurrentState == State.BlackPiece)
            {
                if (cell.X > previousCell.X && cell.Y > previousCell.Y) return true;
                if (cell.X < previousCell.X && cell.Y > previousCell.Y) return true;
            }
            else if (previousCell.CurrentState == State.WhitePiece)
            {
                if (cell.X > previousCell.X && cell.Y < previousCell.Y) return true;
                if (cell.X < previousCell.X && cell.Y < previousCell.Y) return true;
            }
            return false;

        }

        private static bool AreNeighbours(Cell cell, Cell previousCell)
        {
            if (cell.X == previousCell.X + 1 && cell.Y == previousCell.Y + 1) return true;
            if (cell.X == previousCell.X - 1 && cell.Y == previousCell.Y - 1) return true;
            if (cell.X == previousCell.X + 1 && cell.Y == previousCell.Y - 1) return true;
            if (cell.X == previousCell.X - 1 && cell.Y == previousCell.Y + 1) return true;
            return false;
            
        }
    }

}
