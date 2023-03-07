using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data;
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

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary> 
    /// Interaction logic for Reservas.xaml
    /// </summary>
    public partial class ManageReservations : UserControl
    {
        public ManageReservations()
        {
            InitializeComponent();
        }

        string data, user, id, client_id;

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-LQBQ1HM;Initial Catalog=reservas_PAP;Integrated Security=True");

        private void ManageReservations_Loaded(object sender, RoutedEventArgs e)
        {
            con.Open();

            data = "SELECT * FROM Users WHERE type_user = 3 AND username = @username";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@username", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        user = reader["username"].ToString();
                        id = reader["id_user"].ToString();

                        usernameTxtBox.Text = user;
                        idReservaTxtBox.Text = id;
                    }
                }
            }

            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users WHERE type_user = 1", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                reservationcombo.Items.Add(i + 1 + " - " + dt.Rows[i]["username"]);
            }

            con.Close();
        }

        private void ComboBoxSelectClient(object sender, SelectionChangedEventArgs e)
        {

            // OPEN CONNECTION
            con.Open();

            // GET USERNAME OF THE CLIENT AND SHOW THE RESERVATION ID
            data = "SELECT * FROM Users WHERE type_user = 1 AND username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                client_id = reservationcombo.SelectedValue.ToString();

                string[] temp_client = client_id.Split('-');
                client_id = temp_client[1].Trim();

                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idReservationTxtBox.Text = reader["id_reservation"].ToString();
                    }
                }
            }

            // GET NAME OF THE CLIENT
            data = "SELECT * FROM Users WHERE type_user = 1 AND username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nClienteTxtBox.Text = reader["fullname"].ToString();
                    }
                }
            }

            // GET NUMBER OF THE ROOM
            data = "SELECT Rooms.n_room FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id_room = Reservations.id_room INNER JOIN Users ON " +
                "Reservations.id_reservation = Users.id_reservation WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nRoom = reader["n_room"].ToString();

                        nRoomTxtBox.Text = nRoom;
                    }
                }
            }

            // GET CHECK-IN AND CHECK-OUT OF THE RESERVATION
            data = "SELECT FORMAT(Reservations.[check-in], 'dd/MM/yy | hh:mm tt') AS 'check-in'," +
                "FORMAT(Reservations.[check-out], 'dd/MM/yy | hh:mm tt') AS 'check-out'" +
                "FROM Reservations INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation " +
                "WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())

                    {
                        string checkin = reader["check-in"].ToString();
                        string checkout = reader["check-out"].ToString();

                        checkinTxtBox.Text = checkin;
                        checkoutTxtBox.Text = checkout;
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }
    }
}
