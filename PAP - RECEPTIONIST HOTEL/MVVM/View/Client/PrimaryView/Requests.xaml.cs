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
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int count;
        public static string serviceName, typeService;
        public static int serviceID;

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)e.Source).Name)
            {
                case "OrderButton1":
                    serviceName = "Serviço de limpeza de quartos diário";
                    serviceID = 1;
                    typeService = "Serviços";
                    break;
                case "OrderButton2":
                    serviceName = "Serviço extra de limpeza de quarto";
                    serviceID = 1;
                    typeService = "Serviços";
                    break;
                case "OrderButton3":
                    serviceName = "Kit de limpeza para uso dos hóspedes";
                    serviceID = 1;
                    typeService = "Serviços";
                    break;
                case "OrderButton4":
                    serviceName = "Serviço de abertura da cama";
                    serviceID = 1;
                    typeService = "Serviços";
                    break;
                case "OrderButton5":
                    serviceName = "Serviço de comida ao quarto";
                    serviceID = 1;
                    typeService = "Serviços";
                    break;
                case "OrderButton6":
                    serviceName = "Shampoo e gel de banho";
                    serviceID = 2;
                    typeService = "Extras";
                    break;
                case "OrderButton7":
                    serviceName = "Condicionador";
                    serviceID = 2;
                    typeService = "Extras";
                    break;
                case "OrderButton8":
                    serviceName = "Sabão";
                    serviceID = 2;
                    typeService = "Extras";
                    break;
                case "OrderButton9":
                    serviceName = "Kit de pasta de dentes";
                    serviceID = 2;
                    typeService = "Extras";
                    break;
                case "OrderButton10":
                    serviceName = "Kit de barbear";
                    serviceID = 2;
                    typeService = "Extras";
                    break;

                case "MaintainButton1":
                    serviceName = "Ar condicionado (A/C)";
                    serviceID = 3;
                    typeService = "Manutenção";
                    break;
                case "MaintainButton2":
                    serviceName = "Cama";
                    serviceID = 3;
                    typeService = "Manutenção";
                    break;
                case "MaintainButton3":
                    serviceName = "Máquina de café";
                    serviceID = 3;
                    typeService = "Manutenção";
                    break;
                case "MaintainButton4":
                    serviceName = "Pilhas do comando";
                    serviceID = 3;
                    typeService = "Manutenção";
                    break;
                case "MaintainButton5":
                    serviceName = "Minibar";
                    serviceID = 3;
                    typeService = "Manutenção";
                    break;
                case "MaintainButton6":
                    serviceName = "Secador";
                    serviceID = 3;
                    typeService = "Manutenção";
                    break;
            }

            // OPEN CONNECTION
            con.Open();

            data = "SELECT Requests.* FROM Requests JOIN Services ON Requests.services_id = Services.id " +
                "JOIN Reservations_Requests ON Reservations_Requests.request_id = Requests.id " +
                "JOIN Reservations ON Reservations_Requests.reservation_id = Reservations.id " +
                "JOIN Rooms ON Reservations.rooms_id = Rooms.id " +
                "WHERE Requests.name = @name AND Requests.active = 1 AND Requests.services_id = @serviceID AND Rooms.n_room = @nRoom";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@name", serviceName);
                cmd.Parameters.AddWithValue("@serviceID", serviceID);
                cmd.Parameters.AddWithValue("@nRoom", ControlPanel.nRoom);
                cmd.ExecuteNonQuery();
                count = Convert.ToInt32(cmd.ExecuteScalar());

                if (count > 0)
                {
                    MessageBox.Show("Já existe um pedido com o nome " + serviceName + "! " +
                        "Por favor, espere que o pedido seja realizado ou apague o mesmo e refaça", "Alerta!", MessageBoxButton.OK, MessageBoxImage.Warning);
                    MessageBox.Show("A redirecionar para a página de gestão de pedidos", "Alerta!", MessageBoxButton.OK, MessageBoxImage.Warning);

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
    }
}