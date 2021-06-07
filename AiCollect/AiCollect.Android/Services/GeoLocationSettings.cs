using AiCollect.Droid.Extensions;
using AiCollect.Droid.Services;
using AiCollect.Services;
using Android;
using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Gms.Common;
using Android.Gms.Location;
using Android.Locations;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using AndroidX.Core.Content;
using Plugin.CurrentActivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(GeoLocationSettings))]
namespace AiCollect.Droid.Services
{
    public class GeoLocationSettings : IGeoLocationSettings
    {
        private Context ctx;
        private Intent intent;
        private FusedLocationProviderClient fusedLocationProviderClient;
        private LocationRequest locationRequest;
        private LocationCallback locationCallback;
        private bool isRequestingLocationUpdates;
        public GeoLocationSettings()
        {
            Init();
        }

        public async Task<Xamarin.Essentials.Location> GetLocation()
        {
            try
            { 
                if (IsGooglePlayServicesInstalled())
                {
                    if (ContextCompat.CheckSelfPermission(CrossCurrentActivity.Current.Activity, Manifest.Permission.AccessFineLocation) == Permission.Granted)
                        return await GetLastLocation();
                    else
                        RequestLocationPermission(2444);
                }

                return null;
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }

            return null;
        }

        public Task<bool> IsGpsEnabled()
        {
            try
            {
                LocationManager LM = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);

                return Task.FromResult(LM.IsProviderEnabled(LocationManager.GpsProvider));
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
                return Task.FromResult(false);
            }
        }

        public Task<bool> OpenSettings()
        {
            try
            {
                LocationManager LM = (LocationManager)Android.App.Application.Context.GetSystemService(Context.LocationService);

                if (LM.IsProviderEnabled(LocationManager.GpsProvider) == false)
                {
                    ctx = Android.App.Application.Context;
                    intent = new Intent(Android.Provider.Settings.ActionLocationSourceSettings);
                    intent.AddFlags(ActivityFlags.NewTask);
                    ctx.StartActivity(intent);
                }

                return Task.FromResult(true);
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
                return Task.FromResult(false);
            }
        }

        public async void Init()
        {
            try
            {
                if (ContextCompat.CheckSelfPermission(CrossCurrentActivity.Current.AppContext, Manifest.Permission.AccessFineLocation) == Permission.Granted)
                {
                    //locationManager = (LocationManager)CrossCurrentActivity.Current.AppContext.GetSystemService(Context.LocationService);

                    InitLocationRequest();
                    await StartRequestingLocationUpdates();
                    isRequestingLocationUpdates = true;
                }
                else
                {
                    // The app does not have permission ACCESS_FINE_LOCATION 
                    RequestLocationPermission(2444);
                }
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }
        }

        private void InitLocationRequest()
        {
            try
            {
                locationRequest = new LocationRequest()
                                      .SetPriority(LocationRequest.PriorityHighAccuracy)
                                      .SetInterval(6 * 1000 * 2)
                                      .SetFastestInterval(6 * 1000);

                locationCallback = new FusedLocationProviderCallback((MainActivity)CrossCurrentActivity.Current.Activity);
                fusedLocationProviderClient = LocationServices.GetFusedLocationProviderClient(CrossCurrentActivity.Current.Activity);
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }
        }

        private async Task StartRequestingLocationUpdates()
        {
            try
            {
                await fusedLocationProviderClient.RequestLocationUpdatesAsync(locationRequest, locationCallback);
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }
        }

        private bool IsGooglePlayServicesInstalled()
        {
            try
            {
                var queryResult = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(CrossCurrentActivity.Current.AppContext);
                if (queryResult == ConnectionResult.Success)
                {
                    Log.Info("MainActivity", "Google Play Services is installed on this device.");
                    return true;
                }

                if (GoogleApiAvailability.Instance.IsUserResolvableError(queryResult))
                {
                    // Check if there is a way the user can resolve the issue
                    var errorString = GoogleApiAvailability.Instance.GetErrorString(queryResult);
                    Log.Error("MainActivity", "There is a problem with Google Play Services on this device: {0} - {1}",
                              queryResult, errorString);
                }
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }


            return false;
        }

        private async Task<Xamarin.Essentials.Location> GetLastLocation()
        {
            try
            { 
                var location = await fusedLocationProviderClient.GetLastLocationAsync();

                if (location != null)
                {
                    Toast.MakeText(CrossCurrentActivity.Current.Activity, "Accuracy => " + location.Accuracy, ToastLength.Long);
                    return new Xamarin.Essentials.Location
                    {
                        Accuracy = location.Accuracy,
                        Altitude = location.Altitude,
                        Latitude = location.Latitude,
                        Longitude = location.Longitude,
                        Speed = location.Speed
                    };
                }
                else
                    Log.Info("GPS STATUS", "Trouble retrieving location");
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }

            return null;
        }

        private void RequestLocationPermission(int requestCode)
        {
            try
            {
                if (ActivityCompat.ShouldShowRequestPermissionRationale(CrossCurrentActivity.Current.Activity, Manifest.Permission.AccessFineLocation))
                {
                    ActivityCompat.RequestPermissions(CrossCurrentActivity.Current.Activity, new[] { Manifest.Permission.AccessFineLocation }, requestCode);
                }
                else
                {
                    ActivityCompat.RequestPermissions(CrossCurrentActivity.Current.Activity, new[] { Manifest.Permission.AccessFineLocation }, requestCode);
                }
            }
            catch (Exception ex)
            {
                Log.Error("GPS EXCEPTION", ex.StackTrace);
            }
        }
    }
}