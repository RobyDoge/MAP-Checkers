using Checkers.Model;
using System.Collections.ObjectModel;

namespace Checkers.Services
{
    public class GameLogic
    {
        private ObservableCollection<ObservableCollection<Cell>> Cells { get; set; }
        public GameLogic(ObservableCollection<ObservableCollection<Cell>> cells)
        {
            Cells = cells;
        }
        public void ClickAction(Cell cell)
        {
            throw new NotImplementedException();
        }

    }
}
