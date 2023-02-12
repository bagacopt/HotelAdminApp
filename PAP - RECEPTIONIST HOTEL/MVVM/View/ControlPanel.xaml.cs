using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View
{
    /// <summary>
    /// Interaction logic for ControlPanel.xaml
    /// </summary>
    public partial class ControlPanel : UserControl
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        private void classificationStars1_Click(object sender, MouseButtonEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
        }

        private void classificationStars2_Click(object sender, MouseButtonEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
        }

        private void classificationStars3_Click(object sender, MouseButtonEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
        }

        private void classificationStars4_Click(object sender, MouseButtonEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gray_star.png", UriKind.RelativeOrAbsolute));
        }

        private void classificationStars5_Click(object sender, MouseButtonEventArgs e)
        {
            star_1.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_2.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_3.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_4.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
            star_5.Source = new BitmapImage(new Uri("/Forms/Images/full_gold_star.png", UriKind.RelativeOrAbsolute));
        }

    }
}
