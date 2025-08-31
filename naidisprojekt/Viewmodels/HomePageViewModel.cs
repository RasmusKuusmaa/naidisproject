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

        public ICommand SelectCategoryCommand { get; }

        private async Task LoadData()
        {
            var categories = await _apiService.GetAllCategories();
            Categories = new ObservableCollection<Category>(categories);
            Listings = new ObservableCollection<Listing>(await _apiService.GetAllListings());
        }

        private void OnCategorySelected(Category category)
        {
            SelectedCategory = category;
        }

        public HomePageViewModel()
        {
            SelectCategoryCommand = new Command<Category>(OnCategorySelected);
            _ = LoadData();
        }
    }

}
