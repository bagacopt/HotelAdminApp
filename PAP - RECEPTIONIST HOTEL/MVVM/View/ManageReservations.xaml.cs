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

        // CONNECTION
        SqlConnection con = new SqlConnection("Data Source=BAGACINHO;Initial Catalog=reservas_PAP;Integrated Security=True");

        // VARIABLES
        string data, client_id, nRoom;

        private void ManageReservations_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // GET ADMINS
            data = "SELECT * FROM Users WHERE type_user = 3 AND username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        usernameTxtBox.Text = reader["username"].ToString();
                        idReservaTxtBox.Text = reader["id_user"].ToString();
                    }
                }
            }


            // GET CLIENTS
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM Users WHERE type_user = 1", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                reservationcombo.Items.Add(i + 1 + " - " + dt.Rows[i]["username"]);
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void ComboBoxSelectClient(object sender, SelectionChangedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // SHOW RESERVATION ID BY CLIENT USERNAME
            data = "SELECT * FROM Users WHERE type_user = 1 AND username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                string[] temp_str = reservationcombo.SelectedValue.ToString().Split('-');
                client_id = temp_str[1].Trim();

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
            data = "SELECT FORMAT(Reservations.[check-in], 'dd/MM/yy | HH:mm') AS 'check-in'," +
                "FORMAT(Reservations.[check-out], 'dd/MM/yy | HH:mm') AS 'check-out'" +
                "FROM Reservations INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation " +
                "WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        checkinTxtBox.Text = reader["check-in"].ToString();
                        checkoutTxtBox.Text = reader["check-out"].ToString();
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void ChangeNumberRoom_Click(object sender, RoutedEventArgs e)
        {
            changeRoomComboBox.Visibility = Visibility.Visible;

            // OPEN CONNECTION
            con.Open();

            // SQL QUERY
            data = "SELECT n_room FROM Rooms WHERE available = 1";

            // PUT VALUES ON THE COMBO BOX
            SqlDataAdapter da = new SqlDataAdapter(data, con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                changeRoomComboBox.Items.Add(dt.Rows[i]["n_room"]);
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void changeRoomSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            con.Open();

            //
            // PARA ESTUDAR MELHOR QUE TA CAGADO
            //
            /* 
            data = "SELECT * FROM Rooms INNER JOIN Reservations ON Rooms.id_room = Reservations.id_room " +
                "INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nRoom = reader["n_room"].ToString();
                    }
                }
            } */

            //
            // PARA ESTUDAR MELHOR QUE TA CAGADO
            //



            // SQL QUERY
            data = "UPDATE Reservations SET id_room = @idRoom FROM Rooms INNER JOIN Reservations " +
                    "ON Rooms.id_room = Reservations.id_room INNER JOIN Users " +
                    "ON Reservations.id_reservation = Users.id_reservation WHERE Users.username = @user AND Rooms.n_room = @nRoom";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);
                cmd.Parameters.AddWithValue("@nRoom", changeRoomComboBox.SelectedValue);
            }



            data = "UPDATE Rooms SET available = 0 FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id_room = Reservations.id_room INNER JOIN Users " +
                "ON Reservations.id_reservation = Users.id_reservation WHERE username = @user AND Rooms.n_room = @nRoom";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);
                cmd.Parameters.AddWithValue("@nRoom", nRoom);
            }

            // CLOSE CONNECTION
            con.Close();
        }
    }
}
