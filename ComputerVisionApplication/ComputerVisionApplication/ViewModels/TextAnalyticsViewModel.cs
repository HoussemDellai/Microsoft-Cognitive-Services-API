using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ComputerVisionApplication.Models.KeyPhrases;
using ComputerVisionApplication.Models.Language;
using ComputerVisionApplication.Models.Sentiment;
using ComputerVisionApplication.Services;
using Xamarin.Forms;

namespace CognitiveServices.ViewModels
{
    public class TextAnalyticsViewModel : INotifyPropertyChanged
    {


        private string _errorMessage;
        private bool _isBusy;
        private string _text = "The Text Analytics API is a suite of text analytics web services built with Azure Machine Learning. The API can be used to analyze unstructured text for tasks such as sentiment analysis, key phrase extraction and language detection. No training data is needed to use this API; just bring your text data. ";
        private LanguageResult _languageResult;
        private KeyPhrasesResult _keyPhrasesResult;
        private SentimentResult _sentimentResult;
        private const string Key = "aa4e799583524873b8bb6020c5b02797";

        public string Text
        {
            get { return _text; }
            set
            {
                _text = value;
                OnPropertyChanged();
            }
        }

        public LanguageResult LanguageResult
        {
            get { return _languageResult; }
            set
            {
                _languageResult = value;
                OnPropertyChanged();
            }
        }

        public KeyPhrasesResult KeyPhrasesResult
        {
            get { return _keyPhrasesResult; }
            set
            {
                _keyPhrasesResult = value;
                OnPropertyChanged();
            }
        }

        public SentimentResult SentimentResult
        {
            get { return _sentimentResult; }
            set
            {
                _sentimentResult = value;
                OnPropertyChanged();
            }
        }


        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                OnPropertyChanged();
            }
        }

        public bool IsBusy
        {
            get { return _isBusy; }
            set
            {
                _isBusy = value;
                OnPropertyChanged();
            }
        }

        public Command DetectLanguageFromTextCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    var textAnalyticsService = new TextAnalyticsService(Key);

                    try
                    {
                        LanguageResult = await textAnalyticsService.DetectLanguageAsync(_text);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command DetectKeyPhrasesCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    var textAnalyticsService = new TextAnalyticsService(Key);

                    try
                    {
                        KeyPhrasesResult = await textAnalyticsService.DetectKeyPhrasesFromTextAsync(_text);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command DetectSentimentCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    var textAnalyticsService = new TextAnalyticsService(Key);

                    try
                    {
                        SentimentResult = await textAnalyticsService.DetectSentimentFromTextAsync(_text);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
