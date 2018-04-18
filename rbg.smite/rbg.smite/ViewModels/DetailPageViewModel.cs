using Plugin.Share;
using Plugin.Share.Abstractions;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using rbg.smite.Models;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace rbg.smite.ViewModels
{
    public class DetailPageViewModel : BaseViewModel
    {
        #region Properties
        private INavigation Navigation;

        private God god;
        public God God
        {
            get { return god; }
            set { god = value; }
        }
        #endregion

        public DetailPageViewModel(INavigation navigation, God god)
        {
            Navigation = navigation;

            Title = "Détail d'un Dieu";

            God = god;

            TextToSpeechCommand = new Command(async () => await CrossTextToSpeech.Current.Speak(
                God.Lore,
                new CrossLocale { Language = "fr", Country = "fr" }));
            ShareCommand = new Command(async () => await ExecuteShareCommand());
        }

        public Command TextToSpeechCommand { get; set; }

        public Command ShareCommand { get; set; }
        private async Task ExecuteShareCommand()
        {
            try
            {
                await Task.Factory.StartNew(() =>
                {
                    if (!CrossShare.IsSupported)
                        return;

                    CrossShare.Current.Share(new ShareMessage
                    {
                        Title = "Ce perso de Smite est fou!",
                        Text = "Regardes ce Dieu du jeu Smite: " + God.Name,
                        Url = God.GodCard_URL
                    });
                });
            }
            catch { }
        }
    }
}
