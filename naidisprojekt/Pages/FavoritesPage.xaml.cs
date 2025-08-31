using naidisprojekt.Viewmodels;

namespace naidisprojekt.Pages;

public partial class FavoritesPage : ContentPage
{
	public FavoritesPage()
	{
		InitializeComponent();
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is FavoritesViewModel vm)
            await vm.RefreshData();
    }
}