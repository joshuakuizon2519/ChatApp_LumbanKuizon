using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp_LumbanKuizon
{
    public partial class Login : ContentPage
    {
        public Login()
        {
            InitializeComponent();
        }

        private async void ToSignUp(object sender, EventArgs e)
        {
            var SignupPage = new ChatApp_LumbanKuizon.Pages.SignUp();
            await Navigation.PushAsync(SignupPage);
            NavigationPage.SetHasNavigationBar(SignupPage, false);
            NavigationPage.SetHasBackButton(SignupPage, false);
        }

        private async void ToForgotPass(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatApp_LumbanKuizon.Pages.ForgotPassw());
        }

        private async void ToMain(object sender, EventArgs e)
        {
            var Main = new ChatApp_LumbanKuizon.Pages.TabbedMain();
            await Navigation.PushAsync(Main);
            NavigationPage.SetHasNavigationBar(Main, false);
            NavigationPage.SetHasBackButton(Main, false);
        }
        async void OnTapGestureRecognizerTapped(object sender, EventArgs args)
        {
            int tapCount = 0;
            tapCount++;
            if (tapCount % 1 == 0)
            {
                await Navigation.PushAsync(new ChatApp_LumbanKuizon.Pages.ForgotPassw());
            }
        }
       
    }
}
