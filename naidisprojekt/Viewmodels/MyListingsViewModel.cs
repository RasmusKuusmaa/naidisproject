using naidisprojekt.Models;
using naidisprojekt.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace naidisprojekt.Viewmodels
{
    internal partial class MyListingsViewModel : BaseViewModel
    {
        private ObservableCollection<Listing> _listings;
        public ObservableCollection<Listing> Listings
        {
            get => _listings;
            set
            {
                _listings = value;
                OnPropertyChanged();
            }
        }

        public ICommand RemoveCommand { get; }

        private readonly ApiService apiService = new ApiService();

        public MyListingsViewModel()
        {
            RemoveCommand = new Command<Listing>(RemoveListing);
            _ = LoadListings();
        }

        private async Task LoadListings()
        {
            Listings = new ObservableCollection<Listing>(
                await apiService.GetUserListings(UserSession.CurrentUser.UserId)
            );
        }

        private void RemoveListing(Listing listing)
        {
            if (listing == null)
                return;

            if (Listings.Contains(listing))
                Listings.Remove(listing);


        }
    }
}
