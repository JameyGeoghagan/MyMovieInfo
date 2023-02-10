using MyMovieInfo.Models;
using MyMovieInfo.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using System.Windows.Input;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
   public class SimTvViewModel
    {
        public string APIKey = "API Key";
        public SimTvModel.Root ShowList { get; set; }

        public string Title { get; set; }

        public ObservableCollection<SimTvModel.Result> Shows { get; set; }

        public INavigation navigation { get; set; }
        public ICommand Details { get; set; }
        public Command SendUserHome { get; set; }

        public SimTvViewModel(Object obj, INavigation _navigation)
        {
            navigation = _navigation;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/tv/{obj}/similar?api_key={APIKey}&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            SimTvModel.Root myDeserializedClass = JsonConvert.DeserializeObject<SimTvModel.Root>(SearchResponse);

            ShowList = myDeserializedClass;

            ObservableCollection<SimTvModel.Result> results = new ObservableCollection<SimTvModel.Result>();

            foreach (var item in ShowList.results)
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
