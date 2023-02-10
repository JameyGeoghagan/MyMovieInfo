using MyMovieInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMovieInfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SearchView : ContentPage
    {
        public SearchView(ObservableCollection<Models.SearchMovieModel.Result> movies)
        {
            InitializeComponent();


            BindingContext = new SearchViewModel(movies, Navigation);
        }
    }
}