using PAP___RECEPTIONIST_HOTEL.Core;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        readonly string data;
        readonly int typeUser;
        private object _currentView;

        // ------------------------------------ RelayCommand ---------------------------------------- //

        // CLIENT
        public RelayCommand ControlPanelViewCommand { get; set; }

        public RelayCommand RequestsViewCommand { get; set; }

        public RelayCommand ManageRequestsViewCommand { get; set; }

        // ADMIN
        public RelayCommand AdminControlPanelViewCommand { get; set; }

        public RelayCommand ManageReservationsViewCommand { get; set; }

        public RelayCommand AdminManageRequestsViewCommand { get; set; }

        public RelayCommand ManageUsersViewCommand { get; set; }

        // ------------------------------------- ViewModel ----------------------------------------- //

        // CLIENT
        public Client.ControlPanelViewModel ControlPanelVM { get; set; }

        public Client.ManageRequestsViewModel ManageRequestsVM { get; set; }

        public Client.RequestsViewModel RequestsVM { get; set; }

        // ADMIN
        public Admin.ControlPanelViewModel AdminControlPanelVM { get; set; }

        public Admin.ManageReservationsViewModel ManageReservationsVM { get; set; }

        public Admin.ManageRequestsViewModel AdminManageRequestsVM { get; set; }

        public Admin.ManageUsersViewModel ManageUsersVM { get; set; }

        // ----------------------------------- CurrentView ---------------------------------------- //

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public object SubCurrentView 
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
            // CALLS OF THE VIEWMODELS
            // CLIENT
            ControlPanelVM = new Client.ControlPanelViewModel();
            ManageRequestsVM = new Client.ManageRequestsViewModel();
            RequestsVM = new Client.RequestsViewModel();

            // ADMIN
            AdminControlPanelVM = new Admin.ControlPanelViewModel();
            ManageReservationsVM = new Admin.ManageReservationsViewModel();
            AdminManageRequestsVM = new Admin.ManageRequestsViewModel();
            ManageUsersVM = new Admin.ManageUsersViewModel();

            // OPEN CONNECTION
            con.Open();

            // SQL QUERY
            data = "SELECT type_user FROM Users WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        typeUser = Convert.ToInt32(reader["type_user"]);
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();

            if (typeUser == 1)
            {
                CurrentView = ControlPanelVM;
            }
            else
            {
                CurrentView = AdminControlPanelVM;
            }

            // ----------------------------------- ViewCommand ---------------------------------------- //
            ControlPanelViewCommand = new RelayCommand(o => {
                if (typeUser == 1)
                {
                    CurrentView = ControlPanelVM;
                }
                else
                {
                    CurrentView = AdminControlPanelVM;
                }
            });

            ManageRequestsViewCommand = new RelayCommand(o => {
                if (typeUser == 1)
                {
                    CurrentView = ManageRequestsVM;
                }
                else
                {
                    CurrentView = AdminManageRequestsVM;
                }
            });

            RequestsViewCommand = new RelayCommand(o => { CurrentView = RequestsVM; });

            ManageReservationsViewCommand = new RelayCommand(o => { CurrentView = ManageReservationsVM; });

            ManageUsersViewCommand = new RelayCommand(o => { CurrentView = ManageUsersVM; });
        }

        private void Increment()
        {

        }
    }
}