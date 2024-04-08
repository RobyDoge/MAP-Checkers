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
            bool multipleJumps = false;
            GLogic = new GameLogic(board,false);
            GameBoard = CellBoardToCellVMBoard(board);
        }

        private ObservableCollection<ObservableCollection<CellVM>> CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            var boardVM = new ObservableCollection<ObservableCollection<CellVM>>();
            var count = 0;
            var rowNum = 0;
            var colNum = 0;
            foreach (var row in board)
            {

                var rowVM = new ObservableCollection<CellVM>();
                foreach (var cell in row)
                {
                    rowVM.Add(new CellVM(colNum,rowNum,cell.BackgroundEmptyPath, GLogic, cell.WhitePiece, cell.BlackPiece));
                    colNum++;
                }
                colNum = 0;
                rowNum++;
                for(var i=0; i<8;i++)
                {
                    if(count<3) 
                    {
                        if (rowVM[i].SimpleCell.BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                        {
                            rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.BlackPiece;
                            rowVM[i].SimpleCell.CurrentState = State.BlackPiece;
                            continue;
                        }
                        rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.BackgroundEmptyPath;
                    }
                    else if(count>4)
                    {
                        if (rowVM[i].SimpleCell.BackgroundEmptyPath.Contains("BlackSpace_Empty"))
                        {
                            rowVM[i].SimpleCell.CurrentImage = rowVM[i].SimpleCell.WhitePiece;
                            rowVM[i].SimpleCell.CurrentState = State.WhitePiece;
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
