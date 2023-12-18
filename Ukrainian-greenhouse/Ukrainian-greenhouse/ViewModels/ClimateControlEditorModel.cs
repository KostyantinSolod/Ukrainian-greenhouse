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
        string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=control";
        private NpgsqlConnection connection;
        public ClimateControlEditorModel() { }
        private CultureItemID _cultureItemID;
        public ClimateControlEditorModel(CultureItemID cultureItemID)
        {
            _cultureItemID = cultureItemID;
        }
        public DateTime Timestamp { get; private set; }
        
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

        private double _temperature;
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
        private void SaveClick()
        {
            if (_temperature > 0 && _humidity > 0)
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
                            cmd.Parameters.AddWithValue("@listId", _cultureItemID.Id);
                            cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);
                            cmd.Parameters.AddWithValue("@temperature", _temperature);
                            cmd.Parameters.AddWithValue("@humidity", _humidity);

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
            else
            {
                MessageBox.Show("Введіть коректрні дані!");
            }
        }

    }
}
