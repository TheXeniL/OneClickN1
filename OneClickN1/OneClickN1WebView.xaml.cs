using System;
using Xamarin.Forms;

namespace OneClickN1
{
    public partial class OneClickN1WebView : ContentPage
    {
        public OneClickN1WebView(string newsID)
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            webView.Source = "https://newsn1.com/post_" + newsID;
            //webView.Source = newsID;
		}


		void backButtonClicked(object sender, EventArgs e)
		{
				this.Navigation.PopAsync(); // closes the in-app browser view.
		}
    }
}
