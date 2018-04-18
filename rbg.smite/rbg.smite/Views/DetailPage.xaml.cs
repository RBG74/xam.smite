using rbg.smite.Models;
using rbg.smite.ViewModels;
using Xamarin.Forms;

namespace rbg.smite.Views
{
    public partial class DetailPage : ContentPage
    {
        public DetailPage(God god)
        {
            InitializeComponent();
            BindingContext = new DetailPageViewModel(Navigation, god);
        }
    }
}