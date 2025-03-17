using System;
using Project.Core;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        
        public RelayCommand HomeViewCommand { get; set; }
        public HomeViewModel HomeViewModel { get; set; }

        public RelayCommand ArtikelViewCommand { get; set; }
        public ArtikelViewModel ArtikelViewModel { get; set; }

        public RelayCommand SettingsViewCommand { get; set; }
        public SettingsViewModel SettingsViewModel { get; set; }

        private object _currentView;
        public object CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged(nameof(CurrentView));
            }
        }

        public MainViewModel()
        {
            HomeViewModel = new HomeViewModel();
            ArtikelViewModel = new ArtikelViewModel();
            SettingsViewModel = new SettingsViewModel();

            CurrentView = HomeViewModel;

            HomeViewCommand = new RelayCommand(o =>
            {
                CurrentView = HomeViewModel;
            });

            ArtikelViewCommand = new RelayCommand(o => 
            {
                CurrentView = ArtikelViewModel;
            });

            SettingsViewCommand = new RelayCommand(o => 
            {
                CurrentView = SettingsViewModel;
            });
        }
    }
}
