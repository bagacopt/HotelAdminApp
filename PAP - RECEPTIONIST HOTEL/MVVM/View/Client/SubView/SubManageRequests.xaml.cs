using PAP___RECEPTIONIST_HOTEL.MVVM.View.Admin.PrimaryView;
using PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.PrimaryView;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.SubView
{
    public partial class SubManageRequests : UserControl
    {
        public SubManageRequests()
        {
            InitializeComponent();
        }

        // VARIABLES
        string data;
        int serviceSelected;

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        public void SubManageRequests_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "SELECT Requests.id, Requests.name FROM Requests INNER JOIN Reservations_Requests " +
                "ON Requests.id = Reservations_Requests.request_id INNER JOIN Reservations " +
                "ON Reservations_Requests.reservation_id = Reservations.id INNER JOIN Rooms " +
                "ON Reservations.rooms_id = Rooms.id " +
                "WHERE Rooms.n_room = @nRoom AND Requests.services_id = @serviceID";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                if (PrimaryTabControl.SelectedIndex == 0)
                {
                    if (SubTabControl.SelectedIndex == 0)
                    {
                        serviceSelected = 1;
                    }
                    else
                    {
                        serviceSelected = 2;
                    }
                }
                else
                {
                    serviceSelected = 3;
                }

                cmd.Parameters.AddWithValue("@nRoom", ControlPanel.nRoom);
                cmd.Parameters.AddWithValue("@serviceID", serviceSelected);
                cmd.ExecuteNonQuery();
            }

            SqlDataAdapter da = new SqlDataAdapter("SELECT Requests.id, Requests.name, Requests.services_id " +
                "FROM Requests INNER JOIN Reservations_Requests " +
                "ON Requests.id = Reservations_Requests.request_id INNER JOIN Reservations " +
                "ON Reservations_Requests.reservation_id = Reservations.id INNER JOIN Rooms " +
                "ON Reservations.rooms_id = Rooms.id " +
                "WHERE Rooms.n_room = " + ControlPanel.nRoom, con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                if (Convert.ToInt32(dt.Rows[i]["services_id"]) == 1)
                {
                    RequestData1.Items.Add("#" + dt.Rows[i]["id"] + " - " + dt.Rows[i]["name"]);
                }
                if (Convert.ToInt32(dt.Rows[i]["services_id"]) == 2)
                {
                    RequestData2.Items.Add("#" + dt.Rows[i]["id"] + " - " + dt.Rows[i]["name"]);
                }
                if (Convert.ToInt32(dt.Rows[i]["services_id"]) == 3)
                {
                    RequestData3.Items.Add("#" + dt.Rows[i]["id"] + " - " + dt.Rows[i]["name"]);
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void RequestDataSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
