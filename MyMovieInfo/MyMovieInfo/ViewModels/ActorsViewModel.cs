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
    public class ActorsViewModel
    {
        public string APIKey = "API Key";
        public ActorsMovieModel.Root MoviesList { get; set; }

        public string Title { get; set; }

        public ObservableCollection<ActorsMovieModel.Result> Movies { get; set; }

        public INavigation navigation { get; set; }
        public Command Details { get; set; }
        public Command SendUserHome { get; set; }

        public ActorsViewModel(INavigation _navigation, Object obj)
        {
            navigation = _navigation;

            Title = "Movies they played in";

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/discover/movie?api_key=API Key&language=en-US&sort_by=popularity.desc&include_adult=false&include_video=false&page=1&with_cast={obj}&with_watch_monetization_types=flatrate";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            ActorsMovieModel.Root myDeserializedClass = JsonConvert.DeserializeObject<ActorsMovieModel.Root>(SearchResponse);

            MoviesList = myDeserializedClass;

            ObservableCollection<ActorsMovieModel.Result> results = new ObservableCollection<ActorsMovieModel.Result>();

            foreach (var item in MoviesList.results)
            {
                results.Add(item);
            }

            Movies = results;

            foreach (var item in Movies)
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
            await navigation.PushAsync(new MovieDetails(obj));
        }
    }
}
