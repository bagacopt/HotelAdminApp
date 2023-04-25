using PAP___RECEPTIONIST_HOTEL.Core;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class AdminViewModel : ObservableObject
    {
        // VARIABLES
        private object _currentView;

        // ------------------------------------ RelayCommand ---------------------------------------- //

        public RelayCommand AdminManageRequestsViewCommand { get; set; }

        // ------------------------------------- ViewModel ----------------------------------------- //

        public AdminManageRequestsViewModel AdminManageRequestsVM { get; set; }

        // ----------------------------------- CurrentView ---------------------------------------- //

        public object AdminCurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public AdminViewModel()
        {
            // CALLS OF THE VIEWMODELS
            AdminManageRequestsVM = new AdminManageRequestsViewModel();

            AdminCurrentView = AdminManageRequestsVM;

            // ----------------------------------- ViewCommand ---------------------------------------- //
            AdminManageRequestsViewCommand = new RelayCommand(o => { AdminCurrentView = AdminManageRequestsVM; });
        }
    }
}