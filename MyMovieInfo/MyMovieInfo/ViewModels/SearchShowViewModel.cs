using MyMovieInfo.Models;
using MyMovieInfo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
    public class SearchShowViewModel
    {
        public ObservableCollection<SearchShowModel.Result> Movies { get; set; }
        public SearchShowModel SearchShowModel { get; set; }
        public INavigation navigation { get; set; }
        public Command Details { get; set; }
        public Command SendUserHome { get; set; }

        public SearchShowViewModel(ObservableCollection<SearchShowModel.Result> movies, INavigation _navigation)
        {
            navigation = _navigation;
            Movies = movies;

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
            await navigation.PushAsync(new TvShowDetails(obj));
        }

    }
}
