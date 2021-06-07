﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AiCollect.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Button = Xamarin.Forms.Button;
using AiCollect.Controls;

[assembly: ExportRenderer(typeof(LeftButton), typeof(LeftButtonRenderer))]
namespace AiCollect.Droid.Renderers
{
    class LeftButtonRenderer : ButtonRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Button> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    Control.SetBackgroundResource(Resource.Drawable.LeftButton);
                }
            }
            catch(Exception ex)
            {

            }
        }
    }
}