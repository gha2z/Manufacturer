using IntrManHyridApp.UI.View.Products;

namespace IntrManHyridApp.UI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
            
        }
    }
}
