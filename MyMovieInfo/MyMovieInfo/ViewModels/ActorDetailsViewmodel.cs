using MyMovieInfo.Models;
using MyMovieInfo.Views;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
    public class ActorDetailsViewmodel
    {
        string APIKey = "API Key";

        public Object ActorID { get; set; }

        public string profile_path { get; set; }

        public string name { get; set; }

        public string birthday { get; set; }

        public string birth { get; set; }

        public string Biography { get; set; }
        public bool Death { get; set; }

        public string deathday { get; set; }

        public Command ActorMovies { get; set; }
        public Command ActorTv { get; set; }

        public ActorModel.Root Actor { get; set; }
        public INavigation navigation { get; set; }

        public Command SendUserHome { get; set; }

        public ActorDetailsViewmodel(INavigation _navigation, Object obj)
        {
            navigation = _navigation;

            ActorID = obj;

            GetActor(ActorID);

            ActorMovies = new Command(Getmovies);

            ActorTv = new Command(GetShows);

            SendUserHome = new Command(NavToHome);

        }

        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }

        public async void GetShows(object obj)
        {
            await navigation.PushAsync(new ActorShows(obj));
        }

        public async void Getmovies(Object obj)
        {
            await navigation.PushAsync(new ActorsMovies(obj));
        }

        public void GetActor(Object actor)
        {

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/person/{actor}?api_key={APIKey}&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            ActorModel.Root myDeserializedClass = JsonConvert.DeserializeObject<ActorModel.Root>(SearchResponse);
            Actor = myDeserializedClass;

            profile_path = $"https://image.tmdb.org/t/p/w500/{Actor.profile_path}";
            name = Actor.name;
            birthday = Actor.birthday;
            birth = Actor.place_of_birth;
            if(Actor.deathday != null)
            {
                Death = true;
                deathday = Actor.deathday.ToString();
            }

            Biography = Actor.biography;
        }
    }
}
