using System.Linq.Expressions;

namespace IntrManHyridApp.UI.ViewModel;

public partial class ProductViewModel : BaseViewModel
{

    public ObservableCollection<ProductResponse> Products { get; } = new();
    public ProductResponse? SelectedProduct { get; set; }
    ProductService productService;

    public ProductViewModel(ProductService productService)
    {
        Title = "Products";
        this.productService = productService;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    public async Task LoadProducts()
    {
        if(IsBusy)
            return;

        try
        {
            IsBusy = true;

            if (Products.Count > 0) Products.Clear();

            var products = await productService.GetProductsAsync();
            foreach (var product in products)
            {
                Products.Add(product);
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
            await Shell.Current.DisplayAlert("Result", $"Count:{Products.Count}", "OK");
        }
    }
}
