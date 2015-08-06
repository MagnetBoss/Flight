using FlyLight.ViewModel.Messaging;
using GalaSoft.MvvmLight.Messaging;
using Xamarin.Forms;

namespace FlyLight.View
{
    public class MainNavigationPage : NavigationPage
    {
        public MainNavigationPage(Page root) : base(root)
        {
            Messenger.Default.Register<ShowProposalsListMessage>(this, async message =>
            {
                await Navigation.PushAsync(new ProposalsListPage());
            });
        }
    }
}
