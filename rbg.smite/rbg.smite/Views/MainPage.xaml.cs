using rbg.smite.ViewModels;
using Xamarin.Forms;

namespace rbg.smite.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(Navigation);
        }
    }
}
