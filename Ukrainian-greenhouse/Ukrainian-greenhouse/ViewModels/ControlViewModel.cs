using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{
    class ControlViewModel : BaseViewModel, INotifyPropertyChanged
    {
        public string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        CultureItemID cultureItemID = new CultureItemID();
        public ObservableCollection<CultureItem> CmbBoxItems { get; set; } = new ObservableCollection<CultureItem>();
        
        private ICommand _climateControl;
        public ICommand ClimateControl
        {
            get
            {
                return _climateControl ?? (_climateControl = new RelayCommand(
                    param => Climate_Control()
                ));
            }
        }
        private ICommand _waterControl;
        public ICommand WaterControl
        {
            get
            {
                return _waterControl ?? (_waterControl = new RelayCommand(
                    param => Watering_Schedule()
                ));
            }
        }
        private ICommand _energyManagement;
        public ICommand EnergyManagement
        {
            get
            {
                return _energyManagement ?? (_energyManagement = new RelayCommand(
                    param => Energy_Management()
                ));
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
        private ICommand _edit;
        public ICommand Edit
        {
            get
            {
                return _edit ?? (_edit = new RelayCommand(
                    param => Edit_Data()
                ));
            }
        }
        private ICommand _monitoringData;
        public ICommand MonitoringData
        {
            get
            {
                return _monitoringData ?? (_monitoringData = new RelayCommand(
                    param => Monitoring_Data()
                ));
            }
        }
        public ControlViewModel()
        {
            LoadData();
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
                        CmbBoxItems.Add(cultureItem);
                    }
                }
            }
            OnPropertyChanged(nameof(CmbBoxItems));
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

        private void ComboBox_SelectionChanged()
        {
            if (SelectedCultureItem != null)
            {
                int id = SelectedCultureItem.Id;
                string name = SelectedCultureItem.Name;
            }
        }
        private void Edit_Data()
        {
            EditCmb editCmb = new EditCmb();
            editCmb.Show();
        }
        private void Climate_Control()
        {
            if (SelectedCultureItem != null)
            {
                cultureItemID.Id = SelectedCultureItem.Id;

                ClimateControlEditor climatControlEditor = new ClimateControlEditor();
                climatControlEditor.DataContext = new ClimateControlEditorModel(cultureItemID);
                climatControlEditor.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть культуру зі списку, перш ніж додавати дані клімат-контролю.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Watering_Schedule()
        {
            if (SelectedCultureItem != null)
            {
                cultureItemID.Id = SelectedCultureItem.Id;

                WateringControlEditor wateringControlEditor = new WateringControlEditor();
                wateringControlEditor.DataContext = new WateringControlEditorModel(cultureItemID);
                wateringControlEditor.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть культуру зі списку, перш ніж додавати дані розкладу поливу.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Energy_Management()
        {
            if (SelectedCultureItem != null)
            {
                cultureItemID.Id = SelectedCultureItem.Id;

                EnergyControlEditor energyControlEditor = new EnergyControlEditor();
                energyControlEditor.DataContext = new EnergyControlEditorModel(cultureItemID);
                energyControlEditor.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть культуру зі списку, перш ніж додавати дані керування енергоспоживанням.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void Monitoring_Data()
        {
            if (SelectedCultureItem != null)
            {
                cultureItemID.Id = SelectedCultureItem.Id;

                MonitoringData monitoringControlEditor = new MonitoringData();
                monitoringControlEditor.DataContext = new MonitoringDataModel(cultureItemID);
                monitoringControlEditor.Show();
            }
            else
            {
                MessageBox.Show("Будь ласка, виберіть культуру в ComboBox, перш ніж переглядати дані моніторингу.", "Помилка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}