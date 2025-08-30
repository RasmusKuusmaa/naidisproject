using naidisprojekt.Models;
using naidisprojekt.Service;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace naidisprojekt.ViewModels
{
    public class ProfileViewModel : INotifyPropertyChanged
    {
        private string welcomeText;
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

        //public async Task LoadUserAsync()
        //{
        //    int? userId = UserSession.Instance.UserId;

        //    if (userId.HasValue)
        //    {
        //        Dbservice db = new Dbservice();
        //        var user = await db.GetUserByIdAsync(userId.Value);
        //        WelcomeText = $"Welcome {user.Name}";
        //    }
        //    else
        //    {
        //        WelcomeText = "Welcome guest";
        //    }
        //}
    }
}
