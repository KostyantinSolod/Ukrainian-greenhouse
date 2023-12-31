﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ukrainian_greenhouse.ViewModels
{
    class CultureItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int _lampId { get; set; }
        public string _nameLamp { get; set; }
        public DateTime _timestampReport { get; set; }
        public override string ToString()
        {
            return Name;
        }
    }
}