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
                    param => Login(),
                    param => !string.IsNullOrEmpty(User.Login) && User.Password != null
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
        private PasswordBox _password;
        public PasswordBox Password
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
            bool isValidUser = ValidateUser(User.Login, User.Password);

            if (isValidUser)
            {

                ControlViewModel viewModel = new ControlViewModel();
                Control window = new Control();
                window.DataContext = viewModel;
                window.Show();
            }
            else
            {
                MessageBox.Show("Неправильний логін або пароль!");
            }
        }
        private bool ValidateUser(string login, PasswordBox password)
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
                        cmd.Parameters.AddWithValue("Login", login);
                        cmd.Parameters.AddWithValue("Password", password);
                        isValid = (long)cmd.ExecuteScalar() > 0;
                    }
                }
                catch (NpgsqlException ex)
                {
                    // Обработка ошибки подключения или запроса
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
