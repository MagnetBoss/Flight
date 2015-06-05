using FlyLight.View;
using FlyLight.ViewModel;
using Xamarin.Forms;

namespace FlyLight
{
    public class App : Application
    {
        public App()
        {
            _locator = new ViewModelLocator();
            MainPage = GetMainPage();
        }

        public Page GetMainPage()
        {
            return new MainPage();
        }

        private readonly ViewModelLocator _locator;

        public ViewModelLocator Locator
        {
            get { return _locator; }
        }
    }
}
