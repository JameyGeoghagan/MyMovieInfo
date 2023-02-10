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
   public class TvShowDetailsViewModel
    {
        string APIKey = "API Key";
       
        public TvShowModel.Root TvShow { get; set; }

        public INavigation navigation { get; set; }
        public string backdrop { get; set; }

        public string poster { get; set; }

        public string Name { get; set; }

        public string First { get; set; }

        public string Last { get; set; }

        public int Seasons { get; set; }

        public int Episodes { get; set;}

        public double Vote { get; set; }

        public Object ShowID { get; set; }

        public Command SimTv { get; set; }
        public Command TrailerCommand { get; set; }

        public Command Review { get; set; }

        public Command Actors { get; set; }

        public Command GetSeasons { get; set; }
        public int Revenue { get; set; }

        public int Budget { get; set; }
        public Command SendUserHome { get; set; }

        public string Title { get; set; }

        public string OverView { get; set; }

        public TvShowDetailsViewModel(Object obj, INavigation _navigation)
        {
            GetShow(obj);
            SimTv = new Command(GetSimShows);
            TrailerCommand = new Command(GetTrailor);
            Review = new Command(GetReviews);
            Actors = new Command(GetActors);
            ShowID = obj;
            SendUserHome = new Command(NavToHome);
            navigation = _navigation;
        }

        public async void GetSimShows(Object obj)
        {
            await navigation.PushAsync(new SimTv(obj));

        }

        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }

        public async void GetActors(Object obj)
        {
            await navigation.PushAsync(new ShowActorList(obj));
        }

        public async void GetReviews(Object obj)
        {
            await navigation.PushAsync(new TvReviews(obj));

        }

        public async void GetTrailor()
        {
            string keyToUse = "";
            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/tv/{ShowID}/videos?api_key={APIKey}&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            TrailorModel.Root myDeserializedClass = JsonConvert.DeserializeObject<TrailorModel.Root>(SearchResponse);
            ObservableCollection<TrailorModel.Result> results = new ObservableCollection<TrailorModel.Result>();

            foreach (var item in myDeserializedClass.results)
            {
                results.Add(item);
            }

           

            foreach (var item in results)
            {
                keyToUse = item.key;
                if(item.site == "YouTube")
                {
                    keyToUse = item.key;
                }
            }


            await Launcher.OpenAsync($"https://www.youtube.com/watch?v={keyToUse}");


        }



        public void GetShow(object obj)
        {
            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/tv/{obj}?api_key={APIKey}&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            TvShowModel.Root myDeserializedClass = JsonConvert.DeserializeObject<TvShowModel.Root>(SearchResponse);

            TvShow = myDeserializedClass;

            backdrop = $"https://image.tmdb.org/t/p/w500/{TvShow.backdrop_path}";

            poster = $"https://image.tmdb.org/t/p/w500/{TvShow.poster_path}";

            Name = TvShow.name;

            First = TvShow.first_air_date;

            Last = TvShow.last_air_date;

            Seasons = TvShow.number_of_seasons;

            Episodes = TvShow.number_of_episodes;

            Vote = TvShow.vote_average;

            OverView = TvShow.overview;

            Title = TvShow.name;
            
         

           
        }
    }
}
