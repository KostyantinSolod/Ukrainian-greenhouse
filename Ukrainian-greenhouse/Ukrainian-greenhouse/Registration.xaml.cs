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

namespace Ukrainian_greenhouse
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            MainWindow Login = new MainWindow();
            Login.Show();
            this.Close();
        }

        private void Registration_Click(object sender, RoutedEventArgs e)
        {
            
            if (PasswordText.Password == SecondPasswordText.Password)
            {

            }
            else
            {
                RepeatpasswordText.Text = "Вказаний пароль відрізняється від нового пароля";
            }
        }
    }
}
