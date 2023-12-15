using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Ukrainian_greenhouse.ViewModels;


namespace Ukrainian_greenhouse.Views
{
    public partial class Registration : Window
    {
        public Registration()
        {
            InitializeComponent();
            var viewModel = new RegistrationViewModel();
            DataContext = viewModel;
            viewModel.RegistrationWindow = this;
        }
    }

}
