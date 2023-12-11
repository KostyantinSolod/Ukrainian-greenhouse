using Npgsql;
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
    /// Логика взаимодействия для Control.xaml
    /// </summary>
    public partial class Control : Window
    {
        public string connectionString = "Host = localhost;Username=postgres;Password=2002;Database=control";
        private NpgsqlConnection connection;
        public Control()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                // Запрос к базе данных для получения данных из таблицы "list"
                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM list", connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        // Создаем объект CultureItem для каждой записи из базы данных
                        CultureItem cultureItem = new CultureItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                        // Добавляем элемент в комбо-бокс
                        CMB_Culture.Items.Add(cultureItem);
                    }
                }
            }
        }
        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CMB_Culture.SelectedItem is CultureItem selectedCulture)
            {
                // Теперь у вас есть доступ к выбранному элементу
                int id = selectedCulture.Id;
                string name = selectedCulture.Name;

                // Выполните необходимые действия с выбранным элементом
            }
        }
    }
}
