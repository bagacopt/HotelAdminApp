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

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-LQBQ1HM;Initial Catalog=reservas_PAP;Integrated Security=True");

        private void ManageReservations_Loaded(object sender, RoutedEventArgs e)
        {
            con.Open();

            string data = "SELECT * FROM Users WHERE type_user = 3 AND username = @username";

            using (SqlCommand cmd = new SqlCommand(data, con)) 
            {
                cmd.Parameters.AddWithValue("@username", Settings.Default.n_cliente);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        string user = reader["username"].ToString();
                        string id = reader["id_user"].ToString();

                        usernameTxtBox.Text = user;
                        idReservaTxtBox.Text = id;
                    }
                }
            }

        }
    }
}
