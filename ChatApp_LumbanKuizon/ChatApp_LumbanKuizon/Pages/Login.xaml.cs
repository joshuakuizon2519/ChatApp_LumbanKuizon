using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.Text.RegularExpressions;
using ChatApp_LumbanKuizon.Pages;
using Xamarin.Forms.Xaml;
using ChatApp_LumbanKuizon.Models;
using ChatApp_LumbanKuizon.DependencyServices;

namespace ChatApp_LumbanKuizon
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
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
            var email = EntryEmail.Text;
            var emailPattern = "^([\\w\\.\\-]+)@([\\w\\-]+)((\\.(\\w){2,3})+)$";
            if (EntryEmail.Text == null && EntryPassword.Text == null)
            {
                await DisplayAlert("Error", "Missing Fields", "Okay");

            }
            else if(!String.IsNullOrWhiteSpace(email) && !(Regex.IsMatch(email, emailPattern)))
            {
                await DisplayAlert("Error", "Email not verified. Sent another verification email.", "Okay");
            }
            else
            {
                var Main = new ChatApp_LumbanKuizon.Pages.TabbedMain();
                await Navigation.PushAsync(Main);
                NavigationPage.SetHasNavigationBar(Main, false);
                NavigationPage.SetHasBackButton(Main, false);
            }

                

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

        private async void Button_clicked(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(EntryEmail.Text) && string.IsNullOrEmpty(EntryPassword.Text)){
                bool retryBool = await DisplayAlert("Error", "Missing Field/s, Retry", "Yes", "No");
                if (retryBool)
                {
                    EntryEmail.Text = string.Empty;
                    EntryPassword.Text = string.Empty;
                    EntryEmail.Focus();
                }
            }
            else
            {
                //loading.isVisible = true;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().LoginWithEmailPassword(EntryEmail.Text, EntryPassword.Text);

                if(res.Status == true)
                {
                    Application.Current.MainPage = new TabbedMain();
                }
                else
                {
                    bool retryBool = await DisplayAlert("Error", res.Response + " Retry?", "Yes", "No");
                    if (retryBool)
                    {
                        EntryEmail.Text = string.Empty;
                        EntryPassword.Text = string.Empty;
                        EntryEmail.Focus();
                    }
                }
            }
        }
       

    }
}
