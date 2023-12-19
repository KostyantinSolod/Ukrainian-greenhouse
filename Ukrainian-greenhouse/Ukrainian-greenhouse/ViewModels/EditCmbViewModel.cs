using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{

    internal class EditCmbViewModel : BaseViewModel
    {
        public string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        private CultureItemID _cultureItemID;
        public EditCmbViewModel(CultureItemID cultureItemID)
        {
            _cultureItemID = cultureItemID;
        }
        public ObservableCollection<CultureItem> CmbBoxItems { get; set; } = new ObservableCollection<CultureItem>();

        private EditCmb _editWindow;
        public EditCmb EditWindow
        {
            get => _editWindow;
            set
            {
                _editWindow = value;
                OnPropertyChanged(nameof(EditWindow));
            }
        }
        private void Edit_Click()
        {
            if (_editWindow != null)
            {
                _editWindow = new EditCmb();
                _editWindow.Close();
            }
        }
        private CultureItem _selectedCultureItem;

        public CultureItem SelectedCultureItem
        {
            get { return _selectedCultureItem; }
            set
            {
                _selectedCultureItem = value;
                OnPropertyChanged(nameof(SelectedCultureItem));
            }
        }

        private ICommand _addCulture;
        public ICommand AddCulture
        {
            get
            {
                return _addCulture ?? (_addCulture = new RelayCommand(
                    param => AddData()
                ));
            }
        }

        private string _nameOfCulture;
        public string NameOfCulture
        {
            get => _nameOfCulture;
            set
            {
                _nameOfCulture = value;
                OnPropertyChanged(nameof(NameOfCulture));
            }
        }
        private ICommand _deleteCommand;
        public ICommand DeleteCulture
        {
            get
            {
                return _deleteCommand ?? (_deleteCommand = new RelayCommand(
                    param => DeleteData()
                ));
            }
        }
        private bool IsCultureExist()
        {
            connection = new NpgsqlConnection(connectionString);
            bool exist = false;

            try
            {
                connection.Open();
                string query = "SELECT COUNT(*) FROM list WHERE name_of_culture = @name_of_culture";

                using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                {
                    cmd.Parameters.Add(new NpgsqlParameter("name_of_culture", NpgsqlTypes.NpgsqlDbType.Text));
                    cmd.Parameters["name_of_culture"].Value = _nameOfCulture;

                    // Execute the query and get the result
                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    // Check if the count is greater than 0, indicating the culture exists
                    exist = count > 0;
                }
            }
            catch (NpgsqlException ex)
            {
                Console.WriteLine("Помилка перевірки логіна: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }

            return exist;
        }

        private void DeleteData()
        {
            connection = new NpgsqlConnection(connectionString);
            try
            {
                connection.Open();

                if (SelectedCultureItem != null)
                {
                    int id = SelectedCultureItem.Id;
                    string tableName = SelectedCultureItem.Name;

                    string deleteClimat = $"DELETE FROM climate_control WHERE list_id = {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(deleteClimat, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string deleteWater = $"DELETE FROM watering_schedule WHERE list_id = {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(deleteWater, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string deleteEnergy = $"DELETE FROM energy_management WHERE list_id = {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(deleteEnergy, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string deleteMonitoring = $"DELETE FROM greenhouse_monitoring WHERE list_id = {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(deleteMonitoring, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string updateClimate = $"UPDATE climate_control SET list_id = list_id - 1 WHERE list_id > {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateClimate, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string updateWatering = $"UPDATE watering_schedule SET list_id = list_id - 1 WHERE list_id > {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateWatering, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string updateEnergy = $"UPDATE energy_management SET list_id = list_id - 1 WHERE list_id > {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateEnergy, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string updateMonitoring = $"UPDATE greenhouse_monitoring SET list_id = list_id - 1 WHERE list_id > {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateMonitoring, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string deleteCommand = $"DELETE FROM list WHERE list_id = {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(deleteCommand, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    string updateCommand = $"UPDATE list SET list_id = list_id - 1 WHERE list_id > {id}";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(updateCommand, connection))
                    {
                        cmd.ExecuteNonQuery();
                    }
                    LoadData();
                }
                MessageBox.Show("Успішно!");
                Edit_Click();
            }
            catch (NpgsqlException ex)
            {
            }
            finally
            {
                connection.Close();
            }

        }
        private void AddData()
        {
            if (!IsCultureExist())
            {
                connection = new NpgsqlConnection(connectionString);
                try
                {
                    connection.Open();

                    int rowCount = GetRowCount();

                    string query = "INSERT INTO list (list_id, name_of_culture) VALUES (@list_id, @name_of_culture)";
                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@list_id", rowCount + 1);
                        cmd.Parameters.AddWithValue("@name_of_culture", _nameOfCulture);

                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Успішно!");
                    }
                    Edit_Click();
                }
                catch (NpgsqlException ex)
                {
                    Console.WriteLine("Помилка: " + ex.Message);
                }
                finally
                {
                    connection.Close();
                }
            }
            else
            {
                MessageBox.Show("У вас вже є такий вид культури!");
            }
        }
        private int GetRowCount()
        {
            using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT COUNT(*) FROM list", connection))
            {
                int rowCount = Convert.ToInt32(cmd.ExecuteScalar());
                return rowCount;
            }
        }

        private void LoadData()
        {
            using (connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT * FROM list", connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    CmbBoxItems.Clear();

                    while (reader.Read())
                    {
                        CultureItem cultureItem = new CultureItem
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1)
                        };

                        CmbBoxItems.Add(cultureItem);
                    }
                }
            }
        }

        public EditCmbViewModel()
        {
            LoadData();
        }
    }
}
