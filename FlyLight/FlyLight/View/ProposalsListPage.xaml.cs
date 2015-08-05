
using Xamarin.Forms;

namespace FlyLight.View
{
    public partial class ProposalsListPage : ContentPage
    {
        public ProposalsListPage()
        {
            InitializeComponent();
            BindingContext = ((App)Application.Current).Locator.ProposalsList;
        }
    }
}
