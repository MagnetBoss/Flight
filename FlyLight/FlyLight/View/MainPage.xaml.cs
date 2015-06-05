
using Xamarin.Forms;

namespace FlyLight.View
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = ((App)Application.Current).Locator.Main;
        }

    }
}
