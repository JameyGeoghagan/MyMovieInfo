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
   public class TvLatestViewModel
    {
        private string APIkey = "API Key";
        public INavigation navigation { get; set; }

        public ObservableCollection<TvLatest.Result> Shows { get; set; }

        public Command Details { get; set; }
        public Command SendUserHome { get; set; }

        public TvLatestViewModel(INavigation _navigation)
        {
            navigation = _navigation;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/tv/airing_today?api_key={APIkey}&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            TvLatest.Root myDeserializedClass = JsonConvert.DeserializeObject<TvLatest.Root>(SearchResponse);

            ObservableCollection<TvLatest.Result> results = new ObservableCollection<TvLatest.Result>();
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
