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

    }
}