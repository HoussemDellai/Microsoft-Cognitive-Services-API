using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ComputerVisionApplication.Models.KeyPhrases;
using ComputerVisionApplication.Models.Language;
using ComputerVisionApplication.Models.Sentiment;
using Newtonsoft.Json;

namespace ComputerVisionApplication.Services
{
    public class TextAnalyticsService
    {
        /// <summary>
        /// Doc:
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c7
        /// </summary>
        private string _detectLanguageUrl = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/languages";
        
        /// <summary>
        /// Doc:
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c6
        /// </summary>
        private string _detectKeyPhrasesUrl = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/keyPhrases";

        /// <summary>
        /// Doc: 
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c9/
        /// </summary>
        private string _detectSentimentUrl = "https://westus.api.cognitive.microsoft.com/text/analytics/v2.0/sentiment";

        private readonly string _key;

        public TextAnalyticsService(string key)
        {
            _key = key;
        }

        /// <summary>
        /// Detects the language of a given text.
        /// Documentation : 
        /// https://westus.dev.cognitive.microsoft.com/docs/services/TextAnalytics.V2.0/operations/56f30ceeeda5650db055a3c7
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public async Task<LanguageResult> DetectLanguageAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"":[{""id"": ""1"",""text"": """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectLanguageUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var detectLanguageResult = JsonConvert.DeserializeObject<LanguageResult>(json);

                    return detectLanguageResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<KeyPhrasesResult> DetectKeyPhrasesFromTextAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"": [{""language"": ""en"",""id"": ""1"",""text"" : """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectKeyPhrasesUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var keyPhrasesResult = JsonConvert.DeserializeObject<KeyPhrasesResult>(json);

                    return keyPhrasesResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<SentimentResult> DetectSentimentFromTextAsync(string text)
        {
            var httpClient = new HttpClient();

            httpClient.DefaultRequestHeaders.Add("Ocp-Apim-Subscription-Key", _key);

            var stringContent = new StringContent(@"{""documents"": [{""language"": ""en"",""id"": ""1"",""text"" : """ + text + @"""}]}");

            stringContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            try
            {
                var response = await httpClient.PostAsync(_detectSentimentUrl, stringContent);

                var json = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {

                    var sentimentResult = JsonConvert.DeserializeObject<SentimentResult>(json);

                    return sentimentResult;
                }

                throw new Exception(json);
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }
    }
}
