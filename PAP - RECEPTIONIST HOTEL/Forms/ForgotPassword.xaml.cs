﻿using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Net;
using System.Net.Mail;
using System.Windows;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.Forms
{
    public partial class ForgotPassword : Window
    {
        public ForgotPassword()
        {
            InitializeComponent();
        }

        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;

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

        private void ForgotPass_Loaded(object sender, RoutedEventArgs e)
        {
            emailTxtBox.Focus();
        }

        private void ReplacePasswordButton_Click(object sender, RoutedEventArgs e)
        {
            MailMessage mail = new MailMessage
            {
                From = new MailAddress("a30360@aemtg.pt"), // EMAIL COMES FROM ME
                Subject = "Reposição da palavra-passe", // SUBJECT OF THE EMAIL
                Body = "Olá, a sua palavra-passe foi alterada para 1234.", // BODY OF THE EMAIL
                To = { emailTxtBox.Text } // EMAIL GOES TO EMAIL WROTE
            };

            // OPEN CONNECTION
            con.Open();

            using (var smtp = new SmtpClient("smtp.gmail.com", 587))
            {
                smtp.UseDefaultCredentials = false;
                smtp.EnableSsl = true;
                smtp.Credentials = new NetworkCredential("a30360@aemtg.pt", "papdesenvolvimento");
                try
                {
                    data = "SELECT email FROM Users WHERE email = @email";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                        cmd.ExecuteNonQuery();

                        if (cmd.ExecuteScalar().ToString() == emailTxtBox.Text)
                        {
                            smtp.Send(mail);
                            MessageBox.Show("E-mail enviado com sucesso!", "E-mail enviado", MessageBoxButton.OK, MessageBoxImage.Information);
                            MessageBox.Show("A voltar ao menu principal...", "A voltar...", MessageBoxButton.OK, MessageBoxImage.Information);

                            data = "UPDATE Users SET password = 1234 WHERE email = @email";

                            SqlCommand emailQuery = new SqlCommand(data, con);

                            emailQuery.Parameters.AddWithValue("@email", emailTxtBox.Text);
                            emailQuery.ExecuteNonQuery();
                        }
                        else
                        {
                            MessageBox.Show("Não foi possível encontrar este email registado nos nossos sistemas...", "Alerta!", MessageBoxButton.OK, MessageBoxImage.Warning);
                            MessageBox.Show("A voltar ao menu principal...", "A voltar...", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                    }                   
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Falha ao enviar e-mail");
                }
                // CLOSE CONNECTION
                con.Close();

                Login login = new Login();
                Close();
                login.Show();
            }
        }

        private void PressKey(object sender, KeyEventArgs e)
        {
            // PRESSES THE BUTTON OF THE LOGIN
            if (e.Key == Key.Enter)
            {
                ReplacePasswordButton_Click(sender, e);
            }

            // PRESSES ESCAPE
            if (e.Key == Key.Escape)
            {
                // CLOSES PROGRAM
                Application.Current.Shutdown();
            }
        }

        private void BackLogin_Click(object sender, MouseButtonEventArgs e)
        {
            Login login = new Login();
            login.Show();
            Close();
        }
    }
}