
namespace IntrManHyridApp.UI.View.Products;

public partial class NewPage1 : ContentPage
{
	
	public NewPage1(ProductViewModel viewModel)
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