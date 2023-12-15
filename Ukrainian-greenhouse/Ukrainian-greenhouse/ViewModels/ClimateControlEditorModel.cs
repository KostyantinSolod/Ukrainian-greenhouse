using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse
{
    public class ClimateControlEditorModel: BaseViewModel
    {
        public string connectionString = "Host = localhost;Username=postgres;Password=2002;Database=control";
        private NpgsqlConnection connection;
        public DateTime Timestamp { get; private set; }
        private double _temperature;
        
        private Control _ControlWindow;
        public Control ControlWindow
        {
            get => _ControlWindow;
            set
            {
                _ControlWindow = value;
                OnPropertyChanged(nameof(ControlWindow));
            }
        }
        public double Temperature
        {
            get => _temperature;
            set
            {
                _temperature = value;
                OnPropertyChanged(nameof(Temperature));
            }
        }
        private double _humidity;
        public double Humidity
        {
            get => _humidity;
            set
            {
                _humidity = value;
                OnPropertyChanged(nameof(Humidity));
            }
        }
        private ICommand _saveCommand;
        //public ICommand SaveCommand
        //{
        //    get
        //    {
        //        return _saveCommand ?? (_saveCommand = new RelayCommand(
        //            param => Con()
        //        ));
        //    }
        //}
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Temperature = _temperature;
                Humidity = _humidity;
                Application.Current.MainWindow.Show();
                if (_ControlWindow != null)
                {
                    _ControlWindow.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
