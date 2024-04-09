using System.Collections.ObjectModel;
using Checkers.Commands;
using System.Windows.Input;
using Checkers.Model;
using Checkers.Services;
using Checkers.XMLHandlers;
namespace Checkers.ViewModel
{
    public class GameVM
    {
        public ObservableCollection<ObservableCollection<CellVM>> GameBoard { get; set; }

        private GameLogic GLogic { get; set; }

        public GameVM()
        {
            var board = Helper.InitGameBoard();

            GLogic = new GameLogic(board, MultipleJumpsHandler.GetMultipleJumps());
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

        private ICommand _saveGame;

        public ICommand SaveGame
        {
            get
            {
                if (_saveGame == null)
                {
                    _saveGame = new RelayCommand<string>(GLogic.SaveGameAction);
                }
                return _saveGame;
            }
        }

    }

}
