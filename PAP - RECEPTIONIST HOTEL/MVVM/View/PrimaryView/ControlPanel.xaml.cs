using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media.Imaging;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : System.Windows.Controls.UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        int nStars;
        string data;
        public static string n_Quarto;

        // 1 STAR RATE
        private void ClassificationStars1_Click(object sender, RoutedEventArgs e)
        {
            setStars(1);
        }

        // 2 STAR RATE
        private void ClassificationStars2_Click(object sender, RoutedEventArgs e)
        {
            setStars(2);
        }

        // 3 STAR RATE
        private void ClassificationStars3_Click(object sender, RoutedEventArgs e)
        {
            setStars(3);
        }

        // 4 STAR RATE
        private void ClassificationStars4_Click(object sender, RoutedEventArgs e)
        {
            setStars(4);
        }

        // 5 STAR RATE
        private void ClassificationStars5_Click(object sender, RoutedEventArgs e)
        {
            setStars(5);
        }

        private void setStars(int rating) // 1 - 5
        {
            List<Image> starList = new List<Image>() { star_1, star_2, star_3, star_4, star_5 };
            nStars = rating;

            for (int i = 1; i < starList.Count + 1; i++)
            {
                if (i <= rating)
                {
                    starList[i - 1].Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
                    continue;
                }

                starList[i -1].Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            }
        }

        private void ControlPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // TEXT OF USERNAME
            usernameTxtBox.Text = Settings.Default.n_cliente;

            // GET CLIENT
            data = "SELECT * FROM Users WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        idReservaTxtBox.Text = reader["reservation_id"].ToString();
                        int stars = Convert.ToInt32(reader["stars"]);
                        nClienteTxtBox.Text = reader["fullname"].ToString();

                        setStars(stars);
                    }
                }
            }

            // GET NUMBER OF THE ROOM
            data = "SELECT Rooms.n_room FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id = Reservations.rooms_id INNER JOIN Users ON " +
                "Reservations.id = Users.reservation_id WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nQuarto = reader["n_room"].ToString();

                        nQuartoTxtBox.Text = nQuarto;
                        n_Quarto = nQuarto;
                    }
                }
            }

            // GET CHECK-IN AND CHECK-OUT OF THE RESERVATION
            data = "SELECT FORMAT(Reservations.[check-in], 'dd/MM/yy | HH:mm') AS 'check-in', " +
                "FORMAT(Reservations.[check-out], 'dd/MM/yy | HH:mm') AS 'check-out' FROM Reservations " +
                "INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation WHERE username = @user";
            
            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        checkinTxtBox.Text = reader["check-in"].ToString();
                        checkoutTxtBox.Text = reader["check-out"].ToString();
                    }
                }
            }

            // GET RESERVATION PRICE
            data = "SELECT Reservations.reserva_price FROM Reservations " +
                "INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation WHERE username = @user";
            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        labelPagamentoTxt.Content = reader["reserva_price"].ToString() + "€";
                    }
                }
            }
            con.Close();
        }

        // RATE HOTEL BUTTON
        private void Ratehotel_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            //UPDATE THE RATING OF THE HOTEL
            data = "UPDATE Users SET stars = @stars WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@stars", nStars);
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);
            }

            // CLOSE CONNECTION
            con.Close();

            System.Windows.MessageBox.Show("Obrigado por ter classificado o nosso hotel com " + nStars + " estrelas! \nSomos muito agradecidos", "Obrigado!!!");
        }
    }
}
