using PAP___RECEPTIONIST_HOTEL.MVVM.View.SubView;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    public partial class Requests : UserControl
    {
        public Requests()
        {
            InitializeComponent();
        }

        // VARIABLES
        public static string serviceName;
        public static string maintainName;
        public static int serviceID;

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
            }

            Application.Current.MainWindow.Hide();
            Application.Current.MainWindow = new ServicesRequests();
            Application.Current.MainWindow.Show();
        }

        private void MaintenanceButton_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)e.Source).Name)
            {
                case "MaintainButton1":
                    maintainName = "Ar condicionado (A/C)";
                    break;
                case "MaintainButton2":
                    maintainName = "Cama";
                    break;
                case "MaintainButton3":
                    maintainName = "Máquina de café";
                    break;
                case "MaintainButton4":
                    maintainName = "Pilhas do comando";
                    break;
                case "MaintainButton5":
                    maintainName = "Minibar";
                    break;
                case "MaintainButton6":
                    maintainName = "Secador";
                    break;
            }

            Application.Current.MainWindow.Hide();
            Application.Current.MainWindow = new MaintenanceRequests();
            Application.Current.MainWindow.Show();
        }
    }
}