using System.Collections.ObjectModel;
using System.Collections.Specialized;
using Checkers.Model;

namespace Checkers.Services
{
    public class Helper
    {
        public static Cell CurrentCell { get; set; }
        public static Cell PreviousCell { get; set; }

        public static ObservableCollection<ObservableCollection<Cell>> InitGameBoard()
        {
            var board = new ObservableCollection<ObservableCollection<Cell>>();
            var count = 0;
            for (var i = 0; i < 8; i++)
            {
                var row = new ObservableCollection<Cell>();
                for (var j = 0; j < 8; j++)
                {
                    row.Add(count % 2 == 0
                            ? new Cell(@"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_Empty.png",
                                @"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_BlackPiece.png",
                                @"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\BlackSpace_WhitePiece.png")
                            : new Cell(@"D:\AN2-SEM2\MAP\MAP-Tema2\Checkers\bin\Debug\net8.0-windows\Images\WhiteSpace_Empty.png"));
                    row[j].CurrentImage = row[j].BackgroundEmptyPath;
                    count++;
                }
                board.Add(row);
                count--;
            }

            //count = 0;
            //for(var i = 0; i< 8;i++)
            //{
            //    for(var j = 0; j< 8; j++)
            //    {
            //        if (i < 2 && board[i][j].BackgroundEmptyPath.Contains("BlackSpace_Empty"))
            //        {
            //            board[i][j].CurrentImage = board[i][j].BlackPiece;
            //        }
            //        if (i > 5 && board[i][j].BackgroundEmptyPath.Contains("BlackSpace_Empty"))
            //        {
            //            board[i][j].CurrentImage = board[i][j].WhitePiece;
            //        }
            //    }
            //}

            return board;
        }

    }
}