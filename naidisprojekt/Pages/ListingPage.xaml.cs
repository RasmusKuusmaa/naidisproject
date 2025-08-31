using naidisprojekt.Models;

namespace naidisprojekt.Pages;

[QueryProperty(nameof(Listing), "Listing")]
public partial class ListingPage : ContentPage
{
    private Listing listing;
    public Listing Listing
    {
        get => listing;
        set
        {
            listing = value;
            BindingContext = listing;
        }
    }

    public ListingPage()
    {
        InitializeComponent();
    }
}