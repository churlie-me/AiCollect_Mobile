using System;
using System.ComponentModel;
using UIKit;
using AiCollect.iOS.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using AiCollect.Controls;

[assembly: ExportRenderer(typeof(AIEditor), typeof(AIEditorRenderer))]
namespace AiCollect.iOS.Renderers
{
    public class AIEditorRenderer : EditorRenderer
    {
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            Control.Layer.BorderWidth = 0;
            Control.TintColor = UIColor.LightTextColor;
        }
    }
}
