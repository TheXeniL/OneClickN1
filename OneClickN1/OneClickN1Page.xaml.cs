using System;
using Xamarin.Forms;

namespace OneClickN1
{
    public partial class OneClickN1Page : ContentPage
    {
        private string tagsToSearch;
        public static String[] tags = new String[] {}; 

        public RequestJsonData parser = new RequestJsonData();

        public OneClickN1Page()
        {
            InitializeComponent();

            if(placeholderTags.Text == null){
                searchTagButton.IsEnabled = false;
                searchTagButton.BackgroundColor = Color.DarkBlue;
                NavigationPage.SetHasNavigationBar(this, false);
            }
        }

        public async void SearchTagButtonPushed (object sender, EventArgs args)
        {
			//await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + tags[0]+"+"+tags[1]);
			//testLabel.Text = (string)parser.jArray[0]["caption"]+" "+(string)parser.jArray[1]["caption"]+ " " + (string)parser.jArray[2]["caption"];
            var newsPage = new OneClickN1NewsPage();
            await Navigation.PushAsync(newsPage);

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
            tags = tagsToSearch.Split(' ');
        }

        public void N1NewsButton(object sender, EventArgs args){
            Device.OpenUri(new Uri("https://newsn1.com/"));
        }

        public void TelegramButton (object sender, EventArgs args){
            Device.OpenUri(new Uri("https://telegram.me/OneClickNewsBot"));   
        }
    }
}
