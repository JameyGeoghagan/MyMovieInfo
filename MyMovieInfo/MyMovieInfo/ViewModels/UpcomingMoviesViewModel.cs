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
   public class UpcomingMoviesViewModel
    {
        private string APIkey = "API Key";
        public ObservableCollection<UpCommingModel.Result> Movies { get; set; }
        public INavigation navigation { get; set; }

        public Command Details { get; set; }
        public Command SendUserHome { get; set; }
        public UpcomingMoviesViewModel(INavigation _navigation)
        {
            navigation = _navigation;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/movie/upcoming?api_key={APIkey}&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            UpCommingModel.Root myDeserializedClass = JsonConvert.DeserializeObject<UpCommingModel.Root>(SearchResponse);

            ObservableCollection<UpCommingModel.Result> results = new ObservableCollection<UpCommingModel.Result>();

            foreach (var item in myDeserializedClass.results)
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
