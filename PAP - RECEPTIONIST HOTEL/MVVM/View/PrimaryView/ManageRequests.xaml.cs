using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary>
    /// Interaction logic for Manage_Requests.xaml
    /// </summary>
    public partial class ManageRequests : UserControl
    {
        public ManageRequests()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        private void ManageRequests_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // INSERT number of the room CONTENT IN selectRequestComboBox
            SqlDataAdapter da = new SqlDataAdapter("SELECT Rooms.n_room FROM Rooms INNER JOIN Reservations " +
                "ON Rooms.id = Reservations.rooms_id INNER JOIN Reservations_Requests " +
                "ON Reservations.id = Reservations_Requests.reservation_id INNER JOIN Requests " +
                "ON Reservations_Requests.request_id = Requests.id " +
                "WHERE Requests.active = 1 AND Reservations.active = 1 AND Rooms.available = 0", con);

            DataTable dt = new DataTable();

            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                selectRequestComboBox.Items.Add("Pedido no quarto: " + dt.Rows[i]["n_room"]);
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void selectRequestSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            contentControlContent.Visibility = Visibility.Visible;


        }
    }
}