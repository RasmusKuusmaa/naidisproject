using naidisprojekt.Models;
using naidisprojekt.Viewmodels;
using System.Collections.ObjectModel;
using System.Security.Cryptography.X509Certificates;

namespace naidisprojekt.Pages;

public partial class Homepage : ContentPage
{
	public ObservableCollection<Category> Categories { get; set; }
	public Homepage()
	{
		InitializeComponent();
		BindingContext = new HomePageViewModel();
	}

    private void CollectionView_SelectionChanged(object sender, SelectionChangedEventArgs e)
    {

    }
}