using System.Threading.Tasks;

namespace naidisprojekt.Pages;

public partial class SplashPage : ContentPage
{
	public SplashPage()
	{
		InitializeComponent();
        NavigationPage.SetHasNavigationBar(this, false);

    }

    private async void signinbtn_Clicked(object sender, EventArgs e)
    {
        await Navigation.PushAsync(new SignInPage());
    }

    private void SignUpBtn_Clicked(object sender, EventArgs e)
    {

    }
}