using LiveCharts;
using PAP___RECEPTIONIST_HOTEL.MVVM.ViewModel;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Admin.PrimaryView
{
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();

            Loaded += ControlPanel_Loaded;
        }

        // CONNECTION
        SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;


        private void ControlPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "SELECT COUNT(*) AS ActiveCount, (SELECT COUNT(*) FROM Requests WHERE active = 0) AS FinalizedCount " +
                "FROM Requests WHERE active = 1";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        int activeRequests = reader.GetInt32(reader.GetOrdinal("ActiveCount"));
                        int finalizedRequests = reader.GetInt32(reader.GetOrdinal("FinalizedCount"));

                        ChartViewModel showData = new ChartViewModel
                        {
                            ActiveRequests = new ChartValues<int> { activeRequests },
                            FinalizedRequests = new ChartValues<int> { finalizedRequests }
                        };
                        DataContext = showData;
                    }
                }
            }
            
            // CLOSE CONNECTION
            con.Close();
        }
    }
}
