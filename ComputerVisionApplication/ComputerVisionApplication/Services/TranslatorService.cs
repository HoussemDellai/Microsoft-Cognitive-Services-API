//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Net.Http;
//using System.Text;
//using System.Threading.Tasks;
//using HttpWebRequest = System.Net.HttpWebRequest;
//using WebResponse = System.Net.WebResponse;

//namespace ComputerVisionApplication.Services
//{

//    public class AdmAccessToken
//    {
//        public string access_token { get; set; }
//        public string token_type { get; set; }
//        public string expires_in { get; set; }
//        public string scope { get; set; }
//    }

//    public class TranslatorService
//    {

//        /// <summary>
//        /// Create a new app here:
//        /// https://datamarket.azure.com/developer/applications/register
//        /// </summary>
//        const string ClientId = "XamarinDemoApp";
//        const string ClientSecret = "bHXySfJI83ZbBjU2QHpzCx8iwtTh3LEGHWHpPaaQE8o=";
        
//            string languageCode = "en"; //set english as the default
//            string[] friendlyName = { " " }; //Array for passing languages codes to get friendly name
//            List<string> speakLanguages; //List of langauges for speech
//            static string headerValue; //used for auth in http header
//            Dictionary<string, string> languageCodesAndTitles = new Dictionary<string, string>(); //create dictionary to receive the language codes and friendly names
        
//            private void AccessToken()
//            {

//                string clientID = "XamarinDemoApp";
//                string clientSecret = "ADD-bHXySfJI83ZbBjU2QHpzCx8iwtTh3LEGHWHpPaaQE8o=";

//                String strTranslatorAccessURI = "https://datamarket.accesscontrol.windows.net/v2/OAuth2-13";
//                String strRequestDetails = string.Format("grant_type=client_credentials&client_id={0}&client_secret={1}&scope=http://api.microsofttranslator.com", HttpUtility.UrlEncode(clientID), HttpUtility.UrlEncode(clientSecret));

//                System.Net.WebRequest webRequest = System.Net.WebRequest.Create(strTranslatorAccessURI);
//                webRequest.ContentType = "application/x-www-form-urlencoded";
//                webRequest.Method = "POST";

//                byte[] bytes = Encoding.ASCII.GetBytes(strRequestDetails);
//                webRequest.ContentLength = bytes.Length;

//                using (Stream outputStream = webRequest.GetRequestStream())
//                {
//                    outputStream.Write(bytes, 0, bytes.Length);
//                }

//                WebResponse webResponse = webRequest.GetResponse();

//                System.Runtime.Serialization.Json.DataContractJsonSerializer serializer = new System.Runtime.Serialization.Json.DataContractJsonSerializer(typeof(AdmAccessToken));

//                //Get deserialized object from Stream
//                AdmAccessToken token = (AdmAccessToken)serializer.ReadObject(webResponse.GetResponseStream());

//                headerValue = "Bearer " + token.access_token; //create the string for the http header
//            }

//            //*****BUTTON TO START TRANSLATION PROCESS
//            private void translateButton_Click(object sender, EventArgs e)
//            {
//                AccessToken(); //get an access token for each translation because they expire after 10 minutes.

//                languageCodesAndTitles.TryGetValue(LanguageComboBox.Text, out languageCode); //get the language code from the dictionary based on the selection in the combobox

//                if (languageCode == null)  //in case no language is selected.
//                {
//                    languageCode = "en";

//                }

//                //*****BEGIN CODE TO MAKE THE CALL TO THE TRANSLATOR SERVICE TO PERFORM A TRANSLATION FROM THE USER TEXT ENTERED INCLUDES A CALL TO A SPEECH METHOD*****

//                string txtToTranslate = textToTranslate.Text;

//                string uri = string.Format("http://api.microsofttranslator.com/v2/Http.svc/Translate?text=" + System.Web.HttpUtility.UrlEncode(txtToTranslate) + "&to={0}", languageCode);

//                System.Net.WebRequest translationWebRequest = System.Net.WebRequest.Create(uri);

//                translationWebRequest.Headers.Add("Authorization", headerValue); //header value is the "Bearer plus the token from ADM

//                WebResponse response = null;

//                response = translationWebRequest.GetResponse();

//                Stream stream = response.GetResponseStream();

//                Encoding encode = Encoding.GetEncoding("utf-8");

//                StreamReader translatedStream = new StreamReader(stream, encode);

//                System.Xml.XmlDocument xTranslation = new System.Xml.XmlDocument();

//                xTranslation.LoadXml(translatedStream.ReadToEnd());

//                translatedTextLabel.Content = "Translation -->   " + xTranslation.InnerText;

//                if (speakLanguages.Contains(languageCode) && txtToTranslate != "")
//                {
//                    //call the method to speak the translated text
//                    SpeakMethod(headerValue, xTranslation.InnerText, languageCode);
//                }
//            }
        
//            //*****CODE TO GET TRANSLATABLE LANGAUGE CODES*****
//            private void GetLanguagesForTranslate()
//            {

//                string uri = "http://api.microsofttranslator.com/v2/Http.svc/GetLanguagesForTranslate";
//                System.Net.WebRequest WebRequest = WebRequest.Create(uri);
//                WebRequest.Headers.Add("Authorization", headerValue);

//                WebResponse response = null;

//                try
//                {
//                    response = WebRequest.GetResponse();
//                    using (Stream stream = response.GetResponseStream())
//                    {

//                        System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(List<string>));
//                        List<string> languagesForTranslate = (List<string>)dcs.ReadObject(stream);
//                        friendlyName = languagesForTranslate.ToArray(); //put the list of language codes into an array to pass to the method to get the friendly name.

//                    }
//                }
//                catch
//                {
//                    throw;
//                }
//                finally
//                {
//                    if (response != null)
//                    {
//                        response.Close();
//                        response = null;
//                    }
//                }
//            }


//            //*****CODE TO GET TRANSLATABLE LANGAUGE FRIENDLY NAMES FROM THE TWO CHARACTER CODES*****
//            private void GetLanguageNamesMethod(string authToken, string[] languageCodes)
//            {
//                string uri = "http://api.microsofttranslator.com/v2/Http.svc/GetLanguageNames?locale=en";
//                // create the request
//                HttpWebRequest request = (HttpWebRequest)System.Net.WebRequest.Create(uri);
//                request.Headers.Add("Authorization", headerValue);
//                request.ContentType = "text/xml";
//                request.Method = "POST";
//                System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String[]"));
//                using (System.IO.Stream stream = request.GetRequestStream())
//                {
//                    dcs.WriteObject(stream, languageCodes);
//                }
//                WebResponse response = null;
//                try
//                {
//                    response = request.GetResponse();

//                    using (Stream stream = response.GetResponseStream())
//                    {
//                        string[] languageNames = (string[])dcs.ReadObject(stream);

//                        for (int i = 0; i < languageNames.Length; i++)
//                        {

//                            languageCodesAndTitles.Add(languageNames[i], languageCodes[i]); //load the dictionary for the combo box

//                        }
//                    }
//                }
//                catch
//                {
//                    throw;
//                }
//                finally
//                {
//                    if (response != null)
//                    {
//                        response.Close();
//                        response = null;
//                    }
//                }
//            }

//            private void GetLanguagesForSpeakMethod(string authToken)
//            {

//                string uri = "http://api.microsofttranslator.com/v2/Http.svc/GetLanguagesForSpeak";
//                HttpWebRequest httpWebRequest = (HttpWebRequest)System.Net.WebRequest.Create(uri);
//                httpWebRequest.Headers.Add("Authorization", authToken);
//                WebResponse response = null;
//                try
//                {
//                    response = httpWebRequest.GetResponse();
//                    using (Stream stream = response.GetResponseStream())
//                    {

//                        System.Runtime.Serialization.DataContractSerializer dcs = new System.Runtime.Serialization.DataContractSerializer(typeof(List<string>));
//                        speakLanguages = (List<string>)dcs.ReadObject(stream);

//                    }
//                }
//                catch
//                {
//                    throw;
//                }
//                finally
//                {
//                    if (response != null)
//                    {
//                        response.Close();
//                        response = null;
//                    }
//                }
//            }
//        }

//        //public async Task<string> TranslateAsync(string text)
//        //{

//        //    var httpClient = new HttpClient();

//        //    var json = await httpClient.PostAsync("https://datamarket.accesscontrol.windows.net/v2/OAuth2-13",);

//        //    return "";
//        //}
//    }
//}
