using MyMovieInfo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
   public class ReviewsViewModel
    {
        private string APIkey = "API Key";
        public INavigation navigation { get; set; }

        public Object MovieID { get; set; }

        public ObservableCollection<ReviewsModel.Result> Reviews { get; set; }
        public Command SendUserHome { get; set; }
        public ReviewsViewModel(INavigation navigation, Object obj)
        {
            MovieID = obj;

            this.navigation = navigation;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/movie/{MovieID}/reviews?api_key={APIkey}&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            ReviewsModel.Root myDeserializedClass = JsonConvert.DeserializeObject<ReviewsModel.Root>(SearchResponse);

            ObservableCollection<ReviewsModel.Result> results = new ObservableCollection<ReviewsModel.Result>();

            foreach (var item in myDeserializedClass.results)
            {
                results.Add(item);
            }

            Reviews = results;
            SendUserHome = new Command(NavToHome);
        }
        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }
    }
}
