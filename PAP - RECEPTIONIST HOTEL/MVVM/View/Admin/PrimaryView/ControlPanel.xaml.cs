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
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int pendingRequests, concludedRequests, canceledRequests;

        private void ControlPanel_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            data = "SELECT COUNT(*) AS PendingCount, " +
                "(SELECT COUNT(*) FROM Requests WHERE active = 0 AND state = 'CONCLUÍDO') AS ConcludedCount, " +
                "(SELECT COUNT(*) FROM Requests WHERE active = 0 AND state = 'APAGADO') AS CanceledCount " +
                "FROM Requests WHERE active = 1 AND state = 'PENDENTE'";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        pendingRequests = reader.GetInt32(reader.GetOrdinal("PendingCount"));
                        concludedRequests = reader.GetInt32(reader.GetOrdinal("ConcludedCount"));
                        canceledRequests = reader.GetInt32(reader.GetOrdinal("CanceledCount"));

                        ChartViewModel showData = new ChartViewModel
                        {
                            PendingRequests = new ChartValues<int> { pendingRequests },
                            ConcludedRequests = new ChartValues<int> { concludedRequests },
                            CanceledRequests = new ChartValues<int> { canceledRequests }
                        };
                        DataContext = showData;
                    }
                }
            }

            data = "SELECT COUNT(*) AS NTotalReservations, (SELECT COUNT(*) FROM Users WHERE type_user = 1) AS NTotalClients, SUM(CASE WHEN active = 1 THEN 1 ELSE 0 END) AS ActiveReservations FROM Reservations;";

            using (SqlCommand cmd = new SqlCommand(data, con))
            {
                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        nTotalReservations.Content = reader.GetInt32(reader.GetOrdinal("NTotalReservations"));
                        nTotalClients.Content = reader.GetInt32(reader.GetOrdinal("NTotalClients"));
                        activeReservations.Content = reader.GetInt32(reader.GetOrdinal("ActiveReservations"));
                    }
                }
            }
            
            // CLOSE CONNECTION
            con.Close();
        }
    }
}
