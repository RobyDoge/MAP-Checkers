using Checkers.Model;
using System.Windows.Input;
using Checkers.Commands;
using Checkers.Services;

namespace Checkers.ViewModel
{
    public class CellVM
    {
        private GameLogic GLogic { get; set; }
        public CellVM(string backgroundEmptyPath,GameLogic GLogic, params string[] backgroundPiecePath )
        {
            SimpleCell = new Cell(backgroundEmptyPath, backgroundPiecePath);
            this.GLogic = GLogic;
        }

        public Cell SimpleCell { get; set; }

        private ICommand _clickCommand;

        public ICommand ClickCommand
        {
            get
            {
                if (_clickCommand == null)
                {
                    _clickCommand = new RelayCommand<Cell>(GLogic.ClickAction);
                }
                return _clickCommand;
            }
        }
    }
}
