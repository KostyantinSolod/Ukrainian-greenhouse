using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows;
using Ukrainian_greenhouse.Views;

namespace Ukrainian_greenhouse.ViewModels
{
    class WateringControlEditorModel : BaseViewModel
    {
        string connectionString = "Host=localhost;Username=postgres;Password=2002;Database=control";
        private NpgsqlConnection connection;

        public WateringControlEditorModel(){}
        private CultureItemID _cultureItemID;
        public WateringControlEditorModel(CultureItemID cultureItemID)
        {
            _cultureItemID = cultureItemID;
        }
        public DateTime Timestamp { get; private set; }
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
        
        private double _irrigation_volume;
        public double IrrigationVolume
        {
            get => _irrigation_volume;
            set
            {
                _irrigation_volume = value;
                OnPropertyChanged(nameof(IrrigationVolume));
            }
        }
        //private readonly int _listId; // Додайте поле для зберігання list_id

        //public ClimateControlEditorModel(int listId)
        //{
        //    _listId = listId;
        //}
        private void SaveClick()
        {
            if (_irrigation_volume >= 0)
            {
                try
                {
                    using (connection = new NpgsqlConnection(connectionString))
                    {
                        connection.Open();

                        string insertQuery = "INSERT INTO watering_schedule (list_id, irrigation_time, irrigation_volume ) " +
                                             "VALUES (@listId, @irrigation_time, @irrigation_volume)";

                        using (NpgsqlCommand cmd = new NpgsqlCommand(insertQuery, connection))
                        {
                            cmd.Parameters.AddWithValue("@listId", _cultureItemID.Id);
                            cmd.Parameters.AddWithValue("@irrigation_time", DateTime.Now);
                            cmd.Parameters.AddWithValue("@irrigation_volume", _irrigation_volume);

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
