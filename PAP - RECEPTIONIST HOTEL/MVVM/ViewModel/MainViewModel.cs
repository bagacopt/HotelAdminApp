using PAP___RECEPTIONIST_HOTEL.Core;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        // CONNECTION
        SqlConnection con = new SqlConnection("Data Source=BAGACINHO;Initial Catalog=reservas_PAP;Integrated Security=True");

        // VARIABLES
        string data;
        int typeUser;

        // ------------------------------------ RelayCommand ---------------------------------------- //

        public RelayCommand ControlPanelViewCommand { get; set; }

        public RelayCommand AdminControlPanelViewCommand { get; set; }

        public RelayCommand RequestsViewCommand { get; set; }

        public RelayCommand ManageReservationsViewCommand { get; set; }

        public RelayCommand ManageRequestsViewCommand { get; set; }

        public RelayCommand ManageUsersViewCommand { get; set; }

        // ------------------------------------- ViewModel ----------------------------------------- //

        public ControlPanelViewModel ControlPanelVM { get; set; }

        public AdminControlPanelViewModel AdminControlPanelVM { get; set; }

        public RequestsViewModel RequestsVM { get; set; }

        public ManageReservationsViewModel ManageReservationsVM { get; set; }

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
            // CALLS OF THE VIEWMODELS
            ControlPanelVM = new ControlPanelViewModel();
            AdminControlPanelVM = new AdminControlPanelViewModel();
            RequestsVM = new RequestsViewModel();
            ManageReservationsVM = new ManageReservationsViewModel();
            ManageRequestsVM = new ManageRequestsViewModel();
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

            // ----------------------------------- ViewCommand ---------------------------------------- //
            ControlPanelViewCommand = new RelayCommand(o => { CurrentView = ControlPanelVM; });

            AdminControlPanelViewCommand = new RelayCommand(o => { CurrentView = AdminControlPanelVM; });

            RequestsViewCommand = new RelayCommand(o => { CurrentView = RequestsVM; });

            ManageReservationsViewCommand = new RelayCommand(o => { CurrentView = ManageReservationsVM; });

            ManageRequestsViewCommand = new RelayCommand(o => { CurrentView = ManageRequestsVM; });

            ManageUsersViewCommand = new RelayCommand(o => { CurrentView = ManageUsersVM; });
        }
    }
}
