﻿using BookManagement.WPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookManagement.WPF.ViewModels
{
    public class MainViewModel
    {
        public INavigator Navigator { get; set; } = new Navigator();
    }
}
