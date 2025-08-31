using naidisprojekt.Pages;

namespace naidisprojekt;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		Routing.RegisterRoute(nameof(AddnewlistingPage), typeof(AddnewlistingPage));
		Routing.RegisterRoute(nameof(ListingPage), typeof(ListingPage));
        Routing.RegisterRoute(nameof(MyListingsPage), typeof(MyListingsPage));
        Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));

    }
}