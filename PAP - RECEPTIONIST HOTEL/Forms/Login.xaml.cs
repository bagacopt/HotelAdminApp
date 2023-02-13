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
using System.Windows.Shapes;
using System.Data.SqlClient;
using PAP___RECEPTIONIST_HOTEL.Properties;

namespace PAP___RECEPTIONIST_HOTEL
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        SqlConnection con = new SqlConnection("Data Source=DESKTOP-LQBQ1HM;Initial Catalog=reservas_PAP;Integrated Security=True");

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            con.Open();

            string data = "SELECT * FROM Users WHERE username = @username AND password = @pass";

            SqlCommand cmd = new SqlCommand(data, con);

            cmd.Parameters.AddWithValue("@username", txtUser.Text);
            Settings.Default.n_cliente = txtUser.Text;

            PasswordBox myPasswordBox = this.FindName("passwordHidden") as PasswordBox;
            if (myPasswordBox.Name == "passwordHidden")
            {
                cmd.Parameters.AddWithValue("@pass", passwordHidden.Password);
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                con.Close();

                if (count > 0)
                {
                    MainWindow program = new MainWindow();
                    program.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Inseriu o username / password errados, insira novamente", "Erro!!!");
                }
            }
            else
            {
                cmd.Parameters.AddWithValue("@pass", passwordShow.Text);
                cmd.ExecuteNonQuery();
                int count = Convert.ToInt32(cmd.ExecuteScalar());

                con.Close();

                if (count > 0)
                {
                    MainWindow program = new MainWindow();
                    program.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Inseriu o username / password errados, insira novamente", "Erro!!!");
                }
            }
        }

        private void Forgot_pass(object sender, MouseButtonEventArgs e)
        {
            Forgot_pass forgot = new Forgot_pass();
            forgot.Show();
            this.Hide();
        }

        private void PressKey(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                btnLogin_Click(sender, e);
            }
        }

        private void HidePasswordChecked(object sender, RoutedEventArgs e)
        {
            passwordHidden.Visibility = Visibility.Hidden;
            passwordShow.Visibility = Visibility.Visible;
            passwordShow.Text = passwordHidden.Password;
        }

        private void HidePasswordUnchecked(object sender, RoutedEventArgs e)
        {
            passwordHidden.Visibility = Visibility.Visible;
            passwordShow.Visibility = Visibility.Hidden;
            passwordHidden.Password = passwordShow.Text;
        }
    }
}
