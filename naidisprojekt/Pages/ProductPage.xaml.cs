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
            new Product {Id = 1, Name = "Black Simple Lamp", ImageSource = "minimallamp.png", Price=12},
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
        }
    }
}