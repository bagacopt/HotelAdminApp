using PAP___RECEPTIONIST_HOTEL.Core;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        public RelayCommand ControlViewCommand { get; set; }

        public RelayCommand ReservasViewCommand { get; set; }

        public ControlPanelViewModel ControlVM { get; set; }

        public ReservasViewModel ReservasVM { get; set; }

        private object _currentView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            ControlVM = new ControlPanelViewModel();
            ReservasVM = new ReservasViewModel();

            CurrentView = ControlVM;

            ControlViewCommand = new RelayCommand(o => 
            {
                CurrentView = ControlVM;
            });

            ReservasViewCommand = new RelayCommand(o =>
            {
                CurrentView = ReservasVM;
            });
        }
    }
}
