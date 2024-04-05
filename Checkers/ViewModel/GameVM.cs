using System.Collections.ObjectModel;
using Checkers.Model;
using Checkers.Services;

namespace Checkers.ViewModel
{
    public class GameVM
    {
        public ObservableCollection<ObservableCollection<CellVM>> GameBoard { get; set; }

        private GameLogic GLogic { get; set; }

        public GameVM()
        {
            var board = Helper.InitGameBoard();
            GLogic = new GameLogic(board);
            GameBoard = CellBoardToCellVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            var boardVM = new ObservableCollection<ObservableCollection<CellVM>>();
            var count = 0;
            foreach (var row in board)
            {
                var rowVM = new ObservableCollection<CellVM>();
                foreach (var cell in row)
                {
                    rowVM.Add(new CellVM(cell.BackgroundEmptyPath, GLogic, cell.WhitePiece, cell.BlackPiece));
                }
                for(var i=0; i<8;i++)
                {
                    if(count<3) 
                    {
                        if (rowVM[i].SimpleCell.BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                        {
                            rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.BlackPiece;
                            continue;
                        }
                        rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.BackgroundEmptyPath;
                    }
                    else if(count>4)
                    {
                        if (rowVM[i].SimpleCell.BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                        {
                            rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.WhitePiece;
                            continue;
                        }
                        rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.BackgroundEmptyPath;
                    }
                    rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.BackgroundEmptyPath;
                }
                boardVM.Add(rowVM);
                count++;
            }
            return boardVM;


        }

        
    }

}
