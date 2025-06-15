using naidisprojekt.Service;

namespace naidisprojekt.Pages;

public partial class RegisterPage : ContentPage
{
    
    private readonly Dbservice dbservice = new Dbservice();
    public RegisterPage()
	{
		InitializeComponent();

	}

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {

        await dbservice.RegisterUserAsync(Emailtxtbox.Text,StyledEntry.Text, PasswordEntry.Text);
        await DisplayAlert("Registered", "User has been registered. Please log in.", "OK");

        await Navigation.PushAsync(new SignInPage());


    }
}