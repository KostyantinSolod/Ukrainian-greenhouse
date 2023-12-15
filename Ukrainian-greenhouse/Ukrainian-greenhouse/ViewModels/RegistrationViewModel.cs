using Npgsql;
using System;
using System.Windows;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        public string connectionString = "Host = localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        private string _login;
        public string Login
        {
            get => _login;
            set
            {
                _login = value;
                OnPropertyChanged(nameof(Login));
            }
        }

        private string _password;
        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        private string _confirmPassword;
        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged(nameof(ConfirmPassword));
            }
        }

        private ICommand _registerCommand;
        public ICommand RegisterCommand
        {
            get
            {
                return _registerCommand ?? (_registerCommand = new RelayCommand(
                    param => Register(),
                    param => CanRegister()
                ));
            }
        }

        private bool CanRegister()
        {
            return !string.IsNullOrWhiteSpace(Login) &&
                   !string.IsNullOrWhiteSpace(Password) &&
                   Password == ConfirmPassword &&
                   !Login.Contains(" ") &&
                   !Password.Contains(" ");
        }
        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Login = new MainWindow();
            Login.Show();
        }
        private void Register()
        {
            if (CanRegister())
            {
                if (IsLoginExist(Login))
                {
                    // Логін вже існує
                    // Обробка помилки
                }
                else
                {
                    // Реєстрація нового користувача
                    // RegisterNewUser(Login, Password);
                    // Якщо потрібно викликати метод RegisterNewUser, зверніться до вашого логіку реєстрації
                }
            }
            else
            {
                // Повідомлення про помилку - поля не заповнені або є неправильні дані
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

        // Метод для реєстрації нового користувача (якщо потрібно)
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
