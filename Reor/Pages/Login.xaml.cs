using Microsoft.WindowsAzure.MobileServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Reor
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Login : ContentPage
	{
        IMobileServiceTable<Models.User> UserTable = App.MobileService.GetTable<Models.User>();

		public Login ()
		{
			InitializeComponent ();
		}

        private async void SignInClicked(object sender, EventArgs e)
        {
            var users = await UserTable.Where(x => x.Username == usernameEntry.Text & x.Password == passwordEntry.Text).ToListAsync();
            if(users.Count == 1)
            {
                Settings.UserId = users[0].Id;
                App.Current.MainPage = new NavigationPage(new Views.MyTabbedPage());
            }
            else
            {
                await DisplayAlert("Reor", "Username or password is incorrect", "Tamam");
            }
        }

        private void SignUpClicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Registration());
        }
    }
}