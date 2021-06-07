using System;
using Android.Content;
using Android.Content.Res;
using Android.Graphics.Drawables;
using Android.Text;
using AiCollect.Droid.Renderers;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using AiCollect.Controls;

[assembly: ExportRenderer(typeof(AIEntry), typeof(CustomEntryRenderer))]
namespace AiCollect.Droid.Renderers
{
    public class CustomEntryRenderer : EntryRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            try
            {
                if (Control != null)
                {
                    GradientDrawable gd = new GradientDrawable();
                    gd.SetColor(global::Android.Graphics.Color.Transparent);
                    this.Control.SetBackground(gd);

                    this.Control.SetRawInputType(InputTypes.TextFlagNoSuggestions);
                    Control.SetHintTextColor(ColorStateList.ValueOf(global::Android.Graphics.Color.LightSlateGray));
                }
            }
            catch( Exception ex)
            {

            }
        }
    }
}