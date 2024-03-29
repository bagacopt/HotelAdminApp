﻿using System;
using System.Windows;
using System.Data.SqlClient;
using PAP___RECEPTIONIST_HOTEL.Properties;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.Forms
{
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
        }

        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int count;

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            // CLOSES APPLICATION
            Application.Current.Shutdown();
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // CAN MOVE APPLICATION FROM WHATEVER POINT OF THE APPLICATION SIZE
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void PressKey(object sender, KeyEventArgs e)
        {
            // PRESSES THE BUTTON OF THE LOGIN
            if (e.Key == Key.Enter)
            {
                LoginButton_Click(sender, e);
            }

            // PRESSES ESCAPE
            if (e.Key == Key.Escape)
            {
                // CLOSES PROGRAM
                Application.Current.Shutdown();
            }
        }

        private void LoginButton_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            // SQL QUERY
            data = "SELECT * FROM Users WHERE username = @user AND password = @pass";

            using(SqlCommand cmd = new SqlCommand(data, con))
            {
                cmd.Parameters.AddWithValue("@user", usernameTxtBox.Text.ToLower());
                Settings.Default.n_cliente = usernameTxtBox.Text.ToLower();

                if (passwordHidden.Visibility == Visibility.Visible)
                {
                    cmd.Parameters.AddWithValue("@pass", passwordHidden.Password.ToLower());
                    cmd.ExecuteNonQuery();
                    count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0)
                    {
                        // GOES TO FIRST SECTION OF THE APPLICATION
                        MainWindow program = new MainWindow();
                        program.Show();
                        Close();
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
                        // GOES TO FIRST SECTION OF THE APPLICATION
                        MainWindow program = new MainWindow();
                        program.Show();
                        Close();
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

        private void ForgotPass_Click(object sender, MouseButtonEventArgs e)
        {
            // GOES TO FORGOT PASSWORD
            ForgotPassword forgot = new ForgotPassword();
            forgot.Show();
            Close();
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
            usernameTxtBox.Focus();
        }
    }
}
