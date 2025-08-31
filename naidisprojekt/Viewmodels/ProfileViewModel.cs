using naidisprojekt.Models;
using naidisprojekt.Pages;
using naidisprojekt.Service;
using naidisprojekt.Viewmodels;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace naidisprojekt.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {

        public Command NavigateToMyListingsCommand { get; }
        public Command NavigateToSettingsCommand { get; }

        public ProfileViewModel()
        {
            AddNewListingCommand = new Command(AddnewListing);
            NavigateToMyListingsCommand = new Command(NavigateToMyListings);
            NavigateToSettingsCommand = new Command(NavigateToSettings);
        }

        private async void NavigateToMyListings()
        {
            await Shell.Current.GoToAsync(nameof(MyListingsPage));
        }

        private async void NavigateToSettings()
        {
            await Shell.Current.GoToAsync(nameof(SettingsPage));
        }

        private string _userName;

        public string UserName
        {
            get { return _userName; }
            set { _userName = value; OnPropertyChanged(); }
        }
        private string _email;

        public string Email
        {
            get { return _email; }
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }





        public Command AddNewListingCommand { get; }
        private async void AddnewListing()
        {
            await Shell.Current.GoToAsync(nameof(AddnewlistingPage));
        }
        public async Task LoadUserAsync()
        {
            UserName = UserSession.CurrentUser.Name;
            Email = UserSession.CurrentUser.Email;
        }
    }
}
