
using System.Collections.ObjectModel;
using Checkers.XMLHandlers;

namespace Checkers.Services
{
    internal class MenuLogic
    {
        public bool MultipleJumpsAllowed { get; set; }

        public MenuLogic()
        {
            MultipleJumpsAllowed = MultipleJumpsHandler.GetMultipleJumps();
        }

        public void ChangeMultipleJumps(bool multipleJumpsAllowed)
        {
            MultipleJumpsAllowed = multipleJumpsAllowed;
            MultipleJumpsHandler.ChangeMultipleJumps(multipleJumpsAllowed);
        }

        public ObservableCollection<string>? GetSavedGamesName()
        {
            return SavedGamesHandler.GetSavedGamesName();
        }
    }
}
