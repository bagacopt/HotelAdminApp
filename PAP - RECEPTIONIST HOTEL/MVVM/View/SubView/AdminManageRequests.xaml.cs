using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Data.SqlClient;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.SubView
{
    /// <summary>
    /// Interaction logic for ManageRequestsAdmin.xaml
    /// </summary>
    public partial class AdminManageRequests : UserControl
    {
        public AdminManageRequests()
        {
            InitializeComponent();
        }

        // VARIABLES
        string data;

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        private void AdminManageRequests_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            con.Open();

            data = "SELECT id, name FROM Requests";

            




            for (int i = 0; i <= 13; i++)
            {
                Label label = (Label)this.FindName("showDataArray" + i.ToString());
                label.Visibility = System.Windows.Visibility.Hidden;
            }
        }
    }
}
