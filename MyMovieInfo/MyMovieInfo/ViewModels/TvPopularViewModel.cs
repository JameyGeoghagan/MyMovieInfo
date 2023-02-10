using MyMovieInfo.Models;
using MyMovieInfo.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
   public class TvPopularViewModel
    {
        private string APIkey = "API Key";
        public INavigation navigation { get; set; }

        public ObservableCollection<TvPopularModel.Result> Shows { get; set; }

        public Command Details { get; set; }
        public Command SendUserHome { get; set; }

        public TvPopularViewModel(INavigation _navigation)
        {
            navigation = _navigation;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/tv/popular?api_key=API Key&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            TvPopularModel.Root myDeserializedClass = JsonConvert.DeserializeObject<TvPopularModel.Root>(SearchResponse);

            ObservableCollection<TvPopularModel.Result> results = new ObservableCollection<TvPopularModel.Result>();
            foreach (var item in myDeserializedClass.results)
            {
                results.Add(item);
            }

            Shows = results;

            foreach (var item in Shows)
            {
                item.poster_path = $"https://image.tmdb.org/t/p/w500/{item.poster_path}";
            }

            SendUserHome = new Command(NavToHome);
            Details = new Command(GetDetails);
        }
        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }
        public async void GetDetails(Object obj)
        {
            await navigation.PushAsync(new TvShowDetails(obj));
        }
    }
}
