using naidisprojekt.Models;

namespace naidisprojekt.Pages;

[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductPage : ContentPage
{
    private int productId;
    public int ProductId
    {
        get => productId;
        set
        {
            productId = value;
            LoadProduct(productId);
        }
    }

    public ProductPage()
    {
        InitializeComponent();
    }

    private void LoadProduct(int id)
    {
        var products = new List<Product>
        {
            new Product {Id = 1, Name = "Black Simple Lamp", ImageSource = "minimallamp.png", Price=12, Description="Lorem ipsum dolor sit amet, consectetur adipiscing elit. Fusce eget sagittis velit. Integer mollis efficitur lorem vitae varius."},
            new Product {Id = 2, Name = "Minimal Stand", ImageSource="minimalstand.png", Price=25},
            new Product {Id = 3, Name = "Coffee Chair", ImageSource="coffechair.png", Price=20},
            new Product {Id = 4, Name = "Simple Desk", ImageSource = "minimaldesk.png", Price=50},
            new Product {Id = 5, Name = "Coffee Table", ImageSource = "coffetable.png", Price=50}
        };

        var product = products.FirstOrDefault(p => p.Id == id);
        if (product != null)
        {
            ProductImage.Source = product.ImageSource;
            ProductName.Text = product.Name;
            ProductPrice.Text = $"${product.Price:F2}";
            ProductDescription.Text = product.Description ?? "No description available.";
        }
        else
        {
            ProductName.Text = "Product not found";
            ProductPrice.Text = "";
            ProductDescription.Text = "";
            ProductImage.Source = null;
        }
    }

    private void BackButton_Clicked(object sender, EventArgs e)
    {
        Application.Current.MainPage = new AppShell();
    }
   
}
