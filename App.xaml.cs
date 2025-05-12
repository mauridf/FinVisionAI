using FinVisionAI.UI.Views;

namespace FinVisionAI
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new FinVisionAI.UI.Views.MainPage());
        }
    }
}
