using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Com.Mapbox.Mapboxsdk.Maps;
using Com.Mapbox.Mapboxsdk.Annotations;
using Com.Mapbox.Mapboxsdk.Geometry;
using static Com.Mapbox.Mapboxsdk.Maps.MapboxMap;
using Plugin.CurrentActivity;
using Android;

[assembly: UsesFeature("android.hardware.camera", Required = true)]
[assembly: UsesFeature("android.hardware.camera.autofocus", Required = true)]
namespace AiCollect.Droid
{
    [Activity(Label = "AiCollect", Icon = "@drawable/ic_launcher", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            try
            {
                TabLayoutResource = Resource.Layout.Tabbar;
                ToolbarResource = Resource.Layout.Toolbar;

                base.OnCreate(savedInstanceState);

                //Initialize Popup 
                Rg.Plugins.Popup.Popup.Init(this);

                //Initialize mapbox in Android.
                Com.Mapbox.Mapboxsdk.Mapbox.GetInstance(CrossCurrentActivity.Current.AppContext, AICollect.Utils.MapBox.AccessToken);
                Com.Mapbox.Mapboxsdk.Mapbox.Telemetry.SetDebugLoggingEnabled(true);

                //Initialize Plugin.InputKit
                Plugin.InputKit.Platforms.Droid.Config.Init(this, savedInstanceState);

                // Initailize the camera plugin.
                CrossCurrentActivity.Current.Init(this, savedInstanceState);

                //Initialize Xamarin Essentials
                Xamarin.Essentials.Platform.Init(this, savedInstanceState);

                global::Xamarin.Forms.Forms.Init(this, savedInstanceState);

                LoadApplication(new App());
            }
            catch (Exception ex)
            {

            }
        }

        protected override void OnStart()
        {
            base.OnStart();
            try
            {
                if (CheckSelfPermission(Manifest.Permission.AccessCoarseLocation) != (int)Permission.Granted)
                {
                    RequestPermissions(new string[] { Manifest.Permission.AccessCoarseLocation, Manifest.Permission.AccessFineLocation }, 0);
                }
            }
            catch (Exception ex)
            {

            }
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            try
            {
                Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

                Plugin.Permissions.PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);

                base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            }
            catch (Exception ex)
            {

            }
        }
    }
}