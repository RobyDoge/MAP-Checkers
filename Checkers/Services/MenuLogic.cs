
using System.Collections.ObjectModel;
using Checkers.XMLHandlers;

namespace Checkers.Services
{
    internal class MenuLogic
    {
        public bool MultipleJumpsAllowed { get; set; }
        public ObservableCollection<string> Statistics { get; internal set; }

        public MenuLogic()
        {
            MultipleJumpsAllowed = MultipleJumpsHandler.GetMultipleJumps();
            CreateStatistics();
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

        internal void UpdateStatistic(string notUsed)
        {
            if (Statistics == null) return;
            CreateStatistics();
        }

        private void CreateStatistics()
        {
            var statisticsList = StatisticsHandler.GetStatistics();
            if(statisticsList == null)
            {
                Statistics = new ObservableCollection<string>();
                return;
            }
            if (Statistics == null)
            {
                Statistics = new ObservableCollection<string>();
            }
            Statistics.Add($"White wins: {statisticsList[0]}\nMax white pieces: {statisticsList[1]}");
            Statistics.Add($"Black wins: {statisticsList[2]}\nMax black pieces: {statisticsList[3]}");
        }
    }
}
