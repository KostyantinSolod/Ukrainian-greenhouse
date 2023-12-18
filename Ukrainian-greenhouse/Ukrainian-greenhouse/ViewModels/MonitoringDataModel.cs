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
    class MonitoringDataModel : BaseViewModel
    {
        CultureItemID cultureItemID = new CultureItemID();
        private CultureItemID _cultureItemID;
        public MonitoringDataModel() { }
        public MonitoringDataModel(CultureItemID cultureItemID)
        {
            _cultureItemID = cultureItemID;
        }
        private ICommand _reportData;
        public ICommand ReportData
        {
            get
            {
                return _reportData ?? (_reportData = new RelayCommand(
                    param => Report_Data()
                ));
            }
        }
        private ICommand _diagramData;
        public ICommand DiagramData
        {
            get
            {
                return _diagramData ?? (_diagramData = new RelayCommand(
                    param => Diagram_Data()
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
        private void Report_Data()
        {

            ReportData reportData = new ReportData();
            reportData.Show();
        }
        private void Diagram_Data()
        {
            MessageBox.Show("Кнопка розроблюється!\nОчікуйте оновлень...");
        }
    }
}
