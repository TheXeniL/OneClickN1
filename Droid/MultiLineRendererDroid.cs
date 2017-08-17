using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using MyNamescpace;
using MyNamescpace.Droid;
using OneClickN1;

[assembly: ExportRenderer(typeof(MultiLineLabel), typeof(MultiLineLabelRendererDroid))]
namespace MyNamescpace.Droid
{
	public class MultiLineLabelRendererDroid : LabelRenderer
	{
		protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
		{
			base.OnElementChanged(e);

			var baseLabel = (MultiLineLabel)this.Element;
			Control.SetLines(baseLabel.Lines);
		}
	}
}