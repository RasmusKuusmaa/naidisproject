using naidisprojekt.Models;
using naidisprojekt.Pages;
using naidisprojekt.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace naidisprojekt.Viewmodels
{
    public class HomePageViewModel : INotifyPropertyChanged
    {
        public ICommand SelectProductCommand { get; }
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        private readonly Dbservice dbservice = new Dbservice();

        public ICommand SelectCategoryCommand { get; }

        public HomePageViewModel() {
            Products = new ObservableCollection<Product>();

            LoadProductsAsync();

            Categories = new ObservableCollection<Category>
            {
                        new Category { Id = 1, imageSource = "star.png"},
                        new Category { Id = 2, imageSource = "chair.png" },
                        new Category {Id = 3, imageSource = "table.png"},
                        new Category {Id = 4, imageSource = "armchair.png"},
                        new Category {Id = 5, imageSource = "bed.png"},
                        new Category { Id = 6, imageSource = "lamp.png"}

            };
            SelectCategoryCommand = new Command<Category>(category => SelectedCategory = category);
            SelectedCategory = Categories[0];


            SelectProductCommand = new Command<Product>(OnProductSelected);
        }
        private async void LoadProductsAsync()
        {
            var productsFromDb = await dbservice.GetAllProductsAsync();
            Products.Clear();
            foreach (var product in productsFromDb)
                Products.Add(product);
        }
        private Category selectedCategory;
        public Category SelectedCategory
        {
            get => selectedCategory;
            set
            {
                if (selectedCategory != value)
                {
                    selectedCategory = value;
                    OnPropertyChanged(nameof(SelectedCategory));
                    UpdateSelection(value);
                }
            }
        }
        private async void OnProductSelected(Product selectedProduct)
        {
            if (selectedProduct == null)
                return;

            var productPage = new ProductPage();
            productPage.ProductId = selectedProduct.Id;
            Application.Current.MainPage = productPage;
        }

        private void UpdateSelection(Category selected)
        {
            foreach (var category in Categories)
            {
                category.IsSelected = (category == selected);
            }
        }
        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
