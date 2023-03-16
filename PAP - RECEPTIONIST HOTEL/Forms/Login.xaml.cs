using System;
using System.Windows;
using System.Windows.Controls;
using System.Data.SqlClient;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Windows.Input;

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

        // CONNECTION
        SqlConnection con = new SqlConnection("Data Source=BAGACINHO;Initial Catalog=reservas_PAP;Integrated Security=True");

        // VARIABLES
        string data;
        int count;

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // CLOSES APPLICATION
            Application.Current.Shutdown();
        }

        private void BtnMinimize_Click(object sender, RoutedEventArgs e)
        {
            // MINIMIZES APPLICATION
            WindowState = WindowState.Minimized;
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // CAN MOVE APPLICATION FROM WHATEVER POINT OF THE APPLICATION SIZE
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // SQL QUERY
            data = "SELECT * FROM Users WHERE username = @user AND password = @pass";

            using(SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", txtUser.Text.ToLower());
                Settings.Default.n_cliente = txtUser.Text.ToLower();

                PasswordBox myPasswordBox = this.FindName("passwordHidden") as PasswordBox;
                if (myPasswordBox.Name == "passwordHidden")
                {
                    cmd.Parameters.AddWithValue("@pass", passwordHidden.Password.ToLower());
                    cmd.ExecuteNonQuery();
                    count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // GOES TO FIRST PART OF THE APPLICATION
                        MainWindow program = new MainWindow();
                        program.Show();
                        this.Close();
                    }
                    else
                    {
                        // ERROR MESSAGE
                        MessageBox.Show("Inseriu o username / password errados, insira novamente", "Erro!!!");
                    }
                }
                else
                {
                    cmd.Parameters.AddWithValue("@pass", passwordShow.Text.ToLower());
                    cmd.ExecuteNonQuery();
                    count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // GOES T5O FIRST PART OF THE APPLICATION
                        MainWindow program = new MainWindow();
                        program.Show();
                        this.Close();
                    }
                    else
                    {
                        // ERROR MESSAGE
                        MessageBox.Show("Inseriu o username / password errados, insira novamente", "Erro!!!");
                    }
                }
            }
            con.Close();
        }

        private void Forgot_pass(object sender, MouseButtonEventArgs e)
        {
            // GOES TO FORGOT PASSWORD
            Forgot_pass forgot = new Forgot_pass();
            forgot.Show();
            this.Hide();
        }

        private void PressKey(object sender, KeyEventArgs e)
        {
            // PRESSES THE BUTTON OF THE LOGIN
            if (e.Key == Key.Enter)
            {
                BtnLogin_Click(sender, e);
            }

            // PRESSES ESCAPE
            if (e.Key == Key.Escape)
            {
                // CLOSES PROGRAM
                Application.Current.Shutdown();
            }
        }

        private void HidePasswordChecked(object sender, RoutedEventArgs e)
        {
            // VISIBILITY CHANGES
            passwordHidden.Visibility = Visibility.Hidden;
            passwordShow.Visibility = Visibility.Visible;
            passwordShow.Text = passwordHidden.Password;
        }

        private void HidePasswordUnchecked(object sender, RoutedEventArgs e)
        {
            // VISIBILITY CHANGES
            passwordHidden.Visibility = Visibility.Visible;
            passwordShow.Visibility = Visibility.Hidden;
            passwordHidden.Password = passwordShow.Text;
        }

        private void Login_Loaded(object sender, RoutedEventArgs e)
        {
            txtUser.Focus();
        }
    }
}
