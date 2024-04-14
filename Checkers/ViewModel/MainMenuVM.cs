using System.Collections.ObjectModel;
using Checkers.Model;
using Checkers.Services;

namespace Checkers.ViewModel
{
    internal class MainMenuVM : BaseNotification
    {
        public bool MultipleJumps { get; set; }
        public ObservableCollection<string> SavedGamesName { get; set; }

        private MenuLogic MLogic { get; set; }
        public MainMenuVM()
        {
            MLogic = new MenuLogic();
            MultipleJumps = MLogic.MultipleJumpsAllowed;
            SavedGamesName = MLogic.GetSavedGamesName() ?? [];
        }
        public void ChangeMultipleJumps(bool multipleJumps)
        {
            MultipleJumps = multipleJumps;
            MLogic.ChangeMultipleJumps(multipleJumps);
        }
    }
}
