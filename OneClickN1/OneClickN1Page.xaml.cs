﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Globalization;
using System.Reflection;
using System.Resources;

namespace OneClickN1
{
    public partial class OneClickN1Page : ContentPage
    {
        private static String[] tags = new String[] {};
        private RequestJsonData parser = new RequestJsonData();
        public static string searchTags;

        public OneClickN1Page()
        {
            InitializeComponent();
			NavigationPage.SetHasNavigationBar(this, false);

            /* 
             * При инициализации страницы кнопка не доступна так как в поле отсуствуют теги 
            */

			if(placeholderTags.Text == null){
                searchTagButton.IsEnabled = false;
            }

        }


        /* 
         * При нажатии на кнопку создаются теги для обработки, данные теги передаются парсеру и обрабатываются Json, 
         * если запрос новостей прошел успешно то проиходит переход на следующую страницу с новостями, 
         * если нет то появляется сообщение об ошибке и перехода не приосходит.
        */

        private async void SearchTagButtonPushed(object sender, EventArgs args)
        {
            IsBusy = true;
            CreateTagsToSearch(tags);
            searchTagButton.IsEnabled = false;
            imageButton.Source = "back_btn_ina";

            await parser.MakeGetRequest("https://newsn1.com/?mode=query&mask=" + searchTags+"&fillpic=1");

            if (parser.JsonParseSucces == true)
            {
                var newsPage = new OneClickN1NewsPage();
                await Navigation.PushAsync(newsPage);
				parser.JsonParseSucces = true;
                placeholderTags.Text = "";
			}
            else
            {
                errorLabel.IsVisible = true;
                searchTagButton.IsEnabled = false;
                searchTagButton.TextColor = Color.White;
            }

            searchTags = null;
			IsBusy = false;

		}

        /* 
         *  При изменении содержания поля для тегов, то есть при вводе самих тегов пользователем кнопка "Поиск новостей" изменяет состояние
         *  на рабочее и не рабочее.
         */

        private void PlaceholderChangedText(object sender, EventArgs args)
        {
            if (placeholderTags.Text == "")
            {
                imageButton.Source = "back_btn_ina";
                searchTagButton.IsEnabled = false;
                searchTagButton.TextColor = Color.White;
                errorLabel.IsVisible = false;
            }

            else
            {
                imageButton.Source = "back_btn_one";
                searchTagButton.IsEnabled = true;
                searchTagButton.TextColor = Color.White;
                errorLabel.IsVisible = false;

            }

            tags = placeholderTags.Text.Split(' ');

        }

        /*
         * Методы для открытия сторонних URl на мобильном етелефоне
         */

        private void N1NewsButton(object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("https://newsn1.com/"));
        }

        private void TelegramButton (object sender, EventArgs args)
        {
            Device.OpenUri(new Uri("https://telegram.me/OneClickNewsBot"));   
        }

		/*
		 * Метод позволяющий создавать тип данных string нужного формата (перечисление тегов через знак +) 
		 */

		private void CreateTagsToSearch(string[] tagsToSearchFor)
		{
			for (int i = 0; i < tagsToSearchFor.Length; i++)
			{
				searchTags += tagsToSearchFor[i] + "+";
			}
		}
    }
}