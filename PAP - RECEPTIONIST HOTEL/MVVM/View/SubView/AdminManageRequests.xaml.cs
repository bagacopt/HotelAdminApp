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

        private void AdminManageRequests_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            for (int i = 0; i <= 13; i++)
            {
                Label label = (Label)this.FindName("showDataArray" + i.ToString());
                label.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /* EXTRAS
         for (int i = 5; i <= 9; i++){
            Label label = (Label)this.FindName("label" + i.ToString());
            label.Visibility = Visibility.Hidden; }
         */

        /* MANUTENÇÕES
         for (int i = 10; i <= 13; i++){
            Label label = (Label)this.FindName("label" + i.ToString());
            label.Visibility = Visibility.Hidden; }
         */





    }
}
