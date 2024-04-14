using System.Collections.ObjectModel;
using Checkers.Model;

namespace Checkers.XMLHandlers
{
    internal class SGH_ReturnType
    {
        public string PlayerTurn { get; set; }
        public bool MultipleJumps { get; set; }
        public ObservableCollection<ObservableCollection<Cell>> Board { get; set; }
        public int WhitePiecesNumber { get; set; }
        public int BlackPiecesNumber { get; set; }

        public SGH_ReturnType()
        {
        }

    }
}
