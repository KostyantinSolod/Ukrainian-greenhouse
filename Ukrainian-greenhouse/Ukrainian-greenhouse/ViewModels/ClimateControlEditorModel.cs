using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{
    class ClimateControlEditorModel: BaseViewModel
    {
        string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
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
        public ICommand SaveCommand
        {
            get
            {
                return _saveCommand ?? (_saveCommand = new RelayCommand(
                    param => SaveClick()
                ));
            }
        }
        private int _selectedCultureItemId;
        public int SelectedCultureItemId
        {
            get => _selectedCultureItemId;
            set
            {
                _selectedCultureItemId = value;
                OnPropertyChanged(nameof(SelectedCultureItemId));
            }
        }
        private long _listId;

        public ClimateControlEditorModel(long listId)
        {
            _listId = listId;
        }
        private void SaveClick()
        {
            
            try
            {
                using (connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO climate_control (list_id, timestamp, temperature, humidity) " +
                                         "VALUES (@listId, @timestamp, @temperature, @humidity)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@listId", _listId) ;
                        cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                        cmd.Parameters.AddWithValue("@temperature", Temperature);
                        cmd.Parameters.AddWithValue("@humidity", Humidity);

                        cmd.ExecuteNonQuery();
                    }
                }
                MessageBox.Show("Climate control data added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
