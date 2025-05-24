using System.Threading.Tasks;

namespace naidisprojekt.Pages;

public partial class SignInPage : ContentPage
{
	public SignInPage()
	{
		InitializeComponent();
	}

    private async void SignInBtn_Clicked(object sender, EventArgs e)
    {
		Application.Current.MainPage = new AppShell();
    }
}