using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=BAGACINHO;Initial Catalog=reservas_PAP;Integrated Security=True");
        int nStars;

        private void classificationStars1_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 1;
        }

        private void classificationStars2_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 2;
        }

        private void classificationStars3_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 3;
        }

        private void classificationStars4_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 4;
        }

        private void classificationStars5_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));

            nStars = 5;
        }

        private void ControlPanel_Loaded(object sender, RoutedEventArgs e)
        {
            con.Open();

            usernameTxtBox.Text = Settings.Default.n_cliente;

            string data = "SELECT id_reservation, stars, fullname FROM Users WHERE username = @username";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string idReserva = reader["id_reservation"].ToString();
                        string stars = reader["stars"].ToString();
                        string fullname = reader["fullname"].ToString();

                        idReservaTxtBox.Text = idReserva;

                        switch (stars)
                        {
                            case "0":
                                break;
                            case "1":
                                classificationStars1_Click(sender, e);
                                break;
                            case "2":
                                classificationStars2_Click(sender, e);
                                break;
                            case "3":
                                classificationStars3_Click(sender, e);
                                break;
                            case "4":
                                classificationStars4_Click(sender, e);
                                break;
                            case "5":
                                classificationStars5_Click(sender, e);
                                break;
                        }
                        nClienteTxtBox.Text = fullname;
                    }
                }
            }

            data = "SELECT Rooms.n_room FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id_room = Reservations.id_room INNER JOIN Users ON " +
                "Reservations.id_reservation = Users.id_reservation WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nQuarto = reader["n_room"].ToString();

                        nQuartoTxtBox.Text = nQuarto;
                    }
                }
            }

            data = "SELECT FORMAT(Reservations.[check-in], 'dd/MM/yy | hh:mm tt') AS 'check-in', " +
                "FORMAT(Reservations.[check-out], 'dd/MM/yy | hh:mm tt') AS 'check-out' FROM Reservations " +
                "INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);

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

            // FALTA A CONSULTA SQL
            data = "SELECT Reservations.reserva_price FROM Reservations " +
                "INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation WHERE username = @username";
            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string reservaprice = reader["reserva_price"].ToString();

                        labelPagamentoTxt.Content = reservaprice + "€";
                    }
                }
            }
            con.Close();
        }

        private void honortehotel_Click(object sender, RoutedEventArgs e)
        {
            con.Open();

            string data = "UPDATE Users SET stars = @stars WHERE username = @username";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@stars", nStars);
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);
                cmd.ExecuteNonQuery();
            }
            con.Close();

            MessageBox.Show("Obrigado por ter classificado o nosso hotel com " + nStars + " estrelas! \nSomos muito agradecidos", "Obrigado!!!");
        }
    }
}
