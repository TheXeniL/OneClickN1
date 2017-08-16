using System;
using System.Net;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using OneClickN1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace OneClickN1
{
    public partial class OneClickN1NewsPage : ContentPage
    {
        public RequestJsonData parser = new RequestJsonData();
		public static string newsID;

		private ObservableCollection<News> news { get; set; }
        private string[] tags = new string[] { };
        private string searchTags;
        private int offsetNumber;
        private int globalNewsID = 0;
        private bool loadMoreNewsStatus = false;
        DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public OneClickN1NewsPage()
        {
            news = new ObservableCollection<News>();
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);
            newsList.IsPullToRefreshEnabled = true;
            newsList.ItemsSource = news;
            GetTaggedNews();

		}

        private async void ShowDetails(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
			var index = (newsList.ItemsSource as ObservableCollection<News>).IndexOf(e.Item as News);
            Debug.WriteLine(index);

            IEnumerable<News> getDataFromCollection = news.Where(c => c.loacalId == index);

			foreach (var temp in getDataFromCollection)
            {
                newsID = temp.id;
            }

            var newsPage = new OneClickN1WebView(newsID);
            await Navigation.PushAsync(newsPage);
        }

		public void OnTapGestureRecognizerTapped(object sender, EventArgs args)
		{
            this.Navigation.PopAsync();
		}


        /*
         * Данный метод позволяет на странице найденных новостей вводить другие теги для поиска новостей, не переходя на предыдущую страницу
         */

        private async void SearchAnotherTags(object sender, EventArgs args)
        {
			if (!this.IsBusy)
			{
				try
				{
                    this.IsBusy = true;
                    globalNewsID = 0;
                    searchTags = null;
                    tags = newTagsToSeach.Text.Split(' ');
                    CreateTagsToSearch(tags);
                    offsetNumber = 0;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString());
					
                    if (parser.JsonParseSucces == true)
					{
						news.Clear();
						FillNewsFromTags();
						newTagsToSeach.Text = null;
                        errorLabelHeight.Height = 55;
					}
					else
					{
						errorLabel.IsVisible = true;
                        errorLabelHeight.Height = 105;
					}

                }
                finally
                {
                    this.IsBusy = false;
                }
            }
		}

		public void placeHolderTextChanged(object sender, EventArgs args)
		{
            if (newTagsToSeach.Text == "")
			{
				errorLabel.IsVisible = false;
				errorLabelHeight.Height = 55;
			}

			else
			{
                errorLabel.IsVisible = false;
				errorLabelHeight.Height = 55;
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
					newsList.EndRefresh();
                }

                finally
                {
                    this.IsBusy = false;
                }
            }
		}

        private void CreateTagsToSearch (string[] tagsToSearchFor)
        {
            for (int i = 0; i < tagsToSearchFor.Length;i++)
            {
                searchTags += tagsToSearchFor[i] + "+";
            }
        }

        private void  FillNewsFromTags()
        {
           
                for (int i = 0; i < parser.jArray.Count; i++)
                {
                    news.Add(new News()
                    {
                        loacalId = globalNewsID,
                        caption = WebUtility.HtmlDecode(parser.jArray[i]["caption"].ToString()),
                        id = parser.jArray[i]["id"].ToString(),
                        overview = WebUtility.HtmlDecode(parser.jArray[i]["overview"].ToString()),
                        imageURL = "https://newsn1.com/img/knews/" + parser.jArray[i]["picture"],
                        imageSource = "https://newsn1.com/img/knews/" + parser.jArray[i]["srcicon"],
                        newsSource = WebUtility.HtmlDecode(parser.jArray[i]["srcname"].ToString()),
                        newsTime = dtDateTime.AddSeconds((double)parser.jArray[i]["kntime"]).ToLocalTime().ToString("HH:mm  dd, MMMM yyyy")
                    });
                    globalNewsID++;
                }
				changeImage();
		}


        private void changeImage(){
            
			for (int i = 0; i < parser.jArray.Count; i++)
			{
				var item = news.FirstOrDefault(p => p.loacalId == i);
				Debug.WriteLine(item.loacalId);

				if (item.imageURL == "https://newsn1.com/img/knews/")
				{
					Debug.WriteLine("Empty image");
				}
				else
				{
					Debug.WriteLine("Image filled");

				}
			}
        }

        private async void RefreshNews(object sender, EventArgs args)
        {
            if (!this.IsBusy)
            {
                try
                {
                    this.IsBusy = true;
                    globalNewsID = 0;
                    offsetNumber = 0;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString());
                    news.Clear();
                    FillNewsFromTags();
                    newsList.EndRefresh();
                } finally {
                    this.IsBusy = false;
					errorLabelHeight.Height = 55;
				}
            }

		}

        private async void LoadMoreNews(object sender,ItemVisibilityEventArgs e) 
        {
			errorLabelHeight.Height = 55;

			if (loadMoreNewsStatus == false)
            {
                if (e.Item == news[news.Count - 1])
                {
                    await LoadItems();
                }
            }
        }

        private async Task LoadItems()
        {
            loadMoreNewsStatus = true;
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
            await Task.Delay(500);
            loadMoreNewsStatus = false;
        }
    }
}