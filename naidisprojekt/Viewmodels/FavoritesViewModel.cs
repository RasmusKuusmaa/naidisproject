using naidisprojekt.Models;
using naidisprojekt.Service;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using Microsoft.Maui.Controls;

namespace naidisprojekt.Viewmodels
{
    public partial class FavoritesViewModel : BaseViewModel
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

        public FavoritesViewModel()
        {
            RemoveCommand = new Command<Listing>(RemoveListing);
            _ = LoadListings();
        }

        private async Task LoadListings()
        {
            Listings = new ObservableCollection<Listing>(
                await apiService.GetUserFavoriteListings(UserSession.CurrentUser.UserId)
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
