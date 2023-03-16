using PAP___RECEPTIONIST_HOTEL.Core;
using PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel;
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
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Microsoft.VisualBasic;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary> 
    /// Interaction logic for Reservas.xaml
    /// </summary>
    public partial class ManageReservations : System.Windows.Controls.UserControl
    {
        public ManageReservations()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection("Data Source=BAGACINHO;Initial Catalog=reservas_PAP;Integrated Security=True");

        // VARIABLES
        string data, client_id;
        int idRoom, lastIDRoom;

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

            // // INSERT username CONTENT IN reservationComboBox COMBO BOX
            SqlDataAdapter da = new SqlDataAdapter("SELECT username FROM Users WHERE type_user = 1", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                reservationComboBox.Items.Add(i + 1 + " - " + dt.Rows[i]["username"]);
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void ComboBoxSelectClient(object sender, SelectionChangedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // SELECT id_reservation AND fullname OF TABLE Users
            data = "SELECT * FROM Users WHERE type_user = 1 AND username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                // DIVIDES THE reservationComboBox VALUES
                string[] temp_str = reservationComboBox.SelectedValue.ToString().Split('-');

                // GET CLIENT NAME OF THE reservationComboBox VALUE
                client_id = temp_str[1].Trim();

                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idReservationTxtBox.Text = reader["id_reservation"].ToString();
                        nClienteTxtBox.Text = reader["fullname"].ToString();
                    }
                }
            }

            // SELECT n_room OF TABLE Rooms
            data = "SELECT n_room FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id_room = Reservations.id_room INNER JOIN Users ON " +
                "Reservations.id_reservation = Users.id_reservation WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", client_id);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        nRoomTxtBox.Text = reader["n_room"].ToString();
                    }
                }
            }

            // SELECT check-in AND check-out OF TABLE Reservations
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
            // CLEAR OF ITEMS OF THE COMBO BOX
            changeRoomComboBox.Items.Clear();

            // DESIGN AND VISIBILITY CHANGES
            changeRoomComboBox.Visibility = Visibility.Visible;
            ChangeRoomButton.IsEnabled = false;

            // OPEN CONNECTION
            con.Open();

            // SELECT n_room OF TABLE Rooms
            data = "SELECT n_room FROM Rooms WHERE available = 1";

            // INSERT n_room CONTENT IN changeRoomComboBox
            using (SqlDataAdapter da = new SqlDataAdapter(data, con))
            {
                DataTable dt = new DataTable();

                da.Fill(dt);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    changeRoomComboBox.Items.Add(dt.Rows[i]["n_room"]);
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void ClosedDropDownChangeNRoom(object sender, EventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            try
            {
                // SELECT ID ROOM OF Rooms
                data = "SELECT id_room FROM Rooms WHERE n_room = @nRoom";

                using (SqlCommand cmd = new SqlCommand(data, con))
                {
                    cmd.Parameters.AddWithValue("@nRoom", changeRoomComboBox.SelectedValue);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idRoom = Convert.ToInt32(reader["id_room"]);
                        }
                    }
                }

                // lastIDRoom VALUE
                data = "SELECT n_room FROM Rooms INNER JOIN Reservations " +
                    "ON Rooms.id_room = Reservations.id_room INNER JOIN Users " +
                    "ON Reservations.id_reservation = Users.id_reservation WHERE username = @user";

                using (SqlCommand cmd = new SqlCommand(data, con))
                {
                    cmd.Parameters.AddWithValue("@user", client_id);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lastIDRoom = Convert.ToInt32(reader["n_room"]);
                        }
                    }
                }

                // UPDATE AVAILABLE OF Rooms TO 0
                data = "UPDATE Rooms SET available = 0 WHERE n_room = @nRoom";

                using (SqlCommand cmd = new SqlCommand(data, con))
                {
                    cmd.Parameters.AddWithValue("@nRoom", Convert.ToInt32(changeRoomComboBox.SelectedValue));

                    cmd.ExecuteNonQuery();
                }

                // UPDATE AVAILABLE OF Rooms TO 1
                data = "UPDATE Rooms SET available = 1 WHERE n_room = @room";

                using (SqlCommand cmd = new SqlCommand(data, con))
                {
                    cmd.Parameters.AddWithValue("@room", lastIDRoom);
                    cmd.ExecuteNonQuery();
                }

                // UPDATE id_room OF TABLE Reservations
                data = "UPDATE Reservations SET id_room = @idRoom FROM Reservations WHERE id_reservation = @idReservation";

                using (SqlCommand cmd = new SqlCommand(data, con))
                {
                    cmd.Parameters.AddWithValue("@idRoom", idRoom);
                    cmd.Parameters.AddWithValue("@idReservation", idReservationTxtBox.Text);

                    cmd.ExecuteNonQuery();
                }

                // UPDATE VALUE OF nRoomTxtBox 
                nRoomTxtBox.Text = changeRoomComboBox.SelectedValue.ToString();
            }
            catch (SqlException)
            {
                System.Windows.MessageBox.Show("Não clicou em qualquer valor da combo box... Insira novamente", "Aviso");
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void ChangeRoomSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // DESIGN AND VISIBILITY CHANGES
            ChangeRoomButton.IsEnabled = true;
            changeRoomComboBox.Visibility = Visibility.Collapsed;
        }
    }
}
