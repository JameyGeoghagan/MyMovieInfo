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
   public class SimMoviesModel
    {
        public string APIKey = "API Key";
        public SimModel.Root MoviesList { get; set; }

        public string Title { get; set; }

        public ObservableCollection<SimModel.Result> Movies { get; set; }

        public INavigation navigation { get; set; }
        public Command Details { get; set; }
        public Command SendUserHome { get; set; }
        public SimMoviesModel(Object obj, INavigation _navigation)
        {
            navigation = _navigation;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/movie/{obj}/similar?api_key={APIKey}&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            SimModel.Root myDeserializedClass = JsonConvert.DeserializeObject<SimModel.Root>(SearchResponse);

            MoviesList = myDeserializedClass;

            ObservableCollection<SimModel.Result> results = new ObservableCollection<SimModel.Result>();

            foreach (var item in MoviesList.results)
            {
                results.Add(item);
            }

            Movies = results;

            foreach (var item in Movies)
            {
                item.poster_path = $"https://image.tmdb.org/t/p/w500/{item.poster_path}";
                Title = $"Movies";
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
            await navigation.PushAsync(new MovieDetails(obj));
        }
    }
}
