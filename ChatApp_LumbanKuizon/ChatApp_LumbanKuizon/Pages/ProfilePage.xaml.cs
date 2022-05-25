using ChatApp_LumbanKuizon.DependencyServices;
using ChatApp_LumbanKuizon.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ChatApp_LumbanKuizon.Pages
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ProfilePage : ContentPage
    {
        public ProfilePage()
        {
            InitializeComponent();
        }

        private async void BacktoRoot(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void Logout_Button_Clicked(object sender, EventArgs e)
        {
            FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
            res = await DependencyService.Get<iFirebaseAuth>().SignOut();

            if(res.Status == true)
            {
                App.Current.MainPage = new NavigationPage(new TabbedMain());
            }
            else
            {
                await DisplayAlert("Error", res.Response, "Okay");
            }
        }
    }
}