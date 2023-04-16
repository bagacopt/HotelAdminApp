using PAP___RECEPTIONIST_HOTEL.MVVM.View.SubView;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary>
    /// Interaction logic for Requests.xaml
    /// </summary>
    public partial class Requests : UserControl
    {
        public Requests()
        {
            InitializeComponent();
        }

        // VARIABLES
        public static string serviceName;
        public static string maintainName;

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            switch (((Button)e.Source).Name)
            {
                case "OrderButton1":
                    serviceName = "Serviço de limpeza de quartos diário";
                    break;
                case "OrderButton2":
                    serviceName = "Serviço extra de limpeza de quarto";
                    break;
                case "OrderButton3":
                    serviceName = "Kit de limpeza para uso dos hóspedes";
                    break;
                case "OrderButton4":
                    serviceName = "Serviço de abertura da cama";
                    break;
                case "OrderButton5":
                    serviceName = "Shampoo e gel de banho";
                    break;
                case "OrderButton6":
                    serviceName = "Condicionador";
                    break;
                case "OrderButton7":
                    serviceName = "Sabão";
                    break;
                case "OrderButton8":
                    serviceName = "Kit de pasta de dentes";
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