using PAP___RECEPTIONIST_HOTEL.Forms;
using PAP___RECEPTIONIST_HOTEL.MVVM.View.Admin.PrimaryView;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Admin.SubView
{
    public partial class SubManageRequests : UserControl
    {
        public SubManageRequests()
        {
            InitializeComponent();
        }

        // VARIABLES
        string data, client;
        string[] tempClient;
        int serviceSelected;

        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        public void SubManageRequests_Loaded(object sender, RoutedEventArgs e)
        {
            Border1.Visibility = Visibility.Hidden;
            Border2.Visibility = Visibility.Hidden;
            Border3.Visibility = Visibility.Hidden;

            // OPEN CONNECTION
            con.Open();

            data = "SELECT Requests.id, Requests.name FROM Requests INNER JOIN Reservations_Requests " +
                "ON Requests.id = Reservations_Requests.request_id INNER JOIN Reservations " +
                "ON Reservations_Requests.reservation_id = Reservations.id INNER JOIN Rooms " +
                "ON Reservations.rooms_id = Rooms.id " +
                "WHERE Rooms.n_room = @nRoom AND Requests.services_id = @serviceID";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                if(PrimaryTabControl.SelectedIndex == 0)
                {
                    if(SubTabControl.SelectedIndex == 0)
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

                cmd.Parameters.AddWithValue("@nRoom", ManageRequests.nClient);
                cmd.Parameters.AddWithValue("@serviceID", serviceSelected);
                cmd.ExecuteNonQuery();
            }

            SqlDataAdapter da = new SqlDataAdapter("SELECT Requests.id, Requests.name, Requests.services_id " +
                "FROM Requests INNER JOIN Reservations_Requests " +
                "ON Requests.id = Reservations_Requests.request_id INNER JOIN Reservations " +
                "ON Reservations_Requests.reservation_id = Reservations.id INNER JOIN Rooms " +
                "ON Reservations.rooms_id = Rooms.id " +
                "WHERE Requests.active = 1 AND Requests.state = 'PENDENTE' AND Rooms.n_room = " + ManageRequests.nClient, con);
            
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
            // SEPARATE THE ID OF COMBOBOX VALUE
            if (PrimaryTabControl.SelectedIndex == 0)
            {
                if(SubTabControl.SelectedIndex == 0)
                {
                    tempClient = RequestData1.SelectedValue.ToString().Split('#');
                    client = tempClient[1].ToString();
                    tempClient = client.Split('-');
                    client = tempClient[0];
                    Border1.Visibility = Visibility.Visible;
                }
                else
                {
                    tempClient = RequestData2.SelectedValue.ToString().Split('#');
                    client = tempClient[1].ToString();
                    tempClient = client.Split('-');
                    client = tempClient[0];
                    Border2.Visibility = Visibility.Visible;
                }
            }
            else
            {
                tempClient = RequestData3.SelectedValue.ToString().Split('#');
                client = tempClient[1].ToString();
                tempClient = client.Split('-');
                client = tempClient[0];
                Border3.Visibility = Visibility.Visible;
            }

            // OPEN CONNECTION
            con.Open();

            data = "SELECT name, phone_number, email, quantity, [desc] FROM Requests WHERE id = @id";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@id", client);
                cmd.ExecuteNonQuery();

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        if (PrimaryTabControl.SelectedIndex == 0)
                        {
                            if (SubTabControl.SelectedIndex == 0)
                            {
                                for (int i = 0; i <= 4; i++)
                                {
                                    Label label = (Label)this.FindName("showDataArray" + i.ToString());
                                    label.Visibility = Visibility.Visible;
                                    label.Content = reader[i].ToString();
                                }
                            }
                            else
                            {
                                for (int i = 5; i <= 9; i++)
                                {
                                    Label label = (Label)this.FindName("showDataArray" + i.ToString());
                                    label.Visibility = Visibility.Visible;
                                    label.Content = reader[i - 5].ToString();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 10; i <= 13; i++)
                            {
                                Label label = (Label)this.FindName("showDataArray" + i.ToString());
                                label.Visibility = Visibility.Visible;

                                if (i - 10 == 3)
                                {
                                    i += 1;
                                }
                                label.Content = reader[i - 10].ToString();
                            }
                        }
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void EliminateRequest_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "UPDATE Requests SET active = 0, state = 'APAGADO' WHERE id IN (SELECT request_id FROM Reservations_Requests WHERE reservation_id IN " +
                "(SELECT id FROM Reservations WHERE rooms_id = (SELECT id FROM Rooms WHERE n_room = @NRoom)) AND id = @id)";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@NRoom", ManageRequests.nClient);
                cmd.Parameters.AddWithValue("@id", client);

                cmd.ExecuteNonQuery();
            }

            // CLOSE CONNECTION
            con.Close();

            MessageBox.Show("Pedido apagado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("Voltando ao menu principal...", "Voltar", MessageBoxButton.OK, MessageBoxImage.Information);

            if (PrimaryTabControl.SelectedIndex == 0)
            {
                if (SubTabControl.SelectedIndex == 0)
                {
                    RequestData1.SelectionChanged -= RequestDataSelectionChanged;
                    RequestData1.Items.Clear();
                }
                else
                {
                    RequestData2.SelectionChanged -= RequestDataSelectionChanged;
                    RequestData2.Items.Clear();
                }
            }
            else
            {
                RequestData3.SelectionChanged -= RequestDataSelectionChanged;
                RequestData3.Items.Clear();
            }

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ControlPanelRadioButton.IsChecked = true;
            mainWindow.ControlPanelRadioButton.Command.Execute(null);
        }

        private void ConcludedRequest_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "UPDATE Requests SET active = 0, state = 'CONCLUÍDO' WHERE id IN (SELECT request_id FROM Reservations_Requests WHERE reservation_id IN " +
                "(SELECT id FROM Reservations WHERE rooms_id = (SELECT id FROM Rooms WHERE n_room = @NRoom)) AND id = @id)";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@NRoom", ManageRequests.nClient);
                cmd.Parameters.AddWithValue("@id", client);

                cmd.ExecuteNonQuery();
            }

            // CLOSE CONNECTION
            con.Close();

            MessageBox.Show("Pedido concluído com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            MessageBox.Show("Voltando ao menu principal...", "Voltar", MessageBoxButton.OK, MessageBoxImage.Information);

            if (PrimaryTabControl.SelectedIndex == 0)
            {
                if (SubTabControl.SelectedIndex == 0)
                {
                    RequestData1.SelectionChanged -= RequestDataSelectionChanged;
                    RequestData1.Items.Clear();
                }
                else
                {
                    RequestData2.SelectionChanged -= RequestDataSelectionChanged;
                    RequestData2.Items.Clear();
                }
            }
            else
            {
                RequestData3.SelectionChanged -= RequestDataSelectionChanged;
                RequestData3.Items.Clear();
            }

            var mainWindow = (MainWindow)Application.Current.MainWindow;
            mainWindow.ControlPanelRadioButton.IsChecked = true;
            mainWindow.ControlPanelRadioButton.Command.Execute(null);
        }
    }
}