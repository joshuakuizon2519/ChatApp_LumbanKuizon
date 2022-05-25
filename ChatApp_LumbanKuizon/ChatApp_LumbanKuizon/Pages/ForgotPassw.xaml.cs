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
    public partial class ForgotPassw : ContentPage
    {
        public ForgotPassw()
        {
            InitializeComponent();
        }

        private async void SendEmail_Clicked(object sender, EventArgs e)
        {
            if(Email.Text.Length == 0)
            {
                await DisplayAlert("Error", "Missing Field/s", "Okay");
            }
            else
            {
                //loadingVisible = true;
                FirebaseAuthResponseModel res = new FirebaseAuthResponseModel() { };
                res = await DependencyService.Get<iFirebaseAuth>().ResetPassword(Email.Text);

                if(res.Status == true)
                {
                    await DisplayAlert("Succes", res.Response, "Okay");
                    await Navigation.PopModalAsync();
                }
                else
                {
                    await DisplayAlert("Error", res.Response, "Okay");
                }
                //loadingVisible = false;
            }
        }
    }
}