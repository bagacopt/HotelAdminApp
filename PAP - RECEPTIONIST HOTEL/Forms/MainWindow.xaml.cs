using PAP___RECEPTIONIST_HOTEL.Core;
using PAP___RECEPTIONIST_HOTEL.MVVM.View;
using PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using static System.Windows.Input.CommandConverter;

namespace PAP___RECEPTIONIST_HOTEL
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int typeUser;
        MainViewModel test = new MainViewModel();

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // CLOSES APPLICATION
            Application.Current.Shutdown();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // MINIMIZES APPLICATION
            WindowState = WindowState.Minimized;
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
            this.Close();
            login.Show();
        }

        private void GoBack(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Escape)
            {
                // OPENS LOGIN FORM
                Login login = new Login();
                this.Close();
                login.Show();
            }
        }

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
            if (typeUser == 1)
            {         
                ManageReservationsRadionButton.Visibility = Visibility.Collapsed;
                ManageRequestsRadioButton.Visibility = Visibility.Collapsed;
                ManageUsersRadionButton.Visibility = Visibility.Collapsed;
                LogoutRadioButton.Margin = new Thickness(0, 490, 0, 0);
            }
            else if (typeUser == 2)
            {

                ManageUsersRadionButton.Visibility = Visibility.Collapsed;
                RequestsRadioButton.Visibility = Visibility.Collapsed;
                LogoutRadioButton.Margin = new Thickness(0, 420, 0, 0);
            }
            else
            {
                RequestsRadioButton.Visibility = Visibility.Collapsed;
                LogoutRadioButton.Margin = new Thickness(0, 350, 0, 0);
            }
        }
    }
}
