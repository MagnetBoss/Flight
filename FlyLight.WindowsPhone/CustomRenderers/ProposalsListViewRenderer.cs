using FlyLight.View.CustomControls;
using FlyLight.WindowsPhone.CustomRenderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.WinRT;

[assembly: ExportRenderer(typeof(ProposalsListView), typeof(ProposalsListViewRenderer))]

namespace FlyLight.WindowsPhone.CustomRenderers
{
    public class ProposalsListViewRenderer : ListViewRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<ListView> e)
        {
            base.OnElementChanged(e);
        }

    }
}
