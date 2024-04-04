using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Checkers.Model
{ 
    public class BaseNotification : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;
        protected void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}