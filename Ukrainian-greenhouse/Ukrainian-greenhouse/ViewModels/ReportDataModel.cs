using Npgsql;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using Ukrainian_greenhouse.Views;
using static Npgsql.Replication.PgOutput.Messages.RelationMessage;

namespace Ukrainian_greenhouse.ViewModels
{
    class ReportDataModel : BaseViewModel, INotifyPropertyChanged
    {
        public string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=Ukrainian-greenhouse";
        private NpgsqlConnection connection;
        CultureItemID cultureItemID = new CultureItemID();
        private CultureItemID _cultureItemID;
        public ReportDataModel(CultureItemID cultureItemID)
        {
            _cultureItemID = cultureItemID;
        }
        public ObservableCollection<CultureItem> CmbBoxItems { get; set; } = new ObservableCollection<CultureItem>();

        public ReportDataModel()
        {
            connection = new NpgsqlConnection(connectionString);
            SelectData();
            InsertData();
            LoadData();
            GreenHouse_Monitoring();
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
                DateTime reportTime = SelectedCultureItem._timestampReport;
                _timestampReport = reportTime;
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

                using (NpgsqlCommand cmd = new NpgsqlCommand("SELECT timestamp_report FROM greenhouse_monitoring", connection))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        CultureItem cultureItem = new CultureItem
                        {
                            _timestampReport = reader.GetDateTime(0)
                        };

                        CmbBoxItems.Add(cultureItem);
                    }
                }
            }
        }
        private DateTime _timestampReport;
        public DateTime TimestampReport
        {
            get { return _timestampReport; }
            set
            {
                _timestampReport = value;
                OnPropertyChanged(nameof(TimestampReport));
            }
        }
        private DateTime _timestampInformation;
        public DateTime TimestampInformation
        {
            get { return _timestampInformation; }
            set
            {
                _timestampInformation = value;
                OnPropertyChanged(nameof(TimestampInformation));
            }
        }

        private double _temperatureInformation;
        public double TemperatureInformation
        {
            get { return _temperatureInformation; }
            set
            {
                _temperatureInformation = value;
                OnPropertyChanged(nameof(TemperatureInformation));
            }
        }

        private double _humidityInformation;
        public double HumidityInformation
        {
            get { return _humidityInformation; }
            set
            {
                _humidityInformation = value;
                OnPropertyChanged(nameof(HumidityInformation));
            }
        }

        private DateTime _irrigationTimeInformation;
        public DateTime IrrigationTimeInformation
        {
            get { return _irrigationTimeInformation; }
            set
            {
                _irrigationTimeInformation = value;
                OnPropertyChanged(nameof(IrrigationTimeInformation));
            }
        }

        private double _irrigationVolumeInformation;
        public double IrrigationVolumeInformation
        {
            get { return _irrigationVolumeInformation; }
            set
            {
                _irrigationVolumeInformation = value;
                OnPropertyChanged(nameof(IrrigationVolumeInformation));
            }
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
            get { return _numberOfLamps; }
            set
            {
                _numberOfLamps = value;
                OnPropertyChanged(nameof(NumberOfLamps));
            }
        }
        private double _totalEnergyConsumed;
        public double TotalEnergyConsumed
        {
            get => _totalEnergyConsumed;
            set
            {
                _totalEnergyConsumed = value;
                OnPropertyChanged(nameof(TotalEnergyConsumed));
            }
        }
        private double _soilMoisture;
        public double SoilMoisture
        {
            get => _soilMoisture;
            set
            {
                _soilMoisture = value;
                OnPropertyChanged(nameof(SoilMoisture));
            }
        }
        private void SelectData()
        {
            try
            {
                using (connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string selectQuery = "SELECT CC.timestamp, CC.temperature, CC.humidity, WS.irrigation_time, WS.irrigation_volume,  EM.energy_consumed, EM.number_of_lamps\r\nFROM climate_control AS CC RIGHT JOIN watering_schedule AS WS ON CC.list_id = WS.list_id\r\nRIGHT JOIN energy_management AS EM ON WS.list_id = EM.list_id\r\nWHERE WS.list_id = @Listid ORDER BY CC.id DESC, WS.watering_id DESC, EM.energy_id DESC LIMIT 1;";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(selectQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@ListId", 1);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                _timestampInformation = reader.GetDateTime(0);
                                _temperatureInformation = reader.GetDouble(1);
                                _humidityInformation = reader.GetDouble(2);
                                _irrigationTimeInformation = reader.GetDateTime(3);
                                _irrigationVolumeInformation = reader.GetDouble(4);
                                _energyConsumed = reader.GetDateTime(5);
                                _numberOfLamps = reader.GetInt32(6);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while fetching data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void InsertData()
        {
            _timestampReport = DateTime.Now;
            double energyPerLamp = 0.036;
            _totalEnergyConsumed = energyPerLamp * _numberOfLamps;
            double greenhouse = 50;
            _soilMoisture = (_irrigationVolumeInformation / greenhouse) * 100;
            try
            {
                using (connection = new NpgsqlConnection(connectionString))
                {
                    connection.Open();

                    string insertQuery = "INSERT INTO greenhouse_monitoring (list_id, timestamp_report, timestamp_information , temperature_information, humidity_information, irrigation_time_information , irrigation_volume_information, energy_consumed, number_of_lamps, total_energy_consumed, soil_moisture)" +
                                         "VALUES (@list_id, @timestamp_report, @timestamp_information , @temperature_information, @humidity_information, @irrigation_time_information, @irrigation_volume_information, @energy_consumed, @number_Of_Lamps, @total_energy_consumed, @soil_moisture)";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                    {
                        cmd.Parameters.AddWithValue("@list_Id", 1);
                        cmd.Parameters.AddWithValue("@timestamp_report", _timestampReport);
                        cmd.Parameters.AddWithValue("@timestamp_information", _timestampInformation);
                        cmd.Parameters.AddWithValue("@temperature_information", _temperatureInformation);
                        cmd.Parameters.AddWithValue("@humidity_information", _humidityInformation);
                        cmd.Parameters.AddWithValue("@irrigation_time_information", _irrigationTimeInformation);
                        cmd.Parameters.AddWithValue("@irrigation_volume_information", _irrigationVolumeInformation);
                        cmd.Parameters.AddWithValue("@energy_consumed", _energyConsumed);
                        cmd.Parameters.AddWithValue("@number_Of_Lamps", _numberOfLamps);
                        cmd.Parameters.AddWithValue("@total_energy_consumed", _totalEnergyConsumed);
                        cmd.Parameters.AddWithValue("@soil_Moisture", _soilMoisture);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error while saving data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void GreenHouse_Monitoring()
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    string query = "SELECT timestamp_information, temperature_information, humidity_information, " +
                    "irrigation_time_information, irrigation_volume_information, energy_consumed, number_of_lamps " +
                    "FROM greenhouse_monitoring WHERE list_id = @ListId ORDER BY id DESC LIMIT 1";

                    using (NpgsqlCommand cmd = new NpgsqlCommand(query, connection))
                    {
                        cmd.Parameters.AddWithValue("@ListId", 1);

                        using (NpgsqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                TimestampInformation = reader.GetDateTime(0);
                                TemperatureInformation = reader.GetDouble(1);
                                HumidityInformation = reader.GetDouble(2);
                                IrrigationTimeInformation = reader.GetDateTime(3);
                                IrrigationVolumeInformation = reader.GetDouble(4);
                                EnergyConsumed = reader.GetDateTime(5);
                                NumberOfLamps = reader.GetInt32(6);
                            }
                        }
                    }
                }
                catch (NpgsqlException ex)
                {
                    MessageBox.Show($"Error while fetching data: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
