using System.Windows.Input;
using Checkers.Commands;
using Checkers.Services;
using Checkers.XMLHandlers;

namespace Checkers.ViewModel
{
    internal class MainMenuVM
    {
        public bool MultipleJumps { get; set; }
        private MenuLogic MLogic { get; set; }
        public MainMenuVM()
        {
            MLogic = new MenuLogic();
            MultipleJumps = MLogic.MultipleJumpsAllowed;
        }

        public ICommand ChangeMultipleJumps
        {
            get
            {
                if(_changeMultipleJumps == null)
                    _changeMultipleJumps = new RelayCommand<bool>(MLogic.ChangeMultipleJumps);
                return _changeMultipleJumps;
            }
        }
        
        private ICommand _changeMultipleJumps;
    }
}
