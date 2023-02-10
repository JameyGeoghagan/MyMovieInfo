using MyMovieInfo.Models;
using MyMovieInfo.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
    public class MovieDetailsViewModel
    {
        string APIKey = "API Key";
        public Movie.Root movie { get; set; }

        public INavigation navigation { get; set; }
        public string backdrop { get; set; }

        public string poster { get; set; }

        public string Title { get; set; }

        public string Released { get; set; }

        public int RunTime { get; set; }

        public double Pop { get; set; }

        public Object MovieID { get; set; }

        public Command SimMovies { get; set; }
        public Command TrailerCommand { get; set; }

        public Command Review { get; set; }

        public Command Actors { get; set; }

        public int Revenue { get; set; }

        public int Budget { get; set; }
        public Command SendUserHome { get; set; }

        public string OverView { get; set; }
        public MovieDetailsViewModel(Object obj, INavigation _navigation)
        {
            GetMovie(obj);

            MovieID = obj;

            SimMovies = new Command(GetSimMovies);

            TrailerCommand = new Command(GetTrailor);

            Review = new Command(GetReviews);
            Actors = new Command(GetActors);
            SendUserHome = new Command(NavToHome);

            navigation = _navigation;

        }

        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }
        public async void GetActors(Object obj)
        {
            await navigation.PushAsync(new ActorList(obj));
        }


        public async void GetReviews(Object obj)
        {
            await navigation.PushAsync(new Reviews(obj));

        }


        public async void GetTrailor()
        {
            string key = "";
            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/movie/{MovieID}/videos?api_key={APIKey}&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            TrailorModel.Root myDeserializedClass = JsonConvert.DeserializeObject<TrailorModel.Root>(SearchResponse);
            ObservableCollection<TrailorModel.Result> results = new ObservableCollection<TrailorModel.Result>();

            foreach (var item in myDeserializedClass.results)
            {
                results.Add(item);
            }


            foreach (var item in results)
            {
                key = item.key;
            }

            await Launcher.OpenAsync($"https://www.youtube.com/watch?v={key}");


        }

        public async void GetSimMovies(Object obj)
        {
            await navigation.PushAsync(new SimMovies(obj));

        }

        public void GetMovie(object obj)
        {
            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/movie/{obj}?api_key={APIKey}&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            Movie.Root myDeserializedClass = JsonConvert.DeserializeObject<Movie.Root>(SearchResponse);

            movie = myDeserializedClass;

            backdrop = $"https://image.tmdb.org/t/p/w500/{movie.backdrop_path}";

            poster = $"https://image.tmdb.org/t/p/w500/{movie.poster_path}";

            Title = movie.title;

            Pop = movie.vote_average;

            RunTime = movie.runtime;

            Released = movie.release_date;

            OverView = movie.overview;

            Budget = movie.budget;

            Revenue = movie.revenue;
        }
    }
}
