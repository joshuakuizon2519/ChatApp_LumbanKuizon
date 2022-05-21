using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ChatApp_LumbanKuizon;
using ChatApp_LumbanKuizon.Droid;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(RoundedEntry), typeof(CustomEntryRenderer))]

namespace ChatApp_LumbanKuizon.Droid
{
    class CustomEntryRenderer : EntryRenderer
    {
       public CustomEntryRenderer(Context context) : base(context)
        {
        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                var grDrawable = new Android.Graphics.Drawables.GradientDrawable();
                grDrawable.SetStroke(2, global::Android.Graphics.Color.LightSlateGray);
                grDrawable.SetCornerRadius(30f);
                Control.SetPadding(20,15,15,15); 

                
                Control.SetBackground(grDrawable);
            }
            
        }
       
    }
}