using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.PrimaryView
{
    public partial class Complains : UserControl
    {
        public Complains()
        {
            InitializeComponent();
        }

        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int id;

        // OBJECTS

        private void Complains_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "SELECT username FROM Users INNER JOIN Reservations " +
                "ON Users.reservation_id = Reservations.id INNER JOIN Rooms " +
                "ON Reservations.rooms_id = Rooms.id WHERE Rooms.n_room = @nRoom";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@nRoom", ControlPanel.nRoom);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        usernameLabel.Content = reader["username"].ToString();
                    }
                }   
            }

            // CLOSE CONNECTION
            con.Close();

            nRoomLabel.Content = ControlPanel.nRoom;
        }

        private void SendComplain_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "SELECT id FROM Users WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        id = Convert.ToInt32(reader["id"]);
                    }
                }
            }

            data = "INSERT INTO Complains (title, date_complain, description, id_user) VALUES (@title, @date, @desc, @idUser)";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@title", titleTextBox.Text);
                cmd.Parameters.AddWithValue("@date", dateComplainDateTime.Value);
                cmd.Parameters.AddWithValue("@desc", descriptionTextBox.Text);
                cmd.Parameters.AddWithValue("@idUser", id);

                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Reclamação foi realizada com sucesso!", "Alerta", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Ocorreu um erro e a reclamação não pôde ser realizada", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            
            // CLOSE CONNECTION
            con.Close();
        }
    }
}
