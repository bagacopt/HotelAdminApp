using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlTypes;
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

            string data = "SELECT id_reservation FROM Users WHERE username = @username";
            string data1 = "SELECT Rooms.n_room FROM Rooms INNER JOIN Reservations ON Rooms.id_room = Reservations.id_room INNER JOIN Users ON Reservations.id_reservation = Users.id_reservation WHERE username = @username";
            string data2 = "SELECT stars FROM Users WHERE username = @username";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string idReserva = reader["id_reservation"].ToString();

                        idReservaTxtBox.Text = idReserva;
                    }
                }
            }
            using (SqlCommand cmd = new SqlCommand(data1, con))
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

            using (SqlCommand cmd = new SqlCommand(data2, con))
            {
                cmd.Parameters.AddWithValue("@username", usernameTxtBox.Text);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string stars = reader["stars"].ToString();

                        switch (stars)
                        {
                            case "0":
                                continue;
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
        }
    }
}
