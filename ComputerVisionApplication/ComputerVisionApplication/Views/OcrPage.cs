using CognitiveServices.Converters;
using CognitiveServices.ViewModels;
using Xamarin.Forms;

namespace CognitiveServices.Views
{
    public class OcrPage : ContentPage
    {
        public OcrPage()
        {

            Title = "OCR";

            BindingContext = new ComputerVisionViewModel
            {
                ImageUrl = "https://pbs.twimg.com/media/CnQSbt0XgAAjREq.jpg"
                //"https://scontent-lhr3-1.xx.fbcdn.net/t31.0-8/13416828_1110682725621456_2065685633287031902_o.jpg"
                //"https://pbs.twimg.com/media/CmzB22aXgAAySZ7.jpg"
            };

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

            var extractTextFromImageUrlButton = new Button
            {
                Text = "Extract Text (Url)",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#03A9F4"),
                FontSize = 22
            };
            extractTextFromImageUrlButton.SetBinding(Button.CommandProperty, "ExtractTextFromImageUrlCommand");

            var extractTextFromImageStreamButton = new Button
            {
                Text = "Extract Text (Stream)",
                TextColor = Color.White,
                BackgroundColor = Color.Teal,
                FontSize = 22
            };
            extractTextFromImageStreamButton.SetBinding(Button.CommandProperty, "ExtractTextFromImageStreamCommand");

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

            var languageLabel = new Label
            {
                TextColor = Color.Maroon,
                FontSize = 20
            };
            languageLabel.SetBinding(Label.TextProperty, new Binding(
                "OcrResult.Language",
                BindingMode.Default,
                null,
                null,
                "Language: {0:F0}"));

            var textAngleLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            textAngleLabel.SetBinding(Label.TextProperty, new Binding(
                "OcrResult.TextAngle",
                BindingMode.Default,
                null,
                null,
                "TextAngle: {0:F0}"));

            var orientationLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            orientationLabel.SetBinding(Label.TextProperty, new Binding(
                "OcrResult.Orientation",
                BindingMode.Default,
                null,
                null,
                "Orientation: {0:F0}"));

            //var tagsLabel = new Label
            //{
            //    TextColor = Color.Green,
            //    FontSize = 20
            //};
            //tagsLabel.SetBinding(Label.TextProperty, new Binding(
            //    "ImageResult.Description.Tags",
            //    BindingMode.Default,
            //    new ListOfStringToOneStringConverter(),
            //    null,
            //    "TAGS: {0:F0}"));

            var regionDataTemplate = new DataTemplate(() =>
            {
                var boundingBoxLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                boundingBoxLabel.SetBinding(Label.TextProperty, new Binding(
                    "BoundingBox",
                    BindingMode.Default,
                    null,
                    null,
                    "BoundingBox: {0:F0}"));

                var linesLabel = new Label
                {
                    TextColor = Color.Gray,
                    FontSize = 20
                };
                linesLabel.SetBinding(Label.TextProperty, new Binding(
                    "Lines",
                    BindingMode.Default,
                    new ListOfLinesToOneStringConverter(),
                    null,
                    "Lines: {0:F0}"));

                var faceStackLayout = new StackLayout
                {
                    Padding = 5,
                    Children =
                    {
                        boundingBoxLabel,
                        linesLabel
                    }
                };

                return new ViewCell
                {
                    View = faceStackLayout
                };
            });

            var regionsListView = new ListView
            {
                HasUnevenRows = true,
                ItemTemplate = regionDataTemplate
            };
            regionsListView.SetBinding(ListView.ItemsSourceProperty, "OcrResult.Regions");

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
                    extractTextFromImageUrlButton,
                    extractTextFromImageStreamButton,
                    isBusyActivityIndicator,
                    errorMessageLabel,
                    languageLabel,
                    textAngleLabel,
                    orientationLabel,
                    regionsListView
                }
            };

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
    }
}