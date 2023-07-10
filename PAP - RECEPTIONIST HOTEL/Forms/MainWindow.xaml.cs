using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.Forms
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int typeUser;

        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
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

            // "PRIVILEGES" OF EACH TYPE OF USER
            switch (typeUser)
            {
                case 1:
                case 2:
                    ManageReservationsRadionButton.Visibility = Visibility.Collapsed;
                    ManageComplainsRadionButton.Visibility = Visibility.Collapsed;
                    LogoutRadioButton.Margin = new Thickness(0, 355, 0, 0);
                    break;
                default:
                    RequestsRadioButton.Visibility = Visibility.Collapsed;
                    ComplainsRadionButton.Visibility = Visibility.Collapsed;
                    ManageComplainsRadionButton.Visibility = Visibility.Collapsed;
                    LogoutRadioButton.Margin = new Thickness(0, 425, 0, 0);
                    break;
            }

            Application.Current.MainWindow = this;
        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // CLOSES APPLICATION
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // LET THE USER MOVE THE WINDOW FROM WHENEVER HE WANTS
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void Logout_Checked(object sender, RoutedEventArgs e)
        {
            // OPENS LOGIN FORM
            Login login = new Login();
            Close();
            login.Show();
        }

        private void GoBack(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape || e.Key == Key.Delete)
            {
                // OPENS LOGIN FORM
                Login login = new Login();
                Close();
                login.Show();
            }
        }
    }
}