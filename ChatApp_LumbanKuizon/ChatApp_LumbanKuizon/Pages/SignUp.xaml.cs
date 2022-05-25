using ChatApp_LumbanKuizon.DependencyServices;
using ChatApp_LumbanKuizon.Helpers;
using ChatApp_LumbanKuizon.Models;
using Plugin.CloudFirestore;
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
    public partial class SignUp : ContentPage
    {
        public SignUp()
        {
            InitializeComponent();
        }

        private async void ToLogin(object sender, EventArgs e)
        {
            await Navigation.PopToRootAsync();
        }

        private async void SignedUp(object sender, EventArgs e)
        {
            if (SignUpUsername.Text == null || SignUpEmail.Text == null || SignUpPassword.Text == null || SignUpConfirmPassword.Text == null)
            {
                await DisplayAlert("Error", "Missing Fields", "Okay");

            }
            if (SignUpPassword.Text != SignUpConfirmPassword.Text)
            {
                await DisplayAlert("Error", "Passwords should Match", "Okay");
            }
        }

        private async void Signup_Clicked (Object sender, EventArgs e)
        {
            if(SignUpEmail.Text.Length == 0 || SignUpPassword.Text.Length == 0 || SignUpConfirmPassword.Text.Length == 0)
            {
                await DisplayAlert("Error", "Missing Field/s", "Okay");
            }
            else
            {
                if(SignUpPassword.Text != SignUpConfirmPassword.Text)
                {
                    await DisplayAlert("Error", "Passwords don't match", "Okay");
                    SignUpConfirmPassword.Text = String.Empty;
                    SignUpConfirmPassword.Focus();
                }
                else
                {
                    FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                    res = await DependencyService.Get<iFirebaseAuth>().SignUpWithEmailPassword(SignUpUsername.Text, SignUpEmail.Text, SignUpPassword.Text);

                    if(res.Status == true)
                    {
                        try
                        {
                            await CrossCloudFirestore.Current.Instance.GetCollection("users").GetDocument(dataClass.loggedInUser.uid).SetDataAsync(dataClass.loggedInUser);

                            await DisplayAlert("Success", res.Response, "Okay");
                            await Navigation.PushModalAsync(true);
                        }
                        catch(Exception ex)
                        {
                            await DisplayAlert("Error", ex.Message, "Okay");
                        }
                    }
                    else
                    {
                        await DisplayAlert("Success", res.Response, "Okay");
                    }
                    //loading.IsVisible = false;
                }
            }
        }

    }
}