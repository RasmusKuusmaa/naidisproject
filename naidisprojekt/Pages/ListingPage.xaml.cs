using naidisprojekt.Models;
using naidisprojekt.Service;

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
    private bool isFavorite = false;
    public ListingPage()
    {
        InitializeComponent();
    }
    ApiService apiservice = new ApiService();
    private void FavoriteButton_Clicked(object sender, EventArgs e)
    {
        isFavorite = !isFavorite;
        FavoriteButton.Source = isFavorite
            ? "favoritetselected.png"
            : "favoritesiconnotselected.png";

        apiservice.AddListingToFavorites(UserSession.CurrentUser.UserId, Listing.ListingId);
    }
}