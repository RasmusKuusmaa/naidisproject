using naidisprojekt.Models;

namespace naidisprojekt.Pages;

public partial class SettingsPage : ContentPage
{
    public SettingsPage()
    {
        InitializeComponent();
        Nametxt.Text = UserSession.CurrentUser.UserName;
        Emailtxt.Text = UserSession.CurrentUser.Email;
    }
}