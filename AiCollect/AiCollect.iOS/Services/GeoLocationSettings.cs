using AiCollect.iOS.Services;
using AiCollect.Services;
using CoreLocation;
using Foundation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(GeoLocationSettings))]
namespace AiCollect.iOS.Services
{
    public class GeoLocationSettings : IGeoLocationSettings
    {
        public async Task<Location> GetLocation()
        {
            try
            {
                return await Geolocation.GetLastKnownLocationAsync();
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Handle not supported on device exception
            }
            catch (FeatureNotEnabledException fneEx)
            {
                // Handle not enabled on device exception
            }
            catch (PermissionException pEx)
            {
                // Handle permission exception
            }
            catch (Exception ex)
            {
                // Unable to get location
            }

            return null;
        }

        public Task<bool> IsGpsEnabled()
        {
            return Task.FromResult(CLLocationManager.LocationServicesEnabled);
        }

        public Task<bool> OpenSettings()
        {
            try
            {
                if (UIDevice.CurrentDevice.CheckSystemVersion(8, 0))
                {
                    // For iOS 8 and 9, we can navigate automatically to the settings
                    NSUrl url = new NSUrl(UIKit.UIApplication.OpenSettingsUrlString);
                    bool result = UIApplication.SharedApplication.OpenUrl(url);
                }
                else
                {
                    // Below iOS 8, the user has to navigate manually to the settings
                    UIAlertView uiAlert = new UIAlertView(
                        "Location Services Required",
                        "Authorisation to use your location is required to use this feature of the app. To use this feature please go to the settings app and enable location services",
                        null,
                        "Ok");
                    uiAlert.Show();
                }
            }
            catch (Exception ex)
            {

            }

            return Task.FromResult(true);
        }
    }
}