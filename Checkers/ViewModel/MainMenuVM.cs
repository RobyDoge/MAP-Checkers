using System.Collections.ObjectModel;
using Checkers.Commands;
using System.Windows.Input;
using Checkers.Model;
using Checkers.Services;

namespace Checkers.ViewModel
{
    internal class MainMenuVM : BaseNotification
    {
        public bool MultipleJumps { get; set; }
        public ObservableCollection<string> SavedGamesName { get; set; }

        public ObservableCollection<string> Statistics { get; set; }

        private MenuLogic MLogic { get; set; }
        public MainMenuVM()
        {
            MLogic = new MenuLogic();
            Statistics = MLogic.Statistics;
            MultipleJumps = MLogic.MultipleJumpsAllowed;
            SavedGamesName = MLogic.GetSavedGamesName() ?? [];
        }
        public void ChangeMultipleJumps(bool multipleJumps)
        {
            MultipleJumps = multipleJumps;
            MLogic.ChangeMultipleJumps(multipleJumps);
        }

        public ICommand UpdateStatistics
        {
            get
            {
                if (_updateStatistics == null)
                {
                    _updateStatistics = new RelayCommand<string>(MLogic.UpdateStatistic);
                }
                return _updateStatistics;
            }
        }



        private ICommand _updateStatistics;

    }
}
