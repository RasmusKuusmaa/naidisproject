using naidisprojekt.Models;
using naidisprojekt.Service;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace naidisprojekt.Viewmodels
{
    public class HomePageViewModel : BaseViewModel
    {
        private readonly ApiService _apiService = new ApiService();

        private ObservableCollection<Category> _categories;
        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged();
            }
        }

        private Category _selectedCategory;
        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set
            {
                if (_selectedCategory != value)
                {
                    if (_selectedCategory != null)
                        _selectedCategory.IsSelected = false;

                    _selectedCategory = value;

                    if (_selectedCategory != null)
                        _selectedCategory.IsSelected = true;

                    OnPropertyChanged();
                    OnPropertyChanged(nameof(Categories));
                    FilterListings();
                }
            }
        }
        private ObservableCollection<Listing> _listings;

        public ObservableCollection<Listing> Listings
        {
            get { return _listings; }
            set { _listings = value;
                OnPropertyChanged();
            }
        }
        private ObservableCollection<Listing> _allListings;
        private void FilterListings()
        {
            if (SelectedCategory == null)
                return;
            if (SelectedCategory.CategoryId == 3)
                Listings = _allListings;
            else
            {
                Listings = new ObservableCollection<Listing>( _allListings.Where(l => l.CategoryId == SelectedCategory.CategoryId));
            }
        }
        public ICommand SelectCategoryCommand { get; }

        private async Task LoadData()
        {
            var categories = await _apiService.GetAllCategories();
            Categories = new ObservableCollection<Category>(categories);
            _allListings = new ObservableCollection<Listing>(await _apiService.GetAllListings());
            Listings = _allListings;
        }

        private void OnCategorySelected(Category category)
        {
            SelectedCategory = category;
        }

        private async void OnListingSelected(Listing listing)
        {
            if (listing == null)
                return;

            await Shell.Current.GoToAsync(nameof(Pages.ListingPage), true, new Dictionary<string, object>
        {
            { "Listing", listing }
        });
        }
        public ICommand OpenListingCommand { get; }

        public HomePageViewModel()
        {
            SelectCategoryCommand = new Command<Category>(OnCategorySelected);
            _ = LoadData();
            OpenListingCommand = new Command<Listing>(OnListingSelected);

        }
        public async Task RefreshData()
        {
            await LoadData();
        }
    }

}
