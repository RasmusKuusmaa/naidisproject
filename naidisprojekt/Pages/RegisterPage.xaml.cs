using naidisprojekt.Service;

namespace naidisprojekt.Pages;

public partial class RegisterPage : ContentPage
{
    
    private readonly ApiService apiService = new ApiService();
    public RegisterPage()
	{
		InitializeComponent();

	}

    private async void RegisterButton_Clicked(object sender, EventArgs e)
    {

        var res = await apiService.RegisterAnUser(StyledEntry.Text, Emailtxtbox.Text, PasswordEntry.Text);
        if (res)
        {

            await DisplayAlert("Registered", "User has been registered. Please log in.", "OK");
            await Navigation.PushAsync(new SignInPage());
        }
        else
        {
            await DisplayAlert("Registered", "User has not been registered. something went wrong", "OK");

        }


    }
}