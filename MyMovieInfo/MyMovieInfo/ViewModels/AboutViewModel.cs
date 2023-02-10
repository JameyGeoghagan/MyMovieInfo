using MyMovieInfo.Models;
using MyMovieInfo.Views;
using Newtonsoft.Json;
using System;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
    public class AboutViewModel 
    {

        private string APIKey = "API Key";

      
        public Command SearchMovie { get; set; }

        public Command UpcomingCommand { get; set; }

        public Command TopRatedCommand { get; set; }

        public Command NowPlayingCommand { get; set; }

        public Command NewMoviesCommand { get; set; }

        public string SearchTearm { get; set; }




        public AboutViewModel(INavigation navigation)
        {
           
            OpenWebCommand = new Command(async () => await Browser.OpenAsync("https://aka.ms/xamarin-quickstart"));
            this.navigation = navigation;
            SearchMovie = new Command(RunMethod);
            UpcomingCommand = new Command(GetUpcoming);
            TopRatedCommand = new Command(GetTopRated);
            NowPlayingCommand = new Command(GetNowPlaying);
            NewMoviesCommand = new Command(GetNewMovies);
        }

        public async void GetNewMovies()
        {
            await navigation.PushAsync(new NewMovies());
        }

        public async void GetNowPlaying()
        {
            await navigation.PushAsync(new NowPlaying());
        }

        public async void GetTopRated()
        {
            await navigation.PushAsync(new TopRated());
        }

        public async void GetUpcoming()
        {
            await navigation.PushAsync(new UpcomingMovies());
        }

        public void RunMethod()
        {
            GetMovie(SearchTearm);
        }

        private async void GetMovie(object searchTearm)
        {
            try
            {
            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/search/movie?api_key=API Key&language=en-US&query={searchTearm}&page=1&include_adult=false";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            SearchMovieModel.Root  myDeserializedClass = JsonConvert.DeserializeObject<SearchMovieModel.Root>(SearchResponse);

            var response = myDeserializedClass;

            ObservableCollection<SearchMovieModel.Result> movies = new ObservableCollection<SearchMovieModel.Result>();

            foreach (var item in response.results)
            {
                movies.Add(item);
            }

                await navigation.PushAsync(new SearchView(movies));
            }
            catch
            {
              await  navigation.PushAsync(new SearchError());
            }
           

            
        }

        public ICommand OpenWebCommand { get; }

        private INavigation navigation;
    }
}