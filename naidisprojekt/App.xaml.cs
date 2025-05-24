using naidisprojekt.Pages;
namespace naidisprojekt
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SplashPage());
        }

        public void NavigateToMainApp()
        {
            MainPage = new AppShell();
        }

        
    }
}