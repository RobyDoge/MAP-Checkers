
namespace Checkers.Model
{
    public class Cell : BaseNotification
    {
        public Cell(int x, int y,string backgroundEmptyPath, params string[] backgroundPiecePath)
        {
            X = x;
            Y = y;
            BackgroundEmptyPath = backgroundEmptyPath;
            if (backgroundPiecePath.Length != 2)
                return;
            WhitePiece = backgroundPiecePath[0];
            BlackPiece = backgroundPiecePath[1];
        }

        public string BackgroundEmptyPath
        {
            get => _backgroundEmptyPath;
            set
            {
                _backgroundEmptyPath = value;
                NotifyPropertyChanged(nameof(BackgroundEmptyPath));
            }
        }
        public string WhitePiece
        {
            get => _whitePiece;
            set
            {
                _whitePiece = value;
                NotifyPropertyChanged("WhitePiece");
            }
        }
        public string BlackPiece
        {
            get => _blackPiece;
            set
            {
                _blackPiece = value;
                NotifyPropertyChanged("BlackPiece");
            }
        }
        public string CurrentImage
        {
            get => _currentImage;
            set
            { 
                _currentImage = value;
                NotifyPropertyChanged("CurrentImage");
            }
        }
        public State CurrentState
        {
            get => _currentState;
            set
            {
                _currentState = value;
                NotifyPropertyChanged("CurrentState");
            }
        }
        public int X { get; set; }
        public int Y { get; set; }
        
        private State _currentState;
        private string _currentImage;
        private string _backgroundEmptyPath;
        private string _whitePiece;
        private string _blackPiece;
    }

    public enum State
    {
        Empty,
        WhitePiece,
        BlackPiece
    }
}