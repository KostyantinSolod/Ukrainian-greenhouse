﻿using System;
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

namespace Ukrainian_greenhouse
{
    public partial class ClimateControlEditor : Window
    {
        public ClimateControlEditor()
        {
            InitializeComponent();

            DataContext = new ClimateControlEditorModel();
        }
       
    }
}
