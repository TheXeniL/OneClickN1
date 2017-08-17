using NameSpace.iOS.Renderers;
using OneClickN1;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineLabelRendererIOS))]
namespace NameSpace.iOS.Renderers
{
    public class MultiLineLabelRendererIOS : LabelRenderer
    {
		//protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		//{
		//    base.OnElementChanged(e);
		//    if (Control != null)
		//    {
		//        Control.LineBreakMode = UILineBreakMode.TailTruncation;
		//        Control.Lines = 2;
		//    }
		//}

		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			var baseLabel = (MultiLineLabel)this.Element;
            Control.LineBreakMode = UILineBreakMode.TailTruncation;
			Control.Lines = baseLabel.Lines;
		}
    }
}