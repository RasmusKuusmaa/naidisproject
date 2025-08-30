using naidisprojekt.Models;
using naidisprojekt.Service;
using System.Threading.Tasks;

namespace naidisprojekt.Pages;

public partial class SignInPage : ContentPage
{
    private readonly ApiService apiService = new ApiService();
    public SignInPage()
	{
		InitializeComponent();
 	}

    private async void SignInBtn_Clicked(object sender, EventArgs e)
    {
        string username = StyledEntry.Text;
        string password = PasswordEntry.Text;

        var response = await apiService.Login(password, password);
        if (response == true)
        {

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