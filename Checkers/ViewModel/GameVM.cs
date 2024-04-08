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
            foreach (var row in board)
            {
                var rowVM = new ObservableCollection<CellVM>();
                foreach (var cell in row)
                {
                    rowVM.Add(new CellVM(cell,GLogic));
                }
                boardVM.Add(rowVM);
            }
            return boardVM;
        }

    }

}
