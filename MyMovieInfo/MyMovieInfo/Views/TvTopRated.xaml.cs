using MyMovieInfo.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyMovieInfo.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TvTopRated : ContentPage
    {
        public TvTopRated()
        {
            InitializeComponent();
            BindingContext = new TvTopRatedViewModel(Navigation);
        }
    }
}