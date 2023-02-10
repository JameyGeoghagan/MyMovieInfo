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
   public class TvHubViewModel
    {

        private string APIKey = "API Key";
        
        public ICommand Search { get; set; }
        
        public Command OnAirCommand { get; set; }
        
        public Command AiringCommand { get; set; }
        
        public Command TopRatedCommand { get; set; }
        
        public Command PopularCommand { get; set; }
        
        public INavigation navigation;
        public string SearchTearm { get; set; }


        public TvHubViewModel(INavigation _navigation)
        {
            navigation = _navigation;
            Search = new Command(GetMethod);
            AiringCommand = new Command(GetLatest);
            OnAirCommand = new Command(GetOnAir);
            TopRatedCommand = new Command(GetTop);
            PopularCommand = new Command(GetPop);
        
    }
        public async void GetPop()
        {
            await navigation.PushAsync(new TvPopular());
        }
        public async void GetTop()
        {
            await navigation.PushAsync(new TvTopRated());
        }

        public async void GetLatest()
        {
            await navigation.PushAsync(new TvLatestView());
        }

        public async void GetOnAir()
        {
            await navigation.PushAsync(new OnAir());
        }

        public void GetMethod()
        {
            GetShow(SearchTearm);
        }

        public async void GetShow(object searchTearm)
        {
            try
            {
                var client = new HttpClient();
                var SearchUrl = $"https://api.themoviedb.org/3/search/tv?api_key=API Key&language=en-US&page=1&query={searchTearm}&include_adult=false";
                var SearchResponse = client.GetStringAsync(SearchUrl).Result;

                SearchShowModel.Root myDeserializedClass = JsonConvert.DeserializeObject<SearchShowModel.Root>(SearchResponse);

                var response = myDeserializedClass;

                ObservableCollection<SearchShowModel.Result> shows = new ObservableCollection<SearchShowModel.Result>();

                foreach (var item in response.results)
                {
                    shows.Add(item);
                }

                await navigation.PushAsync(new SearchShow(shows));
            }
            catch
            {
                await navigation.PushAsync(new SearchError());
            }



        }

    }
}
