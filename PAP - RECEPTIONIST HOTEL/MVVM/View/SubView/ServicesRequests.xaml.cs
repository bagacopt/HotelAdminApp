using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for ServicesRequests.xaml
    /// </summary>
    public partial class ServicesRequests : Window
    {
        public ServicesRequests()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string id;
        string data;

        private void ServicesRequests_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            nRoomLabel.Content = ControlPanel.n_Quarto;
            titleLabel.Content = Requests.serviceName;

            data = "SELECT id_room FROM Rooms WHERE n_room = @nRoom";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@nRoom", ControlPanel.n_Quarto);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        id = reader["id_room"].ToString();
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {

            // CLOSE THE FORM
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }



        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // LET THE USER MOVE THE WINDOW FROM WHENEVER HE WANTS
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}
