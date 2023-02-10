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
   public class ActorListViewModel
    {
        private string APIkey = "API Key";
        public INavigation navigation { get; set; }

        public Object MovieID { get; set; }
        public Command SendUserHome { get; set; }
        public Object Id { get; set; }
        public ObservableCollection<ActorsListModel.Cast> Actors { get; set; }

        public Command Details { get; set; }
        public ActorListViewModel(INavigation _navigation, Object obj)
        {
            navigation = _navigation;

            MovieID = obj;

            Id = MovieID;

            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/movie/{MovieID}/credits?api_key={APIkey}&language=en-US";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;

            ActorsListModel.Root myDeserializedClass = JsonConvert.DeserializeObject<ActorsListModel.Root>(SearchResponse);

            ObservableCollection<ActorsListModel.Cast> results = new ObservableCollection<ActorsListModel.Cast>();

            foreach (var item in myDeserializedClass.cast)
            {
                results.Add(item);
            }

            Actors = results;

            foreach (var item in Actors)
            {
                item.profile_path = $"https://image.tmdb.org/t/p/w500/{item.profile_path}";
            }


            Details = new Command(GetDetails);
            SendUserHome = new Command(NavToHome);


        }


        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }

        public async void GetDetails(object obj)
        {
            await navigation.PushAsync(new ActorDetails(obj));
        }
    }
}
