using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CognitiveServices.Views;
using Xamarin.Forms;

namespace ComputerVisionApplication
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new TabbedPage
            {
                Children =
                {
                    new ComputerVisionPage(),
                    new OcrPage(),
                    new EmotionPage(),
                    new TextAnalyticsPage(),
                }
            };
            //MainPage = new ComputerVisionApplication.MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
