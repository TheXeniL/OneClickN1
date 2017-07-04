﻿using System;
using Xamarin.Forms;
using System.ComponentModel;
using System.Collections.Generic;

namespace OneClickN1
{
    public partial class OneClickN1Page : ContentPage
    {
        public static String[] tags = new String[] {};
        public RequestJsonData parser = new RequestJsonData();
        public static string searchTags;

        public OneClickN1Page()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

			if(placeholderTags.Text == null){
                searchTagButton.IsEnabled = false;
                searchTagButton.BackgroundColor = Color.DarkBlue;
            }

        }

        public async void SearchTagButtonPushed (object sender, EventArgs args)
        {
			CreateTagsToSearch(tags);
			await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags);
            if (parser.JsonParseSucces == true){
				var newsPage = new OneClickN1NewsPage();
				await Navigation.PushAsync(newsPage);
            } else {
				errorLabel.IsVisible = true;
				searchTagButton.IsEnabled = false;
				searchTagButton.BackgroundColor = Color.DarkBlue;
				searchTagButton.TextColor = Color.White;
				
            }
            parser.JsonParseSucces = true;
            searchTags = null;

        }


        public void PlaceholderChangedText (object sender, EventArgs args){
            if (placeholderTags.Text == ""){
				searchTagButton.IsEnabled = false;
				searchTagButton.BackgroundColor = Color.DarkBlue;
				searchTagButton.TextColor = Color.White;
                errorLabel.IsVisible = false;
                } else {
                searchTagButton.IsEnabled = true;
				searchTagButton.BackgroundColor = Color.Blue;
				searchTagButton.TextColor = Color.White;
                errorLabel.IsVisible = false;

            } 
            tags = placeholderTags.Text.Split(' ');
           
		}

        public void ErrorHandle(){
            
        }

        public void N1NewsButton(object sender, EventArgs args){
            Device.OpenUri(new Uri("https://newsn1.com/"));
        }

        public void TelegramButton (object sender, EventArgs args){
            Device.OpenUri(new Uri("https://telegram.me/OneClickNewsBot"));   
        }

		private void CreateTagsToSearch(string[] tagsToSearchFor)
		{
			for (int i = 0; i < tagsToSearchFor.Length; i++)
			{
				searchTags += tagsToSearchFor[i] + "+";
			}
		}
    }
}
