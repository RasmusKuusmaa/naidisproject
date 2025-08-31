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
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ICommand SelectListingCommand { get; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Listing> Listings { get; set; }
        private List<Listing> allListings;
       //private readonly Dbservice dbservice = new Dbservice();

        public ICommand SelectCategoryCommand { get; }

        public HomePageViewModel()
        {
            
        }

        //private async void LoadListingsAsync()
        //{
        //    allListings = await dbservice.GetAllListingsAsync();
        //    UpdateListingsForSelectedCategory();
        //}

       
        //private void UpdateSelection(Category selected)
        //{
        //    foreach (var category in Categories)
        //        category.IsSelected = (category == selected);
        //}

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
