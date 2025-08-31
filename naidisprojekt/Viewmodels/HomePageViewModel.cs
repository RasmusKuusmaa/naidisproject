using naidisprojekt.Models;
using naidisprojekt.Pages;
using naidisprojekt.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace naidisprojekt.Viewmodels
{
    public class HomePageViewModel : BaseViewModel
    {
        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value; 
            }
        }

        public HomePageViewModel()
        {
            
        }

     

    }
}
