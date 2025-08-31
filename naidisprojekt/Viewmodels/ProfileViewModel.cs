using naidisprojekt.Models;
using naidisprojekt.Pages;
using System.Threading.Tasks;
using Microsoft.Maui.Controls;
using naidisprojekt.Viewmodels;

namespace naidisprojekt.ViewModels
{
    public class ProfileViewModel : BaseViewModel
    {
        private string _userName;
        public string UserName
        {
            get => _userName;
            set { _userName = value; OnPropertyChanged(); }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set { _email = value; OnPropertyChanged(); }
        }

        public Command AddNewListingCommand { get; }
        public Command NavigateToMyListingsCommand { get; }
        public Command NavigateToSettingsCommand { get; }
        public Command LogoutCommand { get; }

        public ProfileViewModel()
        {
            AddNewListingCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(AddnewlistingPage)));
            NavigateToMyListingsCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(MyListingsPage)));
            NavigateToSettingsCommand = new Command(async () => await Shell.Current.GoToAsync(nameof(SettingsPage)));
            LogoutCommand = new Command(Logout);
        }

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
