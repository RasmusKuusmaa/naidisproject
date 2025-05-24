using Microsoft.Maui;
using Microsoft.Maui.Controls;
using naidisprojekt.Pages;

namespace naidisprojekt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new NavigationPage(new SplashPage()));
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
