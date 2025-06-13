using Microsoft.Maui;
using Microsoft.Maui.Controls;
using naidisprojekt.Pages;
using Microsoft.Maui.Handlers; 

namespace naidisprojekt
{
    public partial class App : Application
    {
        public App()
        {
            #if ANDROID
                    EntryHandler.Mapper.AppendToMapping("NoUnderline", (handler, view) =>
                    {
                        handler.PlatformView.Background = null;
                    });
            #endif
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {

            return new Window(new NavigationPage(new SplashPage())
            {
                BarTextColor = Color.FromArgb("#4F63AC")


            });
           // return new Window(new AppShell());
        }

        public void NavigateToMainApp()
        {
            if (Application.Current?.Windows.Count > 0)
            {
                Current.Windows[0].Page = new AppShell(); 
            }
        }
    }
}
