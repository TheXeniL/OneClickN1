using System;
using System.Net;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using OneClickN1.Model;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace OneClickN1
{
    public partial class OneClickN1NewsPage : ContentPage
    {
        private RequestJsonData parser = new RequestJsonData();
		public static string newsID;

		private ObservableCollection<News> news { get; set; }
        private string[] tags = new string[] { };
        private string searchTags;
        private int offsetNumber;
        private int globalNewsID = 0;
        private bool loadMoreNewsStatus = false;
        private DateTime dtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);

        public OneClickN1NewsPage()
        {
            news = new ObservableCollection<News>();
            InitializeComponent();

		/*
         * На устройстве Android приходится изменять некоторые параметры элементов графического интерфейса
         * для корректного отображения.
         */

			if (Device.OS == TargetPlatform.Android)
			{
                searchGrid.Padding = new Thickness(0,0,0,1);
                errorLabelHeight.Height = 45;
                overralHeight.Height = 450;
			}

			loadMoreNewsButton.IsVisible = false;
			NavigationPage.SetHasNavigationBar(this, false);
            newsList.IsPullToRefreshEnabled = true;
            newsList.ItemsSource = news;
			GetTaggedNews();
		}

		/*
         * Метод при помощи локального ID вычисляет какой элемент списка был нажат и переносит пользователя на другую страницу
         * где ID новости используется для перехода на сайт новости.
         */

		private async void ShowDetails(object sender, Xamarin.Forms.ItemTappedEventArgs e)
        {
            this.IsBusy = true;
			var index = (newsList.ItemsSource as ObservableCollection<News>).IndexOf(e.Item as News);

            IEnumerable<News> getDataFromCollection = news.Where(c => c.loacalId == index);

			foreach (var temp in getDataFromCollection)
            {
                newsID = temp.id;
            }

            this.IsBusy = false;
			newsList.SelectedItem = null;
			var newsPage = new OneClickN1WebView(newsID);
            await Navigation.PushAsync(newsPage);
        }


		private void OnTapGestureRecognizerTapped(object sender, EventArgs args)
		{
            this.Navigation.PopAsync();
		}

        /*
         * Данный метод позволяет на странице найденных новостей вводить другие теги для поиска новостей, 
         * не переходя на предыдущую страницу
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
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString()+"&fillpic=1");
					
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

		/*
         * Метод для сброса сообщения об ошибке при вводе неккоректных ключевых слов
         */
		
        private void placeHolderTextChanged(object sender, EventArgs args)
		{
				errorLabel.IsVisible = false;
				errorLabelHeight.Height = 55;
		}


		/*
         * Метод для заполнения таблицы новостями.
         */

		private async void GetTaggedNews()
        {
            if (!this.IsBusy)
            {

                try
                {
                    this.IsBusy = true;
                    offsetNumber = 0;
                    searchTags = OneClickN1Page.searchTags;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString()+"&fillpic=1");
                    FillNewsFromTags();
					newsList.EndRefresh();
                }

                finally
                {
                    this.IsBusy = false;
                    loadMoreNewsButton.IsVisible = true;
                }
            }
		}

		/*
         * Разделение введенных слов с помощью разделителя "+"
         */

		private void CreateTagsToSearch (string[] tagsToSearchFor)
        {
            for (int i = 0; i < tagsToSearchFor.Length;i++)
            {
                searchTags += tagsToSearchFor[i] + "+";
            }
        }


		/*
         * Метод для заполнения коллекции новостями, заполняются такие параметры как Id новости, тема, 
         * небольшая справка, картинка и т.д.
         */

		private void FillNewsFromTags()
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

		}

		/*
         * Метод для обновления таблицы новостей
         */

		private async void RefreshNews(object sender, EventArgs args)
        {
            if (!this.IsBusy)
            {
                try
                {
                    this.IsBusy = true;
                    globalNewsID = 0; // обнуляем ID новостей так как список новостей будет загружен с начала
                    offsetNumber = 0;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString()+"&fillpic=1");
                    news.Clear();
                    FillNewsFromTags();
                    newsList.EndRefresh();

                } finally {
                    this.IsBusy = false;
					errorLabelHeight.Height = 55;
				}
            }

		}

		/*
         * Метод для подгрузки следующих 15 новостей
         */

		private async void LoadItems(object sender, EventArgs args)
        {
            loadMoreNewsStatus = true;
            loadMoreNewsButton.IsEnabled = false;

            if (!this.IsBusy)
            {

                try
                {
                    this.IsBusy = true;
                    offsetNumber += 10;
                    await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags + "&offset=" + offsetNumber.ToString()+"&fillpic=1");
                    FillNewsFromTags();
                }
                finally
                {
                    this.IsBusy = false;
                }
            }

            loadMoreNewsButton.IsEnabled = true;
			loadMoreNewsStatus = false;
        }
    }
}