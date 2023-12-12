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
    public partial class ClimateControlEditor : Window
    {
        public DateTime Timestamp { get; private set; }
        public float Temperature { get; private set; }
        public float Humidity { get; private set; }

        public ClimateControlEditor()
        {
            InitializeComponent();
        }
       
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Timestamp = DateTime.Parse(TxtTimestamp.Text);
                Temperature = float.Parse(TxtTemperature.Text);
                Humidity = float.Parse(TxtHumidity.Text);

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
