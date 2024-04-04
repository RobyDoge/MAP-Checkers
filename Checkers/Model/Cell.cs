
namespace Checkers.Model
{
    public class Cell : BaseNotification
    {
        public Cell(string backgroundEmptyPath, params string[] backgroundPiecePath)
        {
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
                NotifyPropertyChanged("BackgroundEmptyPath");
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



        private string _backgroundEmptyPath;
        private string _whitePiece;
        private string _blackPiece;


    }
}