﻿using System;
using System.ComponentModel;
using UIKit;
using AiCollect.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AiCollect.Controls;

[assembly: ExportRenderer(typeof(AIEntry), typeof(CustomEntryRenderer))]
namespace AiCollect.iOS.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.BorderStyle = UITextBorderStyle.None;
            Control.TintColor = UIColor.LightTextColor;
        }
    }
}
