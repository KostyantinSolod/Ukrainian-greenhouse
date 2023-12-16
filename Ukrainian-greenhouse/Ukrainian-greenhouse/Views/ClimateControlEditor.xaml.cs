using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Ukrainian_greenhouse.ViewModels;

namespace Ukrainian_greenhouse.Views
{
    public partial class ClimateControlEditor : Window
    {
        public ClimateControlEditor(long listId)
        {
            InitializeComponent();

            // Однак, пам'ятайте перевірити на null у випадку необхідності.
            if (listId != null)
            {
                ClimateControlEditorModel viewModel = new ClimateControlEditorModel(listId);
                DataContext = viewModel;
            }
            else
            {
                // Обробка випадку, якщо listId - null.
                MessageBox.Show("List_id is null!", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                Close(); // Закрити вікно в такому випадку, якщо щось пішло не так.
            }
        }
    }
}
