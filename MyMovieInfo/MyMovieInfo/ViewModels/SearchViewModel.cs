using MyMovieInfo.Models;
using MyMovieInfo.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Xamarin.Forms;

namespace MyMovieInfo.ViewModels
{
  public class SearchViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<SearchMovieModel.Root> movies;

        public ObservableCollection<SearchMovieModel.Result> Movies { get; set; }

        public SearchMovieModel SearchMovieModel { get; set; }
        public INavigation navigation { get; set; }
        public Command Details { get; set; }
        public Command SendUserHome { get; set; }

        public SearchViewModel(ObservableCollection<Models.SearchMovieModel.Result> movies, INavigation _navigation)
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

        public SearchViewModel(ObservableCollection<SearchMovieModel.Root> movies, INavigation navigation)
        {
            this.movies = movies;
            this.navigation = navigation;
        }

        public async void NavToHome()
        {
            await navigation.PopToRootAsync();
        }
        public async void GetDetails(Object obj)
        {
            await navigation.PushAsync(new MovieDetails(obj));
        }
    
        
        
        #region INotifyPropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
                handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
