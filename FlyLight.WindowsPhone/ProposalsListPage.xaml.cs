using Windows.UI.Xaml.Navigation;
using Cirrious.MvvmCross.WindowsCommon.Views;
using FlyLight.ViewModel;
using FlyLight.WindowsPhone.Common;


namespace FlyLight.WindowsPhone
{
    public sealed partial class ProposalsListPage : MvxWindowsPage
    {
        public ProposalsListPage()
        {
            InitializeComponent();
            _navigationHelper = new NavigationHelper(this);
        }

        public new ProposalsListViewModel ViewModel
        {
            get { return (ProposalsListViewModel)base.ViewModel; }
            set { base.ViewModel = value; }
        }


        private readonly NavigationHelper _navigationHelper;
        public NavigationHelper NavigationHelper
        {
            get { return _navigationHelper; }
        }

        private readonly ObservableDictionary _defaultViewModel = new ObservableDictionary();
        public ObservableDictionary DefaultViewModel
        {
            get { return _defaultViewModel; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            _navigationHelper.OnNavigatedTo(e);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _navigationHelper.OnNavigatedFrom(e);
        }
    }
}
