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
        public ObservableCollection<GameLogic.Player> CurrentPlayer => GLogic.CurrentPlayer;
        public ObservableCollection<int> WhitePiecesNumber => GLogic.WhitePiecesNumber;
        public ObservableCollection<int> BlackPiecesNumber => GLogic.BlackPiecesNumber;

        public GameVM()
        {
            var board = Helper.InitGameBoard();
            GLogic = new GameLogic(board, MultipleJumpsHandler.GetMultipleJumps());
            GameBoard = CellBoardToCellVMBoard(board);
        }

        public GameVM(string gameName)
        {
            var gameInfo = SavedGamesHandler.LoadGame(gameName);
            GLogic = new GameLogic(gameInfo.Board,gameInfo.MultipleJumps,gameInfo.PlayerTurn,gameInfo.WhitePiecesNumber,gameInfo.BlackPiecesNumber);
            GameBoard = CellBoardToCellVMBoard(gameInfo.Board);
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



        private ICommand _saveGame;
    }

}
