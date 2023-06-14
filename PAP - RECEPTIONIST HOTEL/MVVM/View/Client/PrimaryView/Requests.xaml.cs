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
        public static string maintainName;
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
                    maintainName = "Ar condicionado (A/C)";
                    serviceID = 3;
                    break;
                case "MaintainButton2":
                    maintainName = "Cama";
                    serviceID = 3;
                    break;
                case "MaintainButton3":
                    maintainName = "Máquina de café";
                    serviceID = 3;
                    break;
                case "MaintainButton4":
                    maintainName = "Pilhas do comando";
                    serviceID = 3;
                    break;
                case "MaintainButton5":
                    maintainName = "Minibar";
                    serviceID = 3;
                    break;
                case "MaintainButton6":
                    maintainName = "Secador";
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
                        "Por favor espere que o pedido seja realizado ou apague o pedido anterior e refaça o seu pedido","Erro!");
                    MainWindow win = new MainWindow();
                    win.ManageRequestsRadioButton.IsChecked = true;
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