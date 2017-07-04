using System;
using System.Net;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using OneClickN1.Model;
using System.Threading.Tasks;

namespace OneClickN1
{
    public partial class OneClickN1NewsPage : ContentPage
    {
        public RequestJsonData parser = new RequestJsonData();
        private ObservableCollection<News> news { get; set; }
        private string[] tags = new string[] { };
        private string searchTags;
        private int offsetNumber;


        public OneClickN1NewsPage()
        {
            news = new ObservableCollection<News>();
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            newsList.IsPullToRefreshEnabled = true;
            newsList.ItemsSource = news;
            GetTaggedNews();

        }

        private async void ShowDetails(object sender, ItemTappedEventArgs args){
            //var newsPage = new OneClickN1NewsOverview();
            //await Navigation.PushAsync(new OneClickN1NewsOverview());
        }

        private async void SearchAnotherTags(object sender, EventArgs args)
        {
			if (!this.IsBusy)
			{

				try
				{
                    this.IsBusy = true;

                    searchTags = null;
                    tags = newTagsToSeach.Text.Split(' ');
                    CreateTagsToSearch(tags);
                    offsetNumber = 0;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString());
                    news.Clear();
                    FillNewsFromTags();
                    newTagsToSeach.Text = null;
                }
                finally
                {
                    this.IsBusy = false;
                }
            }
		}

        private async void GetTaggedNews()
        {
            if (!this.IsBusy)
            {

                try
                {
                    this.IsBusy = true;
                    offsetNumber = 0;
                    searchTags = OneClickN1Page.searchTags;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString());
                    FillNewsFromTags();
                }finally{
                    this.IsBusy = false;
                }
            }
        
		}

        private void CreateTagsToSearch (string[] tagsToSearchFor){
            for (int i = 0; i < tagsToSearchFor.Length;i++){
                searchTags += tagsToSearchFor[i] + "+";
            }
        }

        private void FillNewsFromTags()
        {
            for (int i = 0; i < parser.jArray.Count; i++)
            {
                news.Add(new News()
                {
                    caption = WebUtility.HtmlDecode(parser.jArray[i]["caption"].ToString()),
                    id = parser.jArray[i]["id"].ToString(),
                    overview = WebUtility.HtmlDecode(parser.jArray[i]["overview"].ToString()),
                    imageURL = "https://newsn1.com/img/knews/" + parser.jArray[i]["picture"]

                });
            }
        }



        private async void RefreshNews(object sender, EventArgs args){
            if (!this.IsBusy)
            {

                try
                {
                    this.IsBusy = true;
                    offsetNumber = 0;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString());
                    news.Clear();
                    FillNewsFromTags();
                    newsList.EndRefresh();
                } finally {
                    this.IsBusy = false;
                }
            }

		}

        private async void LoadMoreNews(object sender,ItemVisibilityEventArgs e) {
            if (e.Item == news[news.Count-1])
			{
				await LoadItems();
			}

        }

        private async Task LoadItems()
        {
            if (!this.IsBusy)
            {

                try
                {
                    this.IsBusy = true;

                    offsetNumber += 10;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString());
                    FillNewsFromTags();
                }
                finally
                {
                    this.IsBusy = false;
                }
            }
        }
       



    }
}