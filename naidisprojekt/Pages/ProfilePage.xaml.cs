using naidisprojekt.ViewModels;

namespace naidisprojekt.Pages;

public partial class ProfilePage : ContentPage
{
    private readonly ProfileViewModel viewModel;

    public ProfilePage()
    {
        InitializeComponent();
        viewModel = new ProfileViewModel();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await viewModel.LoadUserAsync();
    }
}
