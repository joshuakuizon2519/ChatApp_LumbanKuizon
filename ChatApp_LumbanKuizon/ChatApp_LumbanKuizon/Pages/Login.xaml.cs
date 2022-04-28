using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ChatApp_LumbanKuizon
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void ToSignUp(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatApp_LumbanKuizon.Pages.SignUp());
        }

        private async void ToForgotPass(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new ChatApp_LumbanKuizon.Pages.ForgotPassw());
        }
    }
}
