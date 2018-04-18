using Microsoft.AppCenter.Crashes;
using Newtonsoft.Json;
using rbg.smite.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace rbg.smite.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Properties
        private INavigation Navigation;

        private List<God> allGods;
        public List<God> AllGods
        {
            get { return allGods; }
            set
            {
                allGods = value;
                OnPropertyChanged();
            }
        }

        private List<God> filteredGods;
        public List<God> FilteredGods
        {
            get { return filteredGods; }
            set
            {
                filteredGods = value;
                OnPropertyChanged();
            }
        }

        private God selectedGod;
        public God SelectedGod
        {
            get { return selectedGod; }
            set
            {
                selectedGod = value;
                OnPropertyChanged();
            }
        }

        private string searchText;
        public string SearchText
        {
            get { return searchText; }
            set
            {
                searchText = value.ToLower();
                ExecuteSearchCommand();
            }
        }
        #endregion

        public MainPageViewModel(INavigation navigation)
        {
            Navigation = navigation;

            Title = "Liste des Dieux";

            //var json = Resources.Resources.GetJSONFromResources("gods.json");
            var json = Api.Helper.GetGods();
            AllGods = JsonConvert.DeserializeObject<List<God>>(json);
            AllGods = AllGods.OrderBy(x => x.Name).ToList();
            FilteredGods = AllGods;

            GoToDetailCommand = new Command(async (object o) => await ExecuteGoToDetailCommand(o));
        }

        public Command GoToDetailCommand { get; set; }
        private async Task ExecuteGoToDetailCommand(object param)
        {
            try
            {
                var sample = (God)param;
                SelectedGod = null;
                await Navigation.PushAsync(new Views.DetailPage(sample));
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }

        public Command SearchCommand { get; set; }
        private void ExecuteSearchCommand()
        {
            try
            {
                FilteredGods = AllGods.Where(
                          x => x.Name.ToLower().Contains(SearchText)
                            || x.Roles.ToLower().Contains(SearchText))
                          .ToList();
            }
            catch (Exception exception)
            {
                Crashes.TrackError(exception);
            }
        }
    }
}
