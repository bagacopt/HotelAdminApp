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

        // CONNECTION
        SqlConnection con = new SqlConnection("Data Source=BAGACINHO;Initial Catalog=reservas_PAP;Integrated Security=True");

        // VARIABLES
        int nStars;
        string data;

        // 1 STAR RATE
        private void ClassificationStars1_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 1;
        }

        // 2 STAR RATE
        private void ClassificationStars2_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 2;
        }

        // 3 STAR RATE
        private void ClassificationStars3_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 3;
        }

        // 4 STAR RATE
        private void ClassificationStars4_Click(object sender, RoutedEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));

            nStars = 4;
        }

        // 5 STAR RATE
        private void ClassificationStars5_Click(object sender, RoutedEventArgs e)
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
                        idReservaTxtBox.Text = reader["id_reservation"].ToString();
                        string stars = reader["stars"].ToString();
                        nClienteTxtBox.Text = reader["fullname"].ToString();

                        switch (stars)
                        {
                            case "0":
                                break;
                            case "1":
                                ClassificationStars1_Click(sender, e);
                                break;
                            case "2":
                                ClassificationStars2_Click(sender, e);
                                break;
                            case "3":
                                ClassificationStars3_Click(sender, e);
                                break;
                            case "4":
                                ClassificationStars4_Click(sender, e);
                                break;
                            case "5":
                                ClassificationStars5_Click(sender, e);
                                break;
                        }
                    }
                }
            }

            // GET NUMBER OF THE ROOM
            data = "SELECT Rooms.n_room FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id_room = Reservations.id_room INNER JOIN Users ON " +
                "Reservations.id_reservation = Users.id_reservation WHERE username = @user";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string nQuarto = reader["n_room"].ToString();

                        nQuartoTxtBox.Text = nQuarto;
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
                cmd.ExecuteNonQuery();
            }
            // CLOSE CONNECTION
            con.Close();

            MessageBox.Show("Obrigado por ter classificado o nosso hotel com " + nStars + " estrelas! \nSomos muito agradecidos", "Obrigado!!!");
        }
    }
}
