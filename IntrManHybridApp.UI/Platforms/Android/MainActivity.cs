using Android.App;
using Android.Content.PM;
using Android.OS;
using Microsoft.Maui.ApplicationModel;
using Microsoft.Maui.Controls;

namespace IntrManHybridApp.UI
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState) 
        { 
            base.OnCreate(savedInstanceState); RequestedOrientation = ScreenOrientation.Landscape; 
        }
        //protected override void OnResume() 
        //{ 
        //    base.OnResume(); 
        //    DeviceDisplay.MainDisplayInfoChanged += Current_MainDisplayInfoChanged; 
        //}
        //protected override void OnPause() 
        //{ 
        //    base.OnPause(); 
        //    DeviceDisplay.MainDisplayInfoChanged -= Current_MainDisplayInfoChanged; 
        //}
        //private void Current_MainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e) 
        //{ 
        //    if (e.DisplayInfo.Orientation == DisplayOrientation.Landscape) 
        //    { 
        //        NavigationPage.SetHasNavigationBar((Page)sender, false); 
        //    } 
        //}
    }


}
