using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace Reor.Views
{
    class MyTabbedPage : TabbedPage
    {
        public MyTabbedPage()
        {
            Children.Add(new TimePage());
            Children.Add(new MainPage());


             var logout = new ToolbarItem();
             logout.Text = "Log Out";
             logout.Clicked += Logout_Clicked;
             this.ToolbarItems.Add(logout);


         }

         private void Logout_Clicked(object sender, EventArgs e)
         {

                 Settings.UserId = null;
                 App.Current.MainPage = new NavigationPage(new Login());

         }
         
        
    }
}

