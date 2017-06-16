using System;
using Xamarin.Forms;

namespace OneClickN1
{
    public partial class OneClickN1Page : ContentPage
    {
        public static string tagsToSearch;

        public OneClickN1Page()
        {
            InitializeComponent();

            if(placeholderTags.Text == null){
                searchTagButton.IsEnabled = false;
                searchTagButton.BackgroundColor = Color.DarkBlue;
                NavigationPage.SetHasNavigationBar(this, false);
            }
        }

         public void SearchTagButtonPushed (object sender, EventArgs args){
            Navigation.PushAsync(new OneClickN1NewsPage());
        }

        public void PlaceholderChangedText (object sender, EventArgs args){
            if (placeholderTags.Text == ""){
				searchTagButton.IsEnabled = false;
				searchTagButton.BackgroundColor = Color.DarkBlue;
				searchTagButton.TextColor = Color.White;
            } else {
                searchTagButton.IsEnabled = true;
				searchTagButton.BackgroundColor = Color.Blue;
				searchTagButton.TextColor = Color.White;

            }
            tagsToSearch = placeholderTags.Text;
        }

        public void N1NewsButton(object sender, EventArgs args){
            Device.OpenUri(new Uri("https://newsn1.com/"));
        }

        public void TelegramButton (object sender, EventArgs args){
            Device.OpenUri(new Uri("https://telegram.me/OneClickNewsBot"));   
        }
    }
}
