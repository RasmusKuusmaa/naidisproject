using naidisprojekt.Models;
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
        public ObservableCollection<Category> Categories { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        
        public ICommand SelectCategoryCommand { get; }

        public HomePageViewModel() {
            Products = new ObservableCollection<Product>
            {
                new Product {Id = 1, Name = "Black Simple Lamp", ImageSource = "minimallamp.png",
                 Price=12},

                new Product {Id = 2,
                Name = "Minimal Stand", ImageSource="minimalstand.png",Price=25},
                new Product
                {
                    Id = 3,
                    Name = "Coffee Chair",
                    ImageSource="coffechair.png",
                    Price=20
                },
                new Product
                {
                    Id = 4,
                    Name = "Simple Desk",
                    ImageSource = "minimaldesk.png",
                    Price=50
                },
                new Product
                {
                    Id = 5,
                    Name = "Coffee Table",
                    ImageSource = "coffetable.png",
                    Price =50
                }
            };
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
