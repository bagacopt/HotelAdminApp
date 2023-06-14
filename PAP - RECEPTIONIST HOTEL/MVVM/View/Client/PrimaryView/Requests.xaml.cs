using PAP___RECEPTIONIST_HOTEL.Forms;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.PrimaryView
{
    public partial class Requests : UserControl
    {
        public Requests()
        {
            InitializeComponent();
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        public static string serviceName;
        public static int serviceID;
        string data;
        int count;

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)e.Source).Name)
            {
                case "OrderButton1":
                    serviceName = "Serviço de limpeza de quartos diário";
                    serviceID = 1;
                    break;
                case "OrderButton2":
                    serviceName = "Serviço extra de limpeza de quarto";
                    serviceID = 1;
                    break;
                case "OrderButton3":
                    serviceName = "Kit de limpeza para uso dos hóspedes";
                    serviceID = 1;
                    break;
                case "OrderButton4":
                    serviceName = "Serviço de abertura da cama";
                    serviceID = 1;
                    break;
                case "OrderButton5":
                    serviceName = "Shampoo e gel de banho";
                    serviceID = 2;
                    break;
                case "OrderButton6":
                    serviceName = "Condicionador";
                    serviceID = 2;
                    break;
                case "OrderButton7":
                    serviceName = "Sabão";
                    serviceID = 2;
                    break;
                case "OrderButton8":
                    serviceName = "Kit de pasta de dentes";
                    serviceID = 2;
                    break;
                case "OrderButton9":
                    serviceName = "Kit de barbear";
                    serviceID = 2;
                    break;
                case "OrderButton10":
                    serviceName = "Kit de costura";
                    serviceID = 2;
                    break;
                case "MaintainButton1":
                    serviceName = "Ar condicionado (A/C)";
                    serviceID = 3;
                    break;
                case "MaintainButton2":
                    serviceName = "Cama";
                    serviceID = 3;
                    break;
                case "MaintainButton3":
                    serviceName = "Máquina de café";
                    serviceID = 3;
                    break;
                case "MaintainButton4":
                    serviceName = "Pilhas do comando";
                    serviceID = 3;
                    break;
                case "MaintainButton5":
                    serviceName = "Minibar";
                    serviceID = 3;
                    break;
                case "MaintainButton6":
                    serviceName = "Secador";
                    serviceID = 3;
                    break;
            }

            // OPEN CONNECTION
            con.Open();

            data = "SELECT * FROM Requests WHERE name = @name AND services_id = @serviceID";
            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@name", serviceName);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.ExecuteNonQuery();
                count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Já existe um pedido pendente desta secção, " +
                        "Por favor espere que o pedido seja realizado ou apague o pedido anterior e refaça o seu pedido", "Erro!");

                    SubView.SubManageRequests manageRequests = new SubView.SubManageRequests();

                    if (serviceID == 1)
                    {
                        switch (serviceName)
                        {
                            case "Serviço de limpeza de quartos diário":
                                manageRequests.RequestData1.SelectedIndex = 0;
                                MessageBox.Show(manageRequests.RequestData1.SelectedIndex.ToString());
                                break;
                            case "Serviço extra de limpeza de quarto":
                                manageRequests.RequestData1.SelectedIndex = 1;
                                break;
                            case "Kit de limpeza para uso dos hóspedes":
                                manageRequests.RequestData1.SelectedIndex = 2;
                                break;
                            case "Serviço de abertura da cama":
                                manageRequests.RequestData1.SelectedIndex = 3;
                                break;
                        }
                    }
                    else if (serviceID == 2)
                    {
                        switch (serviceName)
                        {
                            case "Shampoo e gel de banho":
                                manageRequests.RequestData2.SelectedIndex = 0;
                                break;
                            case "Condicionador":
                                manageRequests.RequestData2.SelectedIndex = 1;
                                break;
                            case "Sabão":
                                manageRequests.RequestData2.SelectedIndex = 2;
                                break;
                            case "Kit de pasta de dentes":
                                manageRequests.RequestData2.SelectedIndex = 3;
                                break;
                            case "Kit de barbear":
                                manageRequests.RequestData2.SelectedIndex = 4;
                                break;
                            case "Kit de costura":
                                manageRequests.RequestData2.SelectedIndex = 5;
                                break;
                        }
                    }
                    else
                    {
                        switch (serviceName)
                        {
                            case "Ar condicionado (A/C)":
                                manageRequests.RequestData3.SelectedIndex = 0;
                                break;
                            case "Cama":
                                manageRequests.RequestData3.SelectedIndex = 1;
                                break;
                            case "Máquina de café":
                                manageRequests.RequestData3.SelectedIndex = 2;
                                break;
                            case "Pilhas do comando":
                                manageRequests.RequestData3.SelectedIndex = 3;
                                break;
                            case "Minibar":
                                manageRequests.RequestData3.SelectedIndex = 4;
                                break;
                            case "Secador":
                                manageRequests.RequestData3.SelectedIndex = 5;
                                break;
                        }
                    }

                    MessageBox.Show(manageRequests.RequestData1.SelectedIndex.ToString());
                    var mainWindow = (MainWindow)Application.Current.MainWindow;
                    mainWindow.ManageRequestsRadioButton.IsChecked = true;
                    mainWindow.ManageRequestsRadioButton.Command.Execute(null);
                }
                else
                {
                    Application.Current.MainWindow.Hide();
                    Application.Current.MainWindow = new SubView.ServicesRequests();
                    Application.Current.MainWindow.Show();
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void ManageRequests_Loaded(object sender, RoutedEventArgs e)
        {
            throw new NotImplementedException();
        }
    }
}