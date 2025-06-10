using naidisprojekt.Pages;

namespace naidisprojekt;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
        Routing.RegisterRoute("productpage", typeof(ProductPage));
    }
}