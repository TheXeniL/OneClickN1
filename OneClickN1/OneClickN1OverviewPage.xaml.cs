using System.Diagnostics;
using Xamarin.Forms;
using System.Net;

namespace OneClickN1
{
    public partial class OneClickN1OverviewPage : ContentPage
    {
        public RequestJsonData parser = new RequestJsonData();


        public OneClickN1OverviewPage()
        {
			InitializeComponent();
            BindingContext = this;
			GetTaggedNews();
        }

        private async void GetTaggedNews()
        {
  //          Debug.WriteLine(OneClickN1NewsPage.newsId);
  //          await parser.MakeGetRequest("https://newsn1.com/?mode=news&id="+OneClickN1NewsPage.newsId);
  //          if (parser.JsonParseSucces == true)
  //          {
  //              newsCaption.Text = WebUtility.HtmlDecode(parser.jArray[0]["caption"].ToString());
  //              newsText.FormattedText = WebUtility.HtmlDecode(parser.jArray[0]["text"].ToString());
  //              newsImage.Source =WebUtility.HtmlDecode("https://newsn1.com/img/knews/" + parser.jArray[0]["picture"]);
  //          }
  //          else
  //          {

  //          }
		}

    }
}
