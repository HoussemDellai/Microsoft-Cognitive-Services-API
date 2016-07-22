using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;
using CognitiveServices.Models;
using CognitiveServices.Models.Image;
using CognitiveServices.Models.Ocr;
using CognitiveServices.Services;
using ComputerVisionApplication.Models;
using ComputerVisionApplication.Models.Emotion;
using ComputerVisionApplication.Services;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Xamarin.Forms;

namespace CognitiveServices.ViewModels
{
    public class ComputerVisionViewModel : INotifyPropertyChanged
    {

        private ImageResult _imageResult;
        private OcrResult _imageResultOcr;
        private List<EmotionResult> _imageResultEmotions;
        /// <summary>
        /// Get a subscription key from:
        /// https://www.microsoft.com/cognitive-services/en-us/subscriptions
        /// The following API Key may stop working at anytime, so get your own!
        /// </summary>
        private const string ComputerVisionApiKey = "d5fdc78fad5b4cf98fce5df15146426d";
        private readonly ComputerVisionService _computerVisionService = new ComputerVisionService(ComputerVisionApiKey);
        private const string EmotionApiKey = "3ea63bdad6ec41659989eaa0d260a73b";
        private readonly EmotionService _emotionService = new EmotionService(EmotionApiKey);
        private string _imageUrl = "https://pbs.twimg.com/media/CivvCZ_UgAIB80Z.jpg";
        //"https://pbs.twimg.com/media/Cm9jk7bWcAAf_2d.jpg";
        //"https://pbs.twimg.com/media/CiRu_nsWkAA0cU4.jpg";
        private Stream _imageStream;
        private string _errorMessage;
        private bool _isBusy;

        public ImageResult ImageResult
        {
            get { return _imageResult; }
            set
            {
                _imageResult = value;
                OnPropertyChanged();
            }
        }

        public OcrResult OcrResult
        {
            get { return _imageResultOcr; }
            set
            {
                _imageResultOcr = value;
                OnPropertyChanged();
            }
        }

        public List<EmotionResult> ImageResultEmotions
        {
            get
            {
                return _imageResultEmotions;
            }
            set
            {
                _imageResultEmotions = value;
                OnPropertyChanged();
            }
        }

        public string ImageUrl
        {
            get { return _imageUrl; }
            set
            {
                _imageUrl = value;
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

        public Command TakePhotoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    // Don't forget to install nuget package Xam.Plugin.Media 
                    // on all Solution projects
                    await CrossMedia.Current.Initialize();

                    var mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions());

                    _imageStream = mediaFile?.GetStream();

                    ImageUrl = mediaFile?.Path;
                });
            }
        }

        public Command PickPhotoCommand
        {
            get
            {
                return new Command(async () =>
                {
                    // Don't forget to install nuget package Xam.Plugin.Media 
                    // on all Solution projects
                    await CrossMedia.Current.Initialize();

                    var mediaFile = await CrossMedia.Current.PickPhotoAsync();

                    _imageStream = mediaFile?.GetStream();

                    ImageUrl = mediaFile?.Path;
                });
            }
        }

        public Command AnalyseImageUrlCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    try
                    {
                        ImageResult = null;
                        ErrorMessage = string.Empty;

                        ImageResult = await _computerVisionService.AnalyseImageUrlAsync(_imageUrl);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command AnalyseImageStreamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    try
                    {
                        ImageResult = null;
                        ErrorMessage = string.Empty;

                        ImageResult = await _computerVisionService.AnalyseImageStreamAsync(_imageStream);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command ExtractTextFromImageUrlCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    try
                    {
                        ImageResult = null;
                        ErrorMessage = string.Empty;

                        OcrResult = await _computerVisionService.ExtractTextFromImageUrlAsync(_imageUrl);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command ExtractTextFromImageStreamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    try
                    {
                        ImageResult = null;
                        ErrorMessage = string.Empty;

                        OcrResult = await _computerVisionService.ExtractTextFromImageStreamAsync(_imageStream);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command RecognizeEmotionFromImageUrlCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    try
                    {
                        ImageResult = null;
                        ErrorMessage = string.Empty;

                        ImageResultEmotions = 
                        await _emotionService.RecognizeEmotionsFromImageUrlAsync(_imageUrl);
                    }
                    catch (Exception exception)
                    {
                        ErrorMessage = exception.Message;
                    }

                    IsBusy = false;
                });
            }
        }

        public Command RecognizeEmotionFromImageStreamCommand
        {
            get
            {
                return new Command(async () =>
                {
                    IsBusy = true;

                    try
                    {
                        ImageResult = null;
                        ErrorMessage = string.Empty;

                        ImageResultEmotions = await _emotionService.RecognizeEmotionsFromImageStreamAsync(_imageStream);
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
