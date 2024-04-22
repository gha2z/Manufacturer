namespace IntrManAppUranium.UI.View.Product;

public partial class ProductListPage : UraniumUI.Pages.UraniumContentPage
{
    public ProductListPage(ProductViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected async override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await ((ProductViewModel)BindingContext).LoadProducts();

    }
}
