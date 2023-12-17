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
        private void Report_Data()
        {
                ReportData reportData = new ReportData();
                reportData.Show();
        }
        private void Diagram_Data()
        {
            DiagramData reportData = new DiagramData();
            reportData.Show();
        }
    }
}
