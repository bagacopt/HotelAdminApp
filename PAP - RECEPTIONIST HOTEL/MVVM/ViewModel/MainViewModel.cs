using PAP___RECEPTIONIST_HOTEL.Core;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public ControlPanelViewModel ControlVM { get; set; }

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
            CurrentView = ControlVM;
        }
    }
}
