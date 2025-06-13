using naidisprojekt.Models;
using naidisprojekt.Service;
using System.Threading.Tasks;

namespace naidisprojekt.Pages;

public partial class SignInPage : ContentPage
{
    private readonly Dbservice dbservice = new Dbservice();
    public SignInPage()
	{
		InitializeComponent();
 	}

    private async void SignInBtn_Clicked(object sender, EventArgs e)
    {
        string username = StyledEntry.Text;
        string password = PasswordEntry.Text;

        int? userId = await dbservice.GetUserIdAsync(username, password);

        if (userId.HasValue)
        {
            UserSession.Instance.SetUserId(userId.Value);

            if (Application.Current?.Windows.Count > 0)
            {
                Application.Current.Windows[0].Page = new AppShell();
            }
        }
        else
        {
            await Application.Current.MainPage.DisplayAlert("Login Failed", "Invalid username or password", "OK");
        }
    }

}