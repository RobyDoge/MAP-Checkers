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
                    //if (i is < 2 or > 5)
                    //{
                    //    row.Add(count % 2 == 0
                    //        ? new Cell("Images/BlackSpace_Empty.png", 
                    //            "Images/BlackSpace_Empty_BlackPiece.png",
                    //            "Images/BlackSpace_Empty_WhitePiece.png")
                    //        : new Cell("Images/WhiteSpace_Empty.png"));
                    //}
                    //else
                    //{
                    //    row.Add(count % 2 == 0
                    //        ? new Cell("Images/BlackSpace_Empty.png")
                    //        : new Cell("Images/WhiteSpace_Empty.png"));
                    //}
                    if (i is < 2 or > 5)
                    {
                        row.Add(count % 2 == 0
                            ? new Cell("/Checkers;component/images/blackspace_empty.png",
                                "/Checkers;component/images/blackspace_blackpiece.png",
                                "/Checkers;component/images/blackspace_whitePiece.png")
                            : new Cell("/Checkers;component/images/whitespace_empty.png"));
                    }
                    else
                    {
                        row.Add(count % 2 == 0
                            ? new Cell("/Checkers;component/images/blackspace_empty.png")
                            : new Cell("/Checkers;component/images/whitespace_empty.png"));
                    }
                    count++;
                }
                board.Add(row);
            }

            return board;
        }

    }
}