using MyMovieInfo.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Net.Http;
using System.Text;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
   public class TvReviewsViewModel
    {

        private string APIkey = "API Key";
        public INavigation navigation { get; set; }

        public Object TvID { get; set; }

        public ObservableCollection<TvReviewsModel.Result> Reviews { get; set; }
        public Command SendUserHome { get; set; }

        public TvReviewsViewModel(INavigation _navigation, Object obj)
        {
            TvID = obj;
            navigation = _navigation;


            var client = new HttpClient();
            var SearchUrl = $"https://api.themoviedb.org/3/tv/{TvID}/reviews?api_key={APIkey}&language=en-US&page=1";
            var SearchResponse = client.GetStringAsync(SearchUrl).Result;
            TvReviewsModel.Root myDeserializedClass = JsonConvert.DeserializeObject<TvReviewsModel.Root>(SearchResponse);

            ObservableCollection<TvReviewsModel.Result> results = new ObservableCollection<TvReviewsModel.Result>();

            foreach (var item in myDeserializedClass.results)
            {
                results.Add(item);
            }

            Reviews = results;
            SendUserHome = new Command(NavToHome);


        }

        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }
    }
}
