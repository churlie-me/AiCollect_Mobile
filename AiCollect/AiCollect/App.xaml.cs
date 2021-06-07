using AiCollect.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Linq;

namespace AiCollect
{
    public partial class App : Application
    {
        public static NavigationPage NavigationPage { get; private set; }
        public App()
        {
            InitializeComponent();
            MainPage = new NavigationPage(new SplashScreen());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
