using PAP___RECEPTIONIST_HOTEL.Core;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {

        // ------------------------------------ RelayCommand ---------------------------------------- //

        public RelayCommand ControlViewCommand { get; set; }

        public RelayCommand ManageReservationsViewCommand { get; set; }

        public RelayCommand RequestsViewCommand { get; set; }

        public RelayCommand ManageRequestsViewCommand { get; set; }

        public RelayCommand ManageUsersViewCommand { get; set; }

        // ------------------------------------- ViewModel ----------------------------------------- //

        public ControlPanelViewModel ControlVM { get; set; }

        public ManageReservationsViewModel ReservasVM { get; set; }

        public RequestsViewModel RequestsVM { get; set; }

        public ManageRequestsViewModel ManageRequestsVM { get; set; }

        public ManageUsersViewModel ManageUsersVM { get; set; }

        // ----------------------------------- CurrentView ---------------------------------------- //

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
            ReservasVM = new ManageReservationsViewModel();
            RequestsVM = new RequestsViewModel();
            ManageRequestsVM = new ManageRequestsViewModel();
            ManageUsersVM = new ManageUsersViewModel();

            CurrentView = ControlVM;

            ControlViewCommand = new RelayCommand(o => { CurrentView = ControlVM; });

            ManageReservationsViewCommand = new RelayCommand(o => { CurrentView = ReservasVM; });

            RequestsViewCommand= new RelayCommand(o => { CurrentView = RequestsVM; });

            ManageRequestsViewCommand= new RelayCommand(o => { CurrentView = ManageRequestsVM; });

            ManageRequestsViewCommand= new RelayCommand(o => { CurrentView= ManageUsersVM; });
        }
    }
}
