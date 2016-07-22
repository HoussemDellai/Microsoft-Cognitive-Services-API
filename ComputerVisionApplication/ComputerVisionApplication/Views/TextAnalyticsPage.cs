using CognitiveServices.Converters;
using CognitiveServices.ViewModels;
using Xamarin.Forms;

namespace CognitiveServices.Views
{
    public class TextAnalyticsPage : ContentPage
    {
        public TextAnalyticsPage()
        {

            Title = "Text Analytics";

            BindingContext = new TextAnalyticsViewModel();

            var textEntry = new Editor
            {
                Text = "Hello world",
                FontSize = 16
            };
            textEntry.SetBinding(Editor.TextProperty, "Text");

            var detectLanguageFromTextButton = new Button
            {
                Text = "Detect Language",
                TextColor = Color.White,
                BackgroundColor = Color.FromHex("#03A9F4"),
                FontSize = 24
            };
            detectLanguageFromTextButton.SetBinding(Button.CommandProperty, "DetectLanguageFromTextCommand");

            var detectSentimentFromTextButton = new Button
            {
                Text = "Detect Sentiment",
                TextColor = Color.White,
                BackgroundColor = Color.Teal,
                FontSize = 24
            };
            detectSentimentFromTextButton.SetBinding(Button.CommandProperty, "DetectSentimentCommand");

            var detectKeyPhrasesFromTextButton = new Button
            {
                Text = "Key Phrases",
                TextColor = Color.White,
                BackgroundColor = Color.Purple,
                FontSize = 24
            };
            detectKeyPhrasesFromTextButton.SetBinding(Button.CommandProperty, "DetectKeyPhrasesCommand");

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

            var detectedLanguageLabel = new Label
            {
                TextColor = Color.FromHex("#03A9F4"),
                FontSize = 20
            };
            detectedLanguageLabel.SetBinding(Label.TextProperty, new Binding(
                "LanguageResult.Documents[0].DetectedLanguages[0].Name",
                BindingMode.Default,
                null,
                null,
                "Language: {0:F0}"));

            var detectedLanguageScoreLabel = new Label
            {
                TextColor = Color.FromHex("#03A9F4"),
                FontSize = 20
            };
            detectedLanguageScoreLabel.SetBinding(Label.TextProperty, new Binding(
                "LanguageResult.Documents[0].DetectedLanguages[0].Score",
                BindingMode.Default,
                null,
                null,
                "Score: {0:F0}"));

            var detectedSentimentLabel = new Label
            {
                TextColor = Color.Teal,
                FontSize = 20
            };
            detectedSentimentLabel.SetBinding(Label.TextProperty, new Binding(
                "SentimentResult.Documents[0].Score",
                BindingMode.Default,
                null,
                null,
                "Sentiment Score: {0:F0}"));

            var detectedKeyPhrasesLabel = new Label
            {
                TextColor = Color.Purple,
                FontSize = 20
            };
            detectedKeyPhrasesLabel.SetBinding(Label.TextProperty, new Binding(
                "KeyPhrasesResult.Documents[0].KeyPhrases",
                BindingMode.Default,
                new ListOfStringToOneStringConverter(),
                null,
                "KeyPhrases: {0:F0}"));

            var stackLayout = new StackLayout
            {
                Padding = new Thickness(10, 0),
                Children =
                {
                    textEntry,
                    detectLanguageFromTextButton,
                    detectSentimentFromTextButton,
                    detectKeyPhrasesFromTextButton,
                    isBusyActivityIndicator,
                    detectedLanguageLabel,
                    detectedLanguageScoreLabel,
                    detectedSentimentLabel,
                    detectedKeyPhrasesLabel,
                    errorMessageLabel
                }
            };

            Content = new ScrollView
            {
                Content = stackLayout
            };
        }
    }
}