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

        private ObservableCollection<ObservableCollection<CellVM>>? CellBoardToCellVMBoard(ObservableCollection<ObservableCollection<Cell>> board)
        {
            var boardVM = new ObservableCollection<ObservableCollection<CellVM>>();
            foreach (var row in board)
            {
                var rowVM = new ObservableCollection<CellVM>();
                foreach (var cell in row)
                {
                    rowVM.Add(new CellVM(cell.BackgroundEmptyPath, GLogic, cell.WhitePiece, cell.BlackPiece));
                }
                boardVM.Add(rowVM);
            }
            return boardVM;


        }

        
    }

}
