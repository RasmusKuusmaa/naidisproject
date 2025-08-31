using naidisprojekt.Models;
using naidisprojekt.Service;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace naidisprojekt.Viewmodels
{
    public class AddNewListingPageViewmodel : BaseViewModel
    {
        private ObservableCollection<Category> _categories;

        public ObservableCollection<Category> Categories
        {
            get { return _categories; }
            set { _categories = value;
                OnPropertyChanged();
            }
        }
        private Category _selectedCategory;

        public Category SelectedCategory
        {
            get { return _selectedCategory; }
            set { _selectedCategory = value;
                OnPropertyChanged();
            }
        }

        private string _name;

        public string Name
        {
            get { return _name; }
            set { _name = value;
                OnPropertyChanged();
            }
        }
        public string PriceText
        {
            get => Price.ToString();
            set
            {
                if (decimal.TryParse(value, out var parsed))
                    Price = parsed;
                OnPropertyChanged();
            }
        }
        private decimal _price;

        public decimal Price
        {
            get { return _price; }
            set { _price = value;
                OnPropertyChanged();
            }
        }
        private string _description;

        public string Desctription
        {
            get { return _description; }
            set { _description = value;
                OnPropertyChanged();
            }
        }
        private ImageSource _previewImage;
        public ImageSource PreviewImage
        {
            get => _previewImage;
            set
            {
                _previewImage = value;
                OnPropertyChanged();
            }
        }
        private byte[] _imageBytes;

        public byte[] ImageBytes
        {
            get { return _imageBytes; }
            set { _imageBytes = value;
                OnPropertyChanged();
            }
        }
        public Command PickImageCommand { get; }

        private async Task PickImage()
        {
            try
            {
                var file = await FilePicker.Default.PickAsync(new PickOptions
                {
                    PickerTitle = "Pick an image",
                    FileTypes = FilePickerFileType.Images
                });

                if (file != null)
                {
                    using var stream = await file.OpenReadAsync();
                    using var ms = new MemoryStream();
                    await stream.CopyToAsync(ms);
                    ImageBytes = ms.ToArray();
                    PreviewImage = ImageSource.FromStream(() => new MemoryStream(ImageBytes));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error picking image: {ex.Message}");
            }
        }
        public Command SubmitCommand { get; }
        private async Task Submit()
        {
            Listing listing = new Listing
            {
                CategoryId = SelectedCategory.CategoryId,
                ListingName = Name,
                Price = Price,
                ListingDescription = Desctription,
                UserId = UserSession.CurrentUser.UserId,
                ListingImage = ImageBytes
            };

            var response = await _apiService.AddnewListing(listing);
            if (response)
                await Shell.Current.DisplayAlert("Added", "Listing has been added", "OK");
            else
                await Shell.Current.DisplayAlert("Failed", "Listing has not been added", "OK");
        }
        private readonly ApiService _apiService = new ApiService();
        public AddNewListingPageViewmodel()
        {
            SubmitCommand = new Command(async () => await Submit());
            PickImageCommand = new Command(async () => await PickImage());
            _ = LoadAllCategories();
        }
        private async Task LoadAllCategories()
        {
            Categories = new ObservableCollection<Category>(await _apiService.GetAllCategories());
        }
        
    }
}
