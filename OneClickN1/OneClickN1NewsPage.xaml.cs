using System;
using System.Net;
using System.Collections.Generic;
using Xamarin.Forms;
using System.Collections.ObjectModel;
using OneClickN1.Model;

namespace OneClickN1
{
    public partial class OneClickN1NewsPage : ContentPage
    {
        public RequestJsonData parser = new RequestJsonData();
        private ObservableCollection<News> news { get; set; }


        public OneClickN1NewsPage()
        {
            news = new ObservableCollection<News>();
            InitializeComponent();
            newsList.ItemsSource = news;
            GetTaggedNews();

        }

        public async void GetTaggedNews()
        {
            await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + "Macron");
            for (int i = 0; i < parser.jArray.Count; i++){
                news.Add(new News() { caption =WebUtility.HtmlDecode(parser.jArray[i]["caption"].ToString()) , overview=WebUtility.HtmlDecode(parser.jArray[i]["overview"].ToString()),imageURL="https://newsn1.com/img/knews/"+parser.jArray[i]["picture"].ToString()});
            }
           
        }

    }
}
