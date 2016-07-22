using CognitiveServices.ViewModels;
using Xamarin.Forms;

namespace CognitiveServices.Views
{
    public class EmotionPage : ContentPage
    {
        public EmotionPage()
        {

            Title = "Emotion";

            BindingContext = new ComputerVisionViewModel
            {
                ImageUrl = "https://pbs.twimg.com/media/CeFShQLW4AAE6po.jpg"
                //"https://pbs.twimg.com/media/CnVGRLqWAAAnf1q.jpg:large"
                //"https://pbs.twimg.com/media/CnQSbt0XgAAjREq.jpg"
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
                Text = "Recognize Emotion (Url)",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#03A9F4"),
                FontSize = 22
            };
            extractTextFromImageUrlButton.SetBinding(Button.CommandProperty, "RecognizeEmotionFromImageUrlCommand");

            var extractTextFromImageStreamButton = new Button
            {
                Text = "Recognize Emotion (Stream)",
                TextColor = Color.White,
                BackgroundColor = Color.Fuchsia,
                FontSize = 21
            };
            extractTextFromImageStreamButton.SetBinding(Button.CommandProperty, "RecognizeEmotionFromImageStreamCommand");

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

            var emotionsDataTemplate = new DataTemplate(() =>
            {
                var angerLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                angerLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Anger",
                    BindingMode.Default,
                    null,
                    null,
                    "Anger: {0:F0}"));

                var contemptLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                contemptLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Contempt",
                    BindingMode.Default,
                    null,
                    null,
                    "Contempt: {0:F0}"));

                var disgustLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                disgustLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Disgust",
                    BindingMode.Default,
                    null,
                    null,
                    "Disgust: {0:F0}"));

                var fearLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                fearLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Fear",
                    BindingMode.Default,
                    null,
                    null,
                    "Fear: {0:F0}"));

                var happinessLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                happinessLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Happiness",
                    BindingMode.Default,
                    null,
                    null,
                    "Happiness: {0:F0}"));

                var neutralLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                neutralLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Neutral",
                    BindingMode.Default,
                    null,
                    null,
                    "Neutral: {0:F0}"));

                var sadnessLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                sadnessLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Sadness",
                    BindingMode.Default,
                    null,
                    null,
                    "Sadness: {0:F0}"));

                var surpriseLabel = new Label
                {
                    TextColor = Color.Black,
                    FontSize = 20
                };
                surpriseLabel.SetBinding(Label.TextProperty, new Binding(
                    "Scores.Surprise",
                    BindingMode.Default,
                    null,
                    null,
                    "Surprise: {0:F0}"));

                var faceStackLayout = new StackLayout
                {
                    Padding = 5,
                    Children =
                    {
                       angerLabel,
                       contemptLabel,
                       disgustLabel,
                       fearLabel,
                       happinessLabel,
                       neutralLabel,
                       sadnessLabel,
                       surpriseLabel
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
                ItemTemplate = emotionsDataTemplate
            };
            regionsListView.SetBinding(ListView.ItemsSourceProperty, "ImageResultEmotions");

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