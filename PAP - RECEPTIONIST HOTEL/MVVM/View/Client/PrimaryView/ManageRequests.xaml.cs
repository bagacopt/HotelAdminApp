using PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.SubView;
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

        private void ManageRequests_Loaded(object sender, RoutedEventArgs e)
        {
            ClientRequestName.Content = ControlPanel.nRoom;

            ContentControlContent.Content = new SubManageRequests();
        }
    }
}
