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

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM list", connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CultureItem cultureItem = new CultureItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                        CMB_Culture.Items.Add(cultureItem);
                    }
                }
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CMB_Culture.SelectedItem is CultureItem selectedCulture)
            {
                int id = selectedCulture.Id;
                string name = selectedCulture.Name;
            }
        }
    }
}
