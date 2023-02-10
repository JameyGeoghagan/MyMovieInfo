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
   public class ActorTvViewModel
    {
        public string APIKey = "API Key";
        public ActorTvModel.Root TvList { get; set; }

        public string Title { get; set; }

        public ObservableCollection<ActorTvModel.Cast> Shows { get; set; }

        public INavigation navigation { get; set; }
        public Command Details { get; set; }
        public Command SendUserHome { get; set; }

        public ActorTvViewModel(INavigation _navigation, Object obj)
        {
            navigation = _navigation;
            Title = "Shows they have been in";

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/person/{obj}/tv_credits?api_key=API Key&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

           ActorTvModel.Root myDeserializedClass = JsonConvert.DeserializeObject<ActorTvModel.Root>(SearchResponse);

            TvList = myDeserializedClass;

            ObservableCollection<ActorTvModel.Cast> results = new ObservableCollection<ActorTvModel.Cast>();

            foreach (var item in TvList.cast)
            {
                results.Add(item);
            }

            Shows = results;

            foreach (var item in Shows)
            {
                item.poster_path = $"https://image.tmdb.org/t/p/w500/{item.poster_path}";
            }

            Details = new Command(GetDetails);
            SendUserHome = new Command(NavToHome);

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
