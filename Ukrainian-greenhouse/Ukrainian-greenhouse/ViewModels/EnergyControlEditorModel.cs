using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Ukrainian_greenhouse.ViewModels
{
    class EnergyControlEditorModel : BaseViewModel
    {
        private readonly string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=control";
        private NpgsqlConnection connection;
        public ObservableCollection<CultureItem> CmbBoxItems { get; set; } = new ObservableCollection<CultureItem>();
        public EnergyControlEditorModel() 
        {
            connection = new NpgsqlConnection(connectionString);
            LoadData();
        }
        private CultureItemID _cultureItemID;
        public EnergyControlEditorModel(CultureItemID cultureItemID)
        {
            _cultureItemID = cultureItemID;
        }
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
        private ICommand _selectItem;
        public ICommand SelectItem
        {
            get
            {
                return _selectItem ?? (_selectItem = new RelayCommand(
                    param => LoadData()
                ));
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
        private ICommand _comboBoxSelectionChanged;
        public ICommand ComboBoxSelectionChanged
        {
            get
            {
                return _comboBoxSelectionChanged ?? (_comboBoxSelectionChanged = new RelayCommand(
                    param => ComboBox_SelectionChanged()
                ));
            }
        }

        private string _nameLamp;
        public string NameOfLamps
        {
            get { return _nameLamp; }
            set
            {
                _nameLamp = value;
                OnPropertyChanged(nameof(NameOfLamps));
            }
        }
        private void ComboBox_SelectionChanged()
        {
            if (SelectedCultureItem != null)
            {
                string name = SelectedCultureItem._nameLamp;
                _nameLamp = name;
            }
        }

        private int _selectedListId;
        public int SelectedListId
        {
            get { return _selectedListId; }
            set
            {
                _selectedListId = value;
                OnPropertyChanged(nameof(SelectedListId));
            }
        }
        private void LoadData()
        {
            using (connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT name_of_lamp FROM list_lamp", connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CultureItem cultureItem = new CultureItem
                        {
                            _nameLamp = reader.GetString(0)
                        };
                        CmbBoxItems.Add(cultureItem);
                    }
                }
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
                            cmd.Parameters.AddWithValue("@listId", _cultureItemID.Id);
                            cmd.Parameters.AddWithValue("@energy_consumed", DateTime.Now);
                            cmd.Parameters.AddWithValue("@number_of_lamps", _numberOfLamps);

                            cmd.ExecuteNonQuery();
                        }
                    }
                    MessageBox.Show("Дані управління електрикою додано!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Помилка під час збереження даних: {ex.Message}", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Введіть коректрні дані! \nМінімальна кількість ламп 0, максимальна 10!");
            }
        }
    }
}