using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Reor
{
    public partial class App : Application
    {
        public static MobileServiceClient MobileService = new MobileServiceClient("https://reor.azurewebsites.net");
        public App()
        {
            InitializeComponent();
          if (!string.IsNullOrEmpty(Settings.UserId))
          MainPage = new NavigationPage(new Reor.Views.MyTabbedPage());
          else
                MainPage = new NavigationPage(new Login());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
