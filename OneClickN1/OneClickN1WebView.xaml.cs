using System;
using System.Collections.Generic;
using System.Diagnostics;

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
            Debug.WriteLine("https://newsn1.com/post_" + newsID);
		}


		void backButtonClicked(object sender, EventArgs e)
		{
				this.Navigation.PopAsync(); // closes the in-app browser view.
		}
    }
}
