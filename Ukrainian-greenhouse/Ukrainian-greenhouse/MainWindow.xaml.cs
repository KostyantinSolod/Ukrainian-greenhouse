using System.Windows;
using Npgsql;

namespace Ukrainian_greenhouse
{
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

                bool isValidUser = ValidateUser(login, password);

                if (isValidUser)
                {
                    Control control = new Control();
                    control.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Невірний логін або пароль!");
                }
            }
            else
            {
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
