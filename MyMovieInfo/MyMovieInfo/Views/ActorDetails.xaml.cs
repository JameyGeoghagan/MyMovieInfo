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
    public partial class ActorDetails : ContentPage
    {
        public ActorDetails(Object obj)
        {
            InitializeComponent();
            BindingContext = new ActorDetailsViewmodel(Navigation, obj);
        }
    }
}