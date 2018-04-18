using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace rbg.smite
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

            MainPage = new NavigationPage(new Views.MainPage());
        }

		protected override void OnStart ()
		{
            // Handle when your app starts
            AppCenter.Start("android=ab7a49b1-5823-4309-9107-e4fce048557f;" +
                            "uwp={Your UWP App secret here};" +
                            "ios={Your iOS App secret here}",
                  typeof(Analytics), typeof(Crashes));
        }

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
