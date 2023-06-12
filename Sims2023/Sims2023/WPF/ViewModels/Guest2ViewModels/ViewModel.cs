using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Sims2023.WPF.ViewModels.Guest2ViewModels
{
    public class ViewModel : INotifyPropertyChanged
    {
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}

