using System.Windows;
using Ukrainian_greenhouse.ViewModels;

namespace Ukrainian_greenhouse.Views 
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new LoginViewModel();
        }
    }
}
