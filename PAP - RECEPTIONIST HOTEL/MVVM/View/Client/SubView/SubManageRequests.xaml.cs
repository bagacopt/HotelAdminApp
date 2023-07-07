using PAP___RECEPTIONIST_HOTEL.Forms;
using PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.PrimaryView;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.SubView
{
    public partial class SubManageRequests : UserControl
    {
        public static List<string> labelValues;

        public SubManageRequests()
        {
            InitializeComponent();
            labelValues = new List<string>();
        }

        // VARIABLES
        string data;
        int serviceSelected;
        string[] tempClient;
        public static string verification, client;

        // OBJECTS
        Label label;

        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        public void SubManageRequests_Loaded(object sender, System.Windows.RoutedEventArgs e)
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
                "WHERE Requests.active = 1 AND Rooms.n_room = " + ControlPanel.nRoom, con);

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

            if (Requests.serviceID == 1)
            {
                switch (Requests.serviceName)
                {
                    case "Serviço de limpeza de quartos diário":
                        TabItem1.IsSelected = true;
                        SubTabItem1.IsSelected = true;
                        RequestData1.SelectedIndex = 0;
                        RequestData1.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Serviço extra de limpeza de quarto":
                        TabItem1.IsSelected = true;
                        SubTabItem1.IsSelected = true;
                        RequestData1.SelectedIndex = 1;
                        RequestData1.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Kit de limpeza para uso dos hóspedes":
                        TabItem1.IsSelected = true;
                        SubTabItem1.IsSelected = true;
                        RequestData1.SelectedIndex = 2;
                        RequestData1.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Serviço de abertura da cama":
                        TabItem1.IsSelected = true;
                        SubTabItem1.IsSelected = true;
                        RequestData1.SelectedIndex = 3;
                        RequestData1.SelectionChanged += RequestDataSelectionChanged;
                        break;
                }
            }
            else if (Requests.serviceID == 2)
            {
                switch (Requests.serviceName)
                {
                    case "Shampoo e gel de banho":
                        TabItem1.IsSelected = true;
                        SubTabItem2.IsSelected = true;
                        RequestData2.SelectedIndex = 0;
                        RequestData2.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Condicionador":
                        TabItem1.IsSelected = true;
                        SubTabItem2.IsSelected = true;
                        RequestData2.SelectedIndex = 1;
                        RequestData2.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Sabão":
                        TabItem1.IsSelected = true;
                        SubTabItem2.IsSelected = true;
                        RequestData2.SelectedIndex = 2;
                        RequestData2.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Kit de pasta de dentes":
                        TabItem1.IsSelected = true;
                        SubTabItem2.IsSelected = true;
                        RequestData2.SelectedIndex = 3;
                        RequestData2.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Kit de barbear":
                        TabItem1.IsSelected = true;
                        SubTabItem2.IsSelected = true;
                        RequestData2.SelectedIndex = 4;
                        RequestData2.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Kit de costura":
                        TabItem1.IsSelected = true;
                        SubTabItem2.IsSelected = true;
                        RequestData2.SelectedIndex = 5;
                        RequestData2.SelectionChanged += RequestDataSelectionChanged;
                        break;
                }
            }
            else
            {
                switch (Requests.serviceName)
                {
                    case "Ar condicionado (A/C)":
                        TabItem2.IsSelected = true;
                        RequestData3.SelectedIndex = 0;
                        RequestData3.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Cama":
                        TabItem2.IsSelected = true;
                        RequestData3.SelectedIndex = 1;
                        RequestData3.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Máquina de café":
                        TabItem2.IsSelected = true;
                        RequestData3.SelectedIndex = 2;
                        RequestData3.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Pilhas do comando":
                        TabItem2.IsSelected = true;
                        RequestData3.SelectedIndex = 3;
                        RequestData3.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Minibar":
                        TabItem2.IsSelected = true;
                        RequestData3.SelectedIndex = 4;
                        RequestData3.SelectionChanged += RequestDataSelectionChanged;
                        break;
                    case "Secador":
                        TabItem2.IsSelected = true;
                        RequestData3.SelectedIndex = 5;
                        RequestData3.SelectionChanged += RequestDataSelectionChanged;
                        break;
                }
            }
        }

        private void RequestDataSelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            // SEPARATE THE ID OF COMBOBOX VALUE
            if (PrimaryTabControl.SelectedIndex == 0)
            {
                if (SubTabControl.SelectedIndex == 0)
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
                                    label = (Label)FindName("showDataArray" + i.ToString());
                                    label.Visibility = Visibility.Visible;
                                    label.Content = reader[i].ToString();
                                }
                            }
                            else
                            {
                                for (int i = 5; i <= 9; i++)
                                {
                                    label = (Label)FindName("showDataArray" + i.ToString());
                                    label.Visibility = Visibility.Visible;
                                    label.Content = reader[i - 5].ToString();
                                }
                            }
                        }
                        else
                        {
                            for (int i = 10; i <= 13; i++)
                            {
                                label = (Label)FindName("showDataArray" + i.ToString());
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
                cmd.Parameters.AddWithValue("@NRoom", ControlPanel.nRoom);
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
                cmd.Parameters.AddWithValue("@NRoom", ControlPanel.nRoom);
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

        private void EditRequest_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (PrimaryTabControl.SelectedIndex == 0 && SubTabControl.SelectedIndex == 0)
                {
                    for (int i = 0; i <= 4; i++)
                    {
                        label = (Label)FindName("showDataArray" + i.ToString());
                        labelValues.Add(label.Content as string);
                    }
                }
                else if (PrimaryTabControl.SelectedIndex == 0 && SubTabControl.SelectedIndex == 1)
                {
                    for (int i = 5; i <= 9; i++)
                    {
                        label = (Label)FindName("showDataArray" + i.ToString());
                        labelValues.Add(label.Content as string);
                    }
                }
                else
                {
                    for (int i = 10; i <= 13; i++)
                    {
                        label = (Label)FindName("showDataArray" + i.ToString());
                        labelValues.Add(label.Content as string);
                    }
                }

                verification = "edit this";

                Application.Current.MainWindow.Hide();
                Application.Current.MainWindow = new ServicesRequests();
                Application.Current.MainWindow.Show();
            }
            catch
            {
                MessageBox.Show("Não foi possível editar o pedido!", "Erro", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}