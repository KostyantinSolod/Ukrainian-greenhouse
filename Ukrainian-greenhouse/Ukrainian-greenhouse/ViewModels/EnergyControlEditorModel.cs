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

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get => _timestamp;
            set
            {
                _timestamp = value;
                OnPropertyChanged(nameof(Timestamp));
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
        private bool _electricity;
        public bool Electricity
        {
            get => _electricity;
            set
            {
                _electricity = value;
                OnPropertyChanged(nameof(Electricity));
                UpdateButtonBackground();
            }
        }

        private ICommand _saveCommand;
        public ICommand SaveCommand => _saveCommand ?? (_saveCommand = new RelayCommand(param => SaveClick()));

        private void SaveClick()
        {
            try
            {
                using (connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO energy_management (list_id, electricity, energy_consumed) " +
                                         "VALUES (@listId, @electricity, @timestamp)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@listId", 2);
                        cmd.Parameters.AddWithValue("@electricity", _electricity);
                        cmd.Parameters.AddWithValue("@timestamp", DateTime.Now);

                        cmd.ExecuteNonQuery();
                    }
                }

                LoadLastData(); // Після збереження, перезавантажуємо дані для відображення
                MessageBox.Show("Climate control data added successfully!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadLastData()
        {
            try
            {
                using (connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT electricity FROM energy_management ORDER BY energy_id DESC LIMIT 1";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, connection))
                    {
                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _electricity = reader.GetBoolean(0);
                                Timestamp = DateTime.Now; // Добавлено для примера, так как energy_consumed не используется
                            }
                        }
                    }
                }

                UpdateButtonBackground();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while loading data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateButtonBackground()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                ButtonBackground = _electricity ? Brushes.Green : Brushes.Red;
            });
        }
    }
}
