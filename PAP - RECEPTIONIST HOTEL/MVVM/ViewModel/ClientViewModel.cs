using PAP___RECEPTIONIST_HOTEL.Core;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class ClientViewModel : ObservableObject
    {
        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        readonly string data;
        readonly int typeUser;
        private object _currentView;

        // ------------------------------------ RelayCommand ---------------------------------------- //

        public RelayCommand ControlPanelViewCommand { get; set; }

        public RelayCommand RequestsViewCommand { get; set; }

        public RelayCommand ManageReservationsViewCommand { get; set; }

        public RelayCommand ManageRequestsViewCommand { get; set; }

        public RelayCommand ManageUsersViewCommand { get; set; }

        // ------------------------------------- ViewModel ----------------------------------------- //

        public ControlPanelViewModel ControlPanelVM { get; set; }

        public AdminControlPanelViewModel AdminControlPanelVM { get; set; }

        public RequestsViewModel RequestsVM { get; set; }

        public ManageReservationsViewModel ManageReservationsVM { get; set; }

        public AdminManageRequestsViewModel ManageRequestsVM { get; set; }

        public ManageUsersViewModel ManageUsersVM { get; set; }

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

        public ClientViewModel()
        {
            // CALLS OF THE VIEWMODELS
            ControlPanelVM = new ControlPanelViewModel();
            AdminControlPanelVM = new AdminControlPanelViewModel();
            RequestsVM = new RequestsViewModel();
            ManageReservationsVM = new ManageReservationsViewModel();
            ManageRequestsVM = new AdminManageRequestsViewModel();
            ManageUsersVM = new ManageUsersViewModel();

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

            RequestsViewCommand = new RelayCommand(o => { CurrentView = RequestsVM; });

            ManageReservationsViewCommand = new RelayCommand(o => { CurrentView = ManageReservationsVM; });

            ManageRequestsViewCommand = new RelayCommand(o => { CurrentView = ManageRequestsVM; });

            ManageUsersViewCommand = new RelayCommand(o => { CurrentView = ManageUsersVM; });
        }
    }
}
