using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for MaintenanceRequests.xaml
    /// </summary>
    public partial class MaintenanceRequests : Window
    {
        public MaintenanceRequests()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int idReservation, idRequest;

        private void MaintenanceRequests_Loaded(object sender, RoutedEventArgs e)
        {
            nRoomLabel.Content = ControlPanel.n_Quarto;
            titleLabel.Content = Requests.maintainName;

            // OPEN CONNECTION
            con.Open();

            data = "SELECT Reservations.id FROM Reservations INNER JOIN Users " +
                "ON Reservations.id = Users.reservation_id WHERE Users.username = @user AND Reservations.active = 1";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idReservation = Convert.ToInt32(reader["id"]);
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void MaintainButton_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "INSERT INTO Requests(phone_number, email, [desc], services_id, name, active)" +
                "VALUES(@phone, @email, @desc, 3, @name, 1)";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                cmd.Parameters.AddWithValue("@name", titleLabel.Content);

                cmd.ExecuteNonQuery();
            }

            data = "SELECT id FROM Requests WHERE phone_number = @phone AND email = @email AND [desc] = @desc " +
                "AND services_id = 3 AND name = @name";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                cmd.Parameters.AddWithValue("@name", titleLabel.Content);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idRequest = Convert.ToInt32(reader["id"]);
                    }
                }
            }

            Console.WriteLine(idRequest);

            data = "INSERT INTO Reservations_Requests(reservation_id, request_id) VALUES (@idReservation, @idRequest)";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@idReservation", Convert.ToInt32(idReservation));
                cmd.Parameters.AddWithValue("@idRequest", Convert.ToInt32(idRequest));

                cmd.ExecuteNonQuery();
            }

            // CLOSE CONNECTION
            con.Close();

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
