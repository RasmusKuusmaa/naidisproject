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
            LogoutCommand = new Command(Logout);

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
        public Command LogoutCommand { get; }

        public async Task LoadUserAsync()
        {
            UserName = UserSession.CurrentUser.UserName;
            Email = UserSession.CurrentUser.Email;
        }
        private void Logout()
        {
            UserSession.CurrentUser = null;
            Application.Current.MainPage = new NavigationPage(new SplashPage())
            {
                BarTextColor = Color.FromArgb("#4F63AC")
            };
        }

    }
    
}
