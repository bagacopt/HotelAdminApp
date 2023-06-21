using PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.SubView;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.PrimaryView
{
    public partial class ManageRequests : UserControl
    {
        public ManageRequests()
        {
            InitializeComponent();
        }

        // VARIABLES

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        private void ManageRequests_Loaded(object sender, RoutedEventArgs e)
        {
            ClientRequestName.Content = ControlPanel.nRoom;

            ContentControlContent.Content = new SubManageRequests();
        }
    }
}
