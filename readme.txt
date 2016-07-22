
IMPORTANT!
Please make sure to install nuget package Xam.Plugin.Media into your Solution (not only PCL project) 
to use Camera and pick photos. If not, your app won't build.


How to use it ?
This package provides a sample ContentPage, just call it on your App.xaml.cs:

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