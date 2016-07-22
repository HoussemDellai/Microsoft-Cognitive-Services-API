using System;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CognitiveServices.Models;
using CognitiveServices.Models.Image;
using CognitiveServices.Models.Ocr;
using ComputerVisionApplication.Models;
using Newtonsoft.Json;

namespace CognitiveServices.Services
{
    /// <summary>
    /// Client for Computer Vision API (Microsoft Cognitive Services).
    /// </summary>
    public class ComputerVisionService
    {

        /// <summary>
        /// Get a subscription key from:
        /// https://www.microsoft.com/cognitive-services/en-us/subscriptions
        /// </summary>
        private readonly string _key;// = "d5fdc78fad5b4cf98fce5df15146426d";

        /// <summary>
        /// Documentation for the API: https://www.microsoft.com/cognitive-services/en-us/computer-vision-api
        /// </summary>
        private readonly string _analyseImageUri = "https://api.projectoxford.ai/vision/v1.0/analyze?" + "visualFeatures=Description,Categories,Tags,Faces,ImageType,Color,Adult&details=Celebrities";

        private readonly string _extractTextUri = "https://api.projectoxford.ai/vision/v1.0/ocr?" + "language=unk&detectOrientation=true";

        /// <summary>
        /// Get a subscription key from:
        /// https://www.microsoft.com/cognitive-services/en-us/subscriptions
        /// </summary>
        /// <param name="key">subscription key: required to access the API</param>
        public ComputerVisionService(string key)
        {
            _key = key;
        }

        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content. 
        /// </summary>
        /// <param name="stream">The image to be uploaded.</param>
        /// <returns></returns>
        public async Task<ImageResult> AnalyseImageStreamAsync(Stream stream)
        {

            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var response = await httpClient.PostAsync(_analyseImageUri, streamContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResult = JsonConvert.DeserializeObject<ImageResult>(json);

                    return imageResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }

        /// <summary>
        /// This operation extracts a rich set of visual features based on the image content. 
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <returns></returns>
        public async Task<ImageResult> AnalyseImageUrlAsync(string imageUrl)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""url"":""" + imageUrl + @"""}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_analyseImageUri, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResult = JsonConvert.DeserializeObject<ImageResult>(json);

                    return imageResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }

        /// <summary>
        /// Optical Character Recognition (OCR) detects text in an image 
        /// and extracts the recognized characters into a machine-usable character stream.
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <returns></returns>
        public async Task<OcrResult> ExtractTextFromImageUrlAsync(string imageUrl)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""url"":""" + imageUrl + @"""}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_extractTextUri, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResultOcr = JsonConvert.DeserializeObject<OcrResult>(json);

                    return imageResultOcr;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }

        /// <summary>
        /// Optical Character Recognition (OCR) detects text in an image 
        /// and extracts the recognized characters into a machine-usable character stream.
        /// </summary>
        /// <param name="imageUrl">The image url.</param>
        /// <returns></returns>
        public async Task<OcrResult> ExtractTextFromImageStreamAsync(Stream stream)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var streamContent = new StreamContent(stream);

            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");

            try
            {
                var response = await httpClient.PostAsync(_extractTextUri, streamContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var imageResultOcr = JsonConvert.DeserializeObject<OcrResult>(json);

                    return imageResultOcr;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return null;
        }
    }
}
