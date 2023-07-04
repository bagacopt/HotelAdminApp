using LiveCharts.Defaults;
using LiveCharts;
using PAP___RECEPTIONIST_HOTEL.Core;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Collections.ObjectModel;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int typeUser;
        private object _currentView;
        private SeriesCollection _seriesCollection;
        private List<string> _labels;
        private ChartValues<ObservableValue> _series1Values, _series2Values;

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

        public SeriesCollection SeriesCollection
        {
            get { return _seriesCollection; }
            set
            {
                _seriesCollection = value;
                OnPropertyChanged("SeriesCollection");
            }
        }

        public List<string> Labels
        {
            get { return _labels; }
            set
            {
                _labels = value;
                OnPropertyChanged("Labels");
            }
        }

        public ChartValues<ObservableValue> Series1Values
        {
            get { return _series1Values; }
            set
            {
                _series1Values = value;
                OnPropertyChanged("Series1Values");
            }
        }

        public ChartValues<ObservableValue> Series2Values
        {
            get { return _series2Values; }
            set
            {
                _series2Values = value;
                OnPropertyChanged("Series2Values");
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
    }
}