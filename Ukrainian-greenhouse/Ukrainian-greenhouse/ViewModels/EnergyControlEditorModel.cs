using Npgsql;
using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ukrainian_greenhouse.ViewModels
{
    class EnergyControlEditorModel : BaseViewModel
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=control";
        private NpgsqlConnection connection;

        private DateTime _energyConsumed;
        public DateTime EnergyConsumed
        {
            get => _energyConsumed;
            set
            {
                _energyConsumed = value;
                OnPropertyChanged(nameof(EnergyConsumed));
            }
        }
        private SolidColorBrush _buttonBackground;
        public SolidColorBrush ButtonBackground
        {
            get => _buttonBackground;
            set
            {
                _buttonBackground = value;
                OnPropertyChanged(nameof(ButtonBackground));
            }
        }
        private int _numberOfLamps;
        public int NumberOfLamps
        {
            get => _numberOfLamps;
            set
            {
                _numberOfLamps = value;
                OnPropertyChanged(nameof(NumberOfLamps));
            }
        }
        private double _totalEnergyUse;
        public double TotalEnergyUse
        {
            get => _totalEnergyUse;
            set
            {
                _totalEnergyUse = value;
                OnPropertyChanged(nameof(TotalEnergyUse));
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
            if (_numberOfLamps >= 0 && _numberOfLamps <= 10)
            {
                try
                {
                    using (connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO energy_management (list_id, energy_consumed, number_of_lamps) " +
                                             "VALUES (@listId, @energy_consumed, @number_of_lamps)";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@listId", 2);
                            cmd.Parameters.AddWithValue("@energy_consumed", DateTime.Now);
                            cmd.Parameters.AddWithValue("@number_of_lamps", _numberOfLamps);

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
                MessageBox.Show("Введіть коректрні дані! \nМінімальна кількість ламп 0, максимальна 10!");
            }
        }
    }
}
