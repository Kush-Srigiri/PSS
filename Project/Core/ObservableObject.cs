using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Project.Core
{
    // Notifys the Loader, that the Porperty has changed
    class ObservableObject : INotifyPropertyChanged
    {
        // Variables
        public event PropertyChangedEventHandler PropertyChanged;

        // Functions
        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}