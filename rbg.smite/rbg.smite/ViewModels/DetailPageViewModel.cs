using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Plugin.Share;
using Plugin.Share.Abstractions;
using Plugin.TextToSpeech;
using Plugin.TextToSpeech.Abstractions;
using rbg.smite.Models;
using System;
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
            
            God = god;

            Title = God.Name;

            TextToSpeechCommand = new Command(async () => await CrossTextToSpeech.Current.Speak(
                God.Lore,
                new CrossLocale { Language = "en", Country = "en" }));
            ShareCommand = new Command(async () => await ExecuteShareCommand());

            Analytics.TrackEvent("God viewed: " + God.Name);
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
            catch(Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }
    }
}
