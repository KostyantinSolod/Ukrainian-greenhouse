using Npgsql;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private static string connectionString = "Host = localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        public NpgsqlConnection connection = new NpgsqlConnection(connectionString);

        private Registration _registrationWindow;
        public Registration RegistrationWindow
        {
            get => _registrationWindow;
            set
            {
                _registrationWindow = value;
                OnPropertyChanged(nameof(RegistrationWindow));
            }
        }
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
                    param => Register()
                ));
            }
        }
        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(
                    param => Login_Click()
                 ));
            }
        }

        private bool CanRegister()
        {
            return true;
        }
        private void Login_Click()
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            if (_registrationWindow != null)
            {
                _registrationWindow.Close();
            }
        }
        private void Register()
        {
            if (_login != null && _password != null && _confirmPassword != null)
            {
                string password = Password;
                if (CanRegister())
                {
                    if (IsLoginExist())
                    {
                        MessageBox.Show("Логін вже існує");
                    }
                    else
                    {
                        if (_password == _confirmPassword)
                        {
                            RegisterNewUser();
                            Application.Current.MainWindow.Show();
                            _registrationWindow.Close();
                        }
                        else
                        {
                            MessageBox.Show("Вказаний пароль відрізняється від нового пароля");
                        }
                    }
                }
                else
                {
                    MessageBox.Show("Всі поля не заповнені!");
                }
            }
            else
            {
                MessageBox.Show("Не всі поля заповнені");
            }
        }

        private bool IsLoginExist()
        {
            bool exist = false;
            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM users WHERE login = @Login";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("Login", NpgsqlTypes.NpgsqlDbType.Text));
                    cmd.Parameters["Login"].Value = _login;
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

        private void RegisterNewUser()
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO users (login, password) VALUES (@Login, @Password)";
                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@Login", _login);
                    cmd.Parameters.AddWithValue("@Password", _password);

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
