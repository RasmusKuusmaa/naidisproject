using naidisprojekt.Models;
using naidisprojekt.Pages;
using naidisprojekt.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace naidisprojekt.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string welcomeText;

        public ProfileViewModel()
        {
            AddNewListingCommand = new Command(AddnewListing);
        }

        public string WelcomeText
        {
            get => welcomeText;
            set
            {
                welcomeText = value;
                OnPropertyChanged();
            }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged([CallerMemberName] string name = null)
            => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        
        public Command AddNewListingCommand { get; }
        private async void AddnewListing()
        {
            await Shell.Current.GoToAsync(nameof(AddnewlistingPage));
        }
        public async Task LoadUserAsync()
        {
            welcomeText = UserSession.CurrentUser.Name;
        }
    }
}
