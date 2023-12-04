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
using Npgsql;

namespace Ukrainian_greenhouse
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string connectionString = "Host = localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        public MainWindow()
        {
            InitializeComponent();
            connection = new NpgsqlConnection(connectionString);

        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Close();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (Login.Text != "" && Password.Password != "")
            {
                string login = Login.Text;
                string password = Password.Password;

                // Виклик методу для перевірки логіну та паролю
                bool isValidUser = ValidateUser(login, password);

                if (isValidUser)
                {
                    // Якщо користувач з таким логіном та паролем знайдений
                    // Тут ви можете виконати дії, які потрібно виконати при успішному вході
                    MessageBox.Show("Успішний вхід!");
                }
                else
                {
                    // Повідомлення про невірний логін або пароль
                    MessageBox.Show("Невірний логін або пароль!");
                }
            }
            else
            {
                // Повідомлення про те, що поля логіну та пароля повинні бути заповненими
                MessageBox.Show("Будь ласка, введіть логін та пароль!");
            }
        }

        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE login = @Login AND password = @Password";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("Login", login);
                    cmd.Parameters.AddWithValue("Password", password);
                    isValid = (long)cmd.ExecuteScalar() > 0;
                }
            }
            catch (NpgsqlException ex)
            {
                
            }
            finally
            {
                connection.Close();
            }
            return isValid;
        }

    }
}
