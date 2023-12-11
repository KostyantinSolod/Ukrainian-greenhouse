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
using Npgsql;


namespace Ukrainian_greenhouse
{
    public partial class Registration : Window
    {
        public string connectionString = "Host = localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        public Registration()
        {
            InitializeComponent();
            connection = new NpgsqlConnection(connectionString);

        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Login = new MainWindow();
            Login.Show();
            this.Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            
            loginText.Text = "";
            RepeatpasswordText.Text = "";
            if (LoginText.Text != "" || PasswordText.Password != "" || SecondPasswordText.Password != "")
            {
                if (LoginText.Text.Contains(" ")  || PasswordText.Password.Contains(" ") || SecondPasswordText.Password.Contains(" "))
                {
                    MessageBox.Show("Пробіли в полях недопустимі!");
                    return;
                }
                else if (PasswordText.Password == SecondPasswordText.Password)
                {
                    string login = LoginText.Text;

                    if (!IsLoginExist(login))
                    {
                        Login_Click(sender, e);
                    }
                    else
                    {
                        loginText.Text = "Цей логін вже існує";
                    }
                }
                else
                {
                    RepeatpasswordText.Text = "Вказаний пароль відрізняється від нового пароля";
                }
            }
            else
            {
                MessageBox.Show("Всі поля не заповненні!");
            }
 
        }

        private bool IsLoginExist(string login)
        {
            bool exist = false;
            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE login = @Login";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("Login", login);
                    exist = (long)cmd.ExecuteScalar() > 0;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Помилка перевірки логіна: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            return exist;
        }

        private void RegisterNewUser(string login, string password)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO users (login, password, role) VALUES (@Login, @Password, @Role)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("Login", login);
                    cmd.Parameters.AddWithValue("Password", password);
                    cmd.Parameters.AddWithValue("Role", 0);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Реєстрація успішна!");
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Помилка реєстрації користувача: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

    }
}
