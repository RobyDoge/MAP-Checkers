﻿using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Checkers.Model;

namespace Checkers.Services
{
    public static class Helper
    {
        public static Cell PreviousCell { get; set; }

        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            var board = new ObservableCollection<ObservableCollection<Cell>>();
            var count = 0;

            /*//laptop
            for (var i = 0; i < 8; i++)
            {
                var row = new ObservableCollection<Cell>();
                for (var j = 0; j < 8; j++)
                {
                    row.Add(count % 2 == 0
                            ? new Cell(j, i, @"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_Empty.png",
                                @"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_WhitePiece.png",
                                @"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_BlackPiece.png")
                            : new Cell(j, i, @"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\WhiteSpace_Empty.png"));
                    row[j].CurrentImage = row[j].BackgroundEmptyPath;
                    count++;
                }
                for (var j = 0; j < 8; j++)
               {
                   if (i < 3)
                   {
                       if (row[j].BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                       {
                           row[j].CurrentImage = row[j].BlackPiece;
                           row[j].CurrentState = State.BlackPiece;
                           continue;
                       }
                       row[j].CurrentImage = row[j].BackgroundEmptyPath;
                   }
                   else if (i > 4)
                   {
                       if (row[j].BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                       {
                           row[j].CurrentImage = row[j].WhitePiece;
                           row[j].CurrentState = State.WhitePiece;
                           continue;
                       }
                       row[j].CurrentImage = row[j].BackgroundEmptyPath;
                   }
                   row[j].CurrentImage = row[j].BackgroundEmptyPath;
               }
               board.Add(row);
               count--;
            }
            */

            //PC
            for (var i = 0; i < 8; i++)
            {
                var row = new ObservableCollection<Cell>();
                for (var j = 0; j < 8; j++)
                {
                    row.Add(count % 2 == 0
                            ? new Cell(j,i,@"D:\1FACULTATE\Anu II\MAP\MAP-Checkers\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_Empty.png",
                                @"D:\1FACULTATE\Anu II\MAP\MAP-Checkers\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_WhitePiece.png",
                                @"D:\1FACULTATE\Anu II\MAP\MAP-Checkers\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_BlackPiece.png")
                            : new Cell(j,i,@"D:\1FACULTATE\Anu II\MAP\MAP-Checkers\Checkers\bin\Debug\net8.0-windows\Images\WhiteSpace_Empty.png"));
                    row[j].CurrentImage = row[j].BackgroundEmptyPath;
                    count++;
                }
                for (var j = 0; j < 8; j++)
                {
                    if (i < 3)
                    {
                        if (row[j].BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                        {
                            row[j].CurrentImage = row[j].BlackPiece;
                            row[j].CurrentState = State.BlackPiece;
                            continue;
                        }
                        row[j].CurrentImage = row[j].BackgroundEmptyPath;
                    }
                    else if (i > 4)
                    {
                        if (row[j].BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                        {
                            row[j].CurrentImage = row[j].WhitePiece;
                            row[j].CurrentState = State.WhitePiece;
                            continue;
                        }
                        row[j].CurrentImage = row[j].BackgroundEmptyPath;
                    }
                    row[j].CurrentImage = row[j].BackgroundEmptyPath;
                }
                board.Add(row);
                count--;
            }
            return board;
        }

    }
}