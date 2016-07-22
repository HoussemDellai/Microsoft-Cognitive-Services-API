using CognitiveServices.Converters;
using CognitiveServices.ViewModels;
using Xamarin.Forms;

namespace CognitiveServices.Views
{
    public class ComputerVisionPage : ContentPage
    {
        public ComputerVisionPage()
        {

            Title = "Analyse";

            BindingContext = new ComputerVisionViewModel();

            var takePhotoButton = new Button
            {
                Text = "Take Photo",
                TextColor = Color.White,
                BackgroundColor = Color.Navy,
                FontSize = 24
            };
            takePhotoButton.SetBinding(Button.CommandProperty, "TakePhotoCommand");

            var pickPhotoButton = new Button
            {
                Text = "Pick Photo",
                TextColor = Color.White,
                BackgroundColor = Color.Olive,
                FontSize = 24
            };
            pickPhotoButton.SetBinding(Button.CommandProperty, "PickPhotoCommand");

            var imageUrlEntry = new Entry();
            imageUrlEntry.SetBinding(Entry.TextProperty, "ImageUrl");

            var image = new Image
            {
                HeightRequest = 200
            };
            image.SetBinding(Image.SourceProperty, "ImageUrl");

            var analyseImageUrlButton = new Button
            {
                Text = "Analyse Image Url",
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                FontSize = 24
            };
            analyseImageUrlButton.SetBinding(Button.CommandProperty, "AnalyseImageUrlCommand");

            var analyseImageStreamButton = new Button
            {
                Text = "Analyse Image Stream",
                TextColor = Color.White,
                BackgroundColor = Color.Green,
                FontSize = 24
            };
            analyseImageStreamButton.SetBinding(Button.CommandProperty, "AnalyseImageStreamCommand");

            var extractTextFromImageUrlButton = new Button
            {
                Text = "Extract Text from Image Url",
                TextColor = Color.White,
                BackgroundColor = Color.Silver,
                FontSize = 24
            };
            extractTextFromImageUrlButton.SetBinding(Button.CommandProperty, "ExtractTextFromImageUrlCommand");


            var isBusyActivityIndicator = new ActivityIndicator();
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsRunningProperty, "IsBusy");
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsEnabledProperty, "IsBusy");
            isBusyActivityIndicator.SetBinding(ActivityIndicator.IsVisibleProperty, "IsBusy");

            var errorMessageLabel = new Label
            {
                TextColor = Color.Red,
                FontSize = 20
            };
            errorMessageLabel.SetBinding(Label.TextProperty, "ErrorMessage");

            var captionsLabel = new Label
            {
                TextColor = Color.Maroon,
                FontSize = 20
            };
            captionsLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Description.Captions[0].Text",
                BindingMode.Default,
                null,
                null,
                "CAPTIONS: {0:F0}"));

            var isAdultContentLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            isAdultContentLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Adult.IsAdultContent",
                BindingMode.Default,
                null,
                null,
                "IsAdultContent: {0:F0}"));

            var isRacyContentLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            isRacyContentLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Adult.IsRacyContent",
                BindingMode.Default,
                null,
                null,
                "IsRacyContent: {0:F0}"));

            var tagsLabel = new Label
            {
                TextColor = Color.Green,
                FontSize = 20
            };
            tagsLabel.SetBinding(Label.TextProperty, new Binding(
                "ImageResult.Description.Tags",
                BindingMode.Default,
                new ListOfStringToOneStringConverter(),
                null,
                "TAGS: {0:F0}"));

            var faceDataTemplate = new DataTemplate(() =>
            {
                var ageLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                ageLabel.SetBinding(Label.TextProperty, new Binding(
                    "Age",
                    BindingMode.Default,
                    null,
                    null,
                    "Age: {0:F0}"));

                var genderLabel = new Label
                {
                    TextColor = Color.Gray,
                    FontSize = 20
                };
                genderLabel.SetBinding(Label.TextProperty, new Binding(
                    "Gender",
                    BindingMode.Default,
                    null,
                    null,
                    "Gender: {0:F0}"));

                var faceStackLayout = new StackLayout
                {
                    Padding = 5,
                    Children =
                    {
                        ageLabel,
                        genderLabel
                    }
                };

                return new ViewCell
                {
                    View = faceStackLayout
                };
            });

            var facesListView = new ListView()
            {
                HasUnevenRows = true,
                ItemTemplate = faceDataTemplate
            };
            facesListView.SetBinding(ListView.ItemsSourceProperty, "ImageResult.Faces");

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(10, 0),
                Children =
                {
                    new StackLayout
                    {
                        Orientation = StackOrientation.Horizontal,
                        Children =
                        {
                            takePhotoButton,
                            pickPhotoButton
                        }
                    },
                    imageUrlEntry,
                    image,
                    analyseImageUrlButton,
                    analyseImageStreamButton,
                    isBusyActivityIndicator,
                    errorMessageLabel,
                    captionsLabel,
                    isAdultContentLabel,
                    isRacyContentLabel,
                    tagsLabel,
                    facesListView
                }
            };

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
    }
}

///// if you prefer to work with XAML code, 
///// use the following which is similar to the above code.

//<?xml version="1.0" encoding="utf-8" ?>
//<ContentPage xmlns = "http://xamarin.com/schemas/2014/forms"
//             xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml"
//             xmlns:local="clr-namespace:CognitiveServices"
//             xmlns:viewModels="clr-namespace:CognitiveServices.ViewModels;assembly=CognitiveServices"
//             xmlns:converters="clr-namespace:CognitiveServices.Converters;assembly=CognitiveServices"
//             x:Class="CognitiveServices.ComputerVisionXamlPage"
//             BackgroundColor="White">

//  <ContentPage.BindingContext>
//    <viewModels:ComputerVisionViewModel/>
//  </ContentPage.BindingContext>

//  <ContentPage.Resources>
//    <ResourceDictionary>
//      <converters:ListOfStringToOneStringConverter x:Key="ListOfStringToOneStringConverter"/>
//    </ResourceDictionary>
//  </ContentPage.Resources>

//  <ScrollView>
//    <StackLayout Orientation = "Vertical"
//                 Padding="10,0">

//      <StackLayout Orientation = "Horizontal" >
//        < Button Text="Take Photo"
//                TextColor="White"
//                BackgroundColor="Navy"
//                FontSize="24"
//                Command="{Binding TakePhotoCommand}"/>
//        <Button Text = "Pick Photo"
//                TextColor="White"
//                BackgroundColor="Olive"
//                FontSize="24"
//                Command="{Binding PickPhotoCommand}"/>
//      </StackLayout>

//      <Entry Text = "{Binding ImageUrl}" />

//      < Image Source="{Binding ImageUrl}"
//             HeightRequest="200"/>

//      <Button Text = "Analyse Image Url"
//              TextColor="White"
//              BackgroundColor="Purple"
//              FontSize="24"
//              Command="{Binding AnalyseImageUrlCommand}"/>

//      <Button Text = "Analyse Image Stream"
//              TextColor="White"
//              BackgroundColor="Green"
//              FontSize="24"
//              Command="{Binding AnalyseImageStreamCommand}"/>

//      <ActivityIndicator IsRunning = "{Binding IsBusy}"
//                         IsEnabled="{Binding IsBusy}"
//                         IsVisible="{Binding IsBusy}"/>

//      <Label Text = "{Binding ErrorMessage}"
//             TextColor="Red"
//             FontSize="20" />

//      <Label Text = "{Binding ImageResult.Description.Captions[0].Text,
//                    StringFormat='CAPTIONS: {0:F0}'}"
//             TextColor="Maroon"
//             FontSize="20"/>

//      <Label Text = "{Binding ImageResult.Adult.IsAdultContent,
//                    StringFormat='IsAdultContent: {0:F0}'}"
//             TextColor="Teal"
//             FontSize="20"/>

//      <Label Text = "{Binding ImageResult.Adult.IsRacyContent,
//                    StringFormat='IsRacyContent: {0:F0}'}"
//             TextColor="Green"
//             FontSize="20"/>

//      <Label Text = "{Binding ImageResult.Description.Tags, 
//                    Converter={StaticResource ListOfStringToOneStringConverter},
//                    StringFormat='TAGS: {0:F0}'}"
//             TextColor="Navy"
//             FontSize="20"/>

//      <ListView ItemsSource = "{Binding ImageResult.Faces}"
//                HasUnevenRows="True">
//        <ListView.ItemTemplate>
//          <DataTemplate>
//            <ViewCell>
//              <StackLayout Padding = "5" >
//                < Label Text="{Binding Age, StringFormat='Age: {0:F0}'}"
//                       TextColor="Black"
//                       FontSize="20"/>
//                <Label Text = "{Binding Gender, StringFormat='Gender: {0:F0}'}"
//                       TextColor="Gray"
//                       FontSize="20"/>
//              </StackLayout>
//            </ViewCell>
//          </DataTemplate>
//        </ListView.ItemTemplate>
//      </ListView>

//    </StackLayout>
//  </ScrollView>
//</ContentPage>