using Microsoft.WindowsAzure.MobileServices;
using Reor.Models;
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
    public partial class Registration : ContentPage
    {
        IMobileServiceTable<Models.User> UserTable = App.MobileService.GetTable<Models.User>();

        public Registration()
        {
            InitializeComponent();
        }

        async void RegisterClicked(object sender, EventArgs e)
        {
            if (regPassword.Text == regPassConfirm.Text)
            {
                var users = await UserTable.Where(x => x.Username == regUsername.Text | x.Email == regEmail.Text).ToListAsync();
                if (users.Count == 0)
                {
                    User user = new User()
                    {
                        Username = regUsername.Text,
                        Password = regPassConfirm.Text,
                        Email = regEmail.Text
                    };

                    await UserTable.InsertAsync(user);

                    if (user.Id != null)
                    {

                        await Navigation.PopAsync();
                    }
                    else
                    {

                    }
                }
                else
                {
                    //TODO: Exists User Alert
                }
            }
            else
            {
                //TODO: Password Validation Error Alert
            }
        }
    }
}