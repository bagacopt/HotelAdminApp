using PAP___RECEPTIONIST_HOTEL.Properties;
using System;
using System.Data.SqlClient;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PAP___RECEPTIONIST_HOTEL.MVVM.View.Client.SubView
{
    public partial class ServicesRequests : Window
    {
        public ServicesRequests()
        {
            InitializeComponent();
        }

        // CONNECTION
        readonly SqlConnection con = new SqlConnection(Settings.Default.ConnectionString);

        // VARIABLES
        string data;
        int idReservation, idRequest;

        // OBJECTS
        TextBox textBox = new TextBox();

        private void ServicesRequests_Loaded(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            if (PrimaryView.Requests.typeService == "Manutenção")
            {
                quantityLabel.Visibility = Visibility.Collapsed;
                quantityUpDown.Visibility = Visibility.Collapsed;
            }

            if (SubManageRequests.verification == "edit this")
            {
                if (PrimaryView.Requests.typeService == "Manutenção")
                {
                    titleLabel.Content = SubManageRequests.labelValues.ElementAtOrDefault(0);
                    mobileTxtBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(1);
                    emailTxtBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(2);
                    descriptionTxtBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(4);
                }
                else
                {
                    titleLabel.Content = SubManageRequests.labelValues.ElementAtOrDefault(0);
                    mobileTxtBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(1);
                    emailTxtBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(2);
                    textBox = (TextBox)quantityUpDown.Template.FindName("PART_TextBox", quantityUpDown);
                    textBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(3);
                    descriptionTxtBox.Text = SubManageRequests.labelValues.ElementAtOrDefault(4);
                }
            }
            else
            {
                nRoomLabel.Content = PrimaryView.ControlPanel.nRoom;
                titleLabel.Content = PrimaryView.Requests.serviceName;

                data = "SELECT Reservations.id FROM Reservations INNER JOIN Users " +
                    "ON Reservations.id = Users.reservation_id WHERE Users.username = @user AND Reservations.active = 1";

                using (SqlCommand cmd = new SqlCommand(data, con))
                {
                    cmd.Parameters.AddWithValue("@user", Settings.Default.n_cliente);

                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            idReservation = Convert.ToInt32(reader["id"]);
                        }
                    }
                }
            }

            // CLOSE CONNECTION
            con.Close();
        }

        private void OrderButton_Click(object sender, RoutedEventArgs e)
        {
            // OPEN CONNECTION
            con.Open();

            if (SubManageRequests.verification == "edit this")
            {
                try
                {
                    if (PrimaryView.Requests.typeService != "Manutenção")
                    {
                        data = "UPDATE Requests SET phone_number = @phone, email = @email, [desc] = @desc, quantity = @quantity WHERE id = @idRequest";

                        using (SqlCommand cmd = new SqlCommand(data, con))
                        {
                            cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                            cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                            cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                            cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox.Text));
                            cmd.Parameters.AddWithValue("@idRequest", SubManageRequests.client);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    else
                    {
                        data = "UPDATE Requests SET phone_number = @phone, email = @email, [desc] = @desc WHERE id = @idRequest";

                        using (SqlCommand cmd = new SqlCommand(data, con))
                        {
                            cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                            cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                            cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                            cmd.Parameters.AddWithValue("@idRequest", SubManageRequests.client);

                            cmd.ExecuteNonQuery();
                        }
                    }
                }
                catch
                {

                }
                

                MessageBox.Show("Pedido editado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                if (PrimaryView.Requests.typeService != "Manutenção")
                {
                    data = "INSERT INTO Requests(phone_number, email, [desc], services_id, name, quantity, active, state) " +
                    "VALUES(@phone, @email, @desc, @serviceID, @name, @quantity, 1, @state)";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                        cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                        cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                        cmd.Parameters.AddWithValue("@serviceID", PrimaryView.Requests.serviceID);
                        cmd.Parameters.AddWithValue("@name", titleLabel.Content);
                        cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox.Text));
                        cmd.Parameters.AddWithValue("@state", "PENDENTE");

                        cmd.ExecuteNonQuery();
                    }

                    data = "SELECT id FROM Requests WHERE phone_number = @phone AND email = @email " +
                        "AND [desc] = @desc AND services_id = @serviceID AND name = @name AND quantity = @quantity AND active = 1 AND state = 'PENDENTE'";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                        cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                        cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                        cmd.Parameters.AddWithValue("@serviceID", PrimaryView.Requests.serviceID);
                        cmd.Parameters.AddWithValue("@name", titleLabel.Content);
                        cmd.Parameters.AddWithValue("@quantity", Convert.ToInt32(textBox.Text));


                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idRequest = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }

                    data = "INSERT INTO Reservations_Requests(reservation_id, request_id) VALUES (@idReservation, @idRequest)";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@idReservation", Convert.ToInt32(idReservation));
                        cmd.Parameters.AddWithValue("@idRequest", Convert.ToInt32(idRequest));

                        cmd.ExecuteNonQuery();
                    }
                }
                else
                {
                    data = "INSERT INTO Requests(phone_number, email, [desc], services_id, name, active, state)" +
                    "VALUES(@phone, @email, @desc, 3, @name, 1, @state)";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                        cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                        cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                        cmd.Parameters.AddWithValue("@name", titleLabel.Content);
                        cmd.Parameters.AddWithValue("@state", "PENDENTE");

                        cmd.ExecuteNonQuery();
                    }

                    data = "SELECT id FROM Requests WHERE phone_number = @phone AND email = @email AND [desc] = @desc " +
                        "AND services_id = 3 AND name = @name AND active = 1 AND state = 'PENDENTE'";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@phone", mobileTxtBox.Text);
                        cmd.Parameters.AddWithValue("@email", emailTxtBox.Text);
                        cmd.Parameters.AddWithValue("@desc", descriptionTxtBox.Text);
                        cmd.Parameters.AddWithValue("@name", titleLabel.Content);

                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                idRequest = Convert.ToInt32(reader["id"]);
                            }
                        }
                    }

                    Console.WriteLine(idRequest);

                    data = "INSERT INTO Reservations_Requests(reservation_id, request_id) VALUES (@idReservation, @idRequest)";

                    using (SqlCommand cmd = new SqlCommand(data, con))
                    {
                        cmd.Parameters.AddWithValue("@idReservation", Convert.ToInt32(idReservation));
                        cmd.Parameters.AddWithValue("@idRequest", Convert.ToInt32(idRequest));

                        cmd.ExecuteNonQuery();
                    }
                }

                MessageBox.Show("Pedido criado com sucesso!", "Sucesso", MessageBoxButton.OK, MessageBoxImage.Information);
            }

            // CLOSE CONNECTION
            con.Close();

            BackButton_Click(sender, e);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // CLOSE THE FORM
            Close();
            Forms.MainWindow mainWindow = new Forms.MainWindow();
            mainWindow.Show();
        }

        private void IncrementButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textBox.Text, out int currentValue))
            {
                textBox.Text = (currentValue + 1).ToString();
            }
            else
            {
                MessageBox.Show("Não pode conter letras!!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            
        }

        private void DecreaseButton_Click(object sender, RoutedEventArgs e)
        {
            if (int.TryParse(textBox.Text, out int currentValue))
            {
                textBox.Text = (currentValue - 1).ToString();
            }
            else
            {
                MessageBox.Show("Não pode conter letras!!", "Aviso", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            // LET THE USER MOVE THE WINDOW FROM WHENEVER HE WANTS
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }
    }
}