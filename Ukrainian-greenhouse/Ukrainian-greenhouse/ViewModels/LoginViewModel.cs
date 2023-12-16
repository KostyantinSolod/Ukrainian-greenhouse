using System;
using System.Security;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Npgsql;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        private readonly UserModel _userModel;
        public LoginViewModel()
        {
            _userModel = new UserModel();
            connection = new NpgsqlConnection(connectionString);
        }
        
        public UserModel User => _userModel;

        private ICommand _loginCommand;
        public ICommand LoginCommand
        {
            get
            {
                return _loginCommand ?? (_loginCommand = new RelayCommand(
                    param => Login()
                ));
            }
        }
        private ICommand _registrationCommand;
        public ICommand RegistrationCommand
        {
            get
            {
                return _registrationCommand ?? (_registrationCommand = new RelayCommand(
                    param => Registration()                   
                ));
            }
        }
        private string _password;
        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        private void Login()
        {
            if (User.Login != null && User.Password != null)
            {
                bool isValidUser = ValidateUser(User.Login, User.Password);

                if (isValidUser)
                {

                    ControlViewModel viewModel = new ControlViewModel();
                    Views.Control window = new Views.Control();
                    window.DataContext = viewModel;
                    window.Show();
                    Application.Current.MainWindow.Close();
                }
                else
                {
                    MessageBox.Show("Неправильний логін або пароль!");
                }
            }
            else
            {
                MessageBox.Show("Не всі поля заповнені");
            }
        }
        private bool ValidateUser(string login, string password)
        {
            bool isValid = false;
            
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT COUNT(*) FROM users WHERE login = @Login AND password = @Password";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.Add(new NpgsqlParameter("Login", NpgsqlTypes.NpgsqlDbType.Text));
                        cmd.Parameters["Login"].Value = login;
                        cmd.Parameters.Add(new NpgsqlParameter("Password", NpgsqlTypes.NpgsqlDbType.Text));
                        cmd.Parameters["Password"].Value = password;
                        int count = Convert.ToInt32(cmd.ExecuteScalar());

                        isValid = count > 0;
                    }
                }
                catch (NpgsqlException ex)
                {
                    
                }
            }
            return isValid;
        }
        private void Registration()
        {
            Registration RegistrationWindow = new Registration();
            RegistrationWindow.Show();
            Application.Current.MainWindow.Hide();
            
        }
        
    }
}
