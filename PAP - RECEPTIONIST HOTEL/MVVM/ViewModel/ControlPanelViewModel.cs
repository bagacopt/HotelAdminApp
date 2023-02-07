using PAP___RECEPTIONIST_HOTEL.Core;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    internal class ControlPanelViewModel : ObservableObject
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


        public ControlPanelViewModel()
        {
            ControlVM = new ControlPanelViewModel();

            
        }
    }
}
