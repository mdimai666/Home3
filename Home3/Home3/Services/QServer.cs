using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Home3.Services
{
    public class QServer
    {

        public HttpClient m_client;

        public Uri m_backendUrl;

        /// <summary>
        /// Вылетать если статус запросе НЕ 200
        /// </summary>
        public bool EnsureSuccessStatusCode { get; set; } = true;
        public bool IgnoreParseJsonError { get; set; } = false;

        public QServer()
        {

        }

        public QServer(string backendUrl, HttpClient client = null)
        {
            m_client = client ?? new HttpClient();
            m_backendUrl = new Uri($"{backendUrl}/");
            m_client.BaseAddress = m_backendUrl;
        }

        public QServer(HttpClient client)
        {
            m_client = client;
            m_backendUrl = client.BaseAddress;
        }

        public void HandleDeserializationError(object sender, Newtonsoft.Json.Serialization.ErrorEventArgs errorArgs)
        {
            var currentError = errorArgs.ErrorContext.Error.Message;
            errorArgs.ErrorContext.Handled = true;
        }

        //public async Task<string> Send(string url, HttpMethod method, string content = "", MediaType mediaType = MediaType.Json)
        //{

        //    string _mediaType = GetMediaString(mediaType);
        //    string _url = url;

        //    if(_)

        //    HttpContent data = new StringContent(content, Encoding.UTF8, _mediaType);

        //    HttpRequestMessage request = new HttpRequestMessage
        //    {
        //        Method = method,
        //        RequestUri = new Uri(_url),
        //        Content = data,
        //    };

        //    var response = await m_client.SendAsync(request);

        //    if (EnsureSuccessStatusCode && !response.IsSuccessStatusCode)
        //        response.EnsureSuccessStatusCode();

        //    string body = await response.Content.ReadAsStringAsync();

        //    return body;
        //}

        #region GET

        /// <summary>
        /// 
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> GET(string url)
        {

            var response = await m_client.GetAsync(url);

            if (EnsureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            // response.EnsureSuccessStatusCodeAdditional();

            string body = await response.Content.ReadAsStringAsync();

            return body;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="url"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<T> GET<T>(string url)
        {

            var body = await GET(url);

            //body = Regex.Replace(body, @"'product_features':\[\]".Replace("'","\\\n"), "{}");
            //body = body.Replace("[]", "{}");

            T responseItem = await Task.Run(() => JsonConvert.DeserializeObject<T>(body, GetDefaultConvertSetting()));

            return responseItem;
        }
        #endregion


        #region DELETE
        public async Task<string> DELETE(string url)
        {
            var response = await m_client.DeleteAsync(url);

            if (EnsureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();

            string body = await response.Content.ReadAsStringAsync();

            return body;
        }

        public async Task<HttpResponseMessage> DELETE2(string url)
        {
            HttpResponseMessage response = await m_client.DeleteAsync(url);

            return response;
        }

        public async Task<T> DELETE<T>(string url)
        {

            var body = await DELETE(url);

            //body = Regex.Replace(body, @"'product_features':\[\]".Replace("'","\\\n"), "{}");
            //body = body.Replace("[]", "{}");

            T responseItem = await Task.Run(() => JsonConvert.DeserializeObject<T>(body, GetDefaultConvertSetting()));

            return responseItem;
        }
        #endregion


        #region POST

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> Post(string path, string data = "")
        {

            bool isJson = IsJsonString(data);

            HttpContent post = new StringContent(data, Encoding.UTF8, isJson ? "application/json" : "application/x-www-form-urlencoded");


            //string body = await m_client.GetStringAsync(path);
            HttpResponseMessage response = await m_client.PostAsync(path, post);
            string body = await response.Content.ReadAsStringAsync();


            if (EnsureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();

            return body;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> Post(string path, object data)
        {

            string _data = "";

            if (data != null)
            {
                _data = JsonConvert.SerializeObject(data, GetDefaultConvertSetting());

            }

            return await Post(path, _data);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> Post(string path, Dictionary<string, string> data)
        {
            string s = "";
            if (data != null) s = string.Join(";", data.Select(x => x.Key + "=" + x.Value).ToArray());
            return await Post(path, s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<T> Post<T>(string path, object data)
        {
            string jsonText = await Post(path, data);
            T obj = JsonConvert.DeserializeObject<T>(jsonText, GetDefaultConvertSetting());
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<T> Post<T>(string path, Dictionary<string, string> data)
        {
            string jsonText = await Post(path, data);
            T obj = JsonConvert.DeserializeObject<T>(jsonText, GetDefaultConvertSetting());
            return obj;
        }
        #endregion


        #region PUT

        public async Task<string> PUT(string path, string data)
        {
            bool isJson = IsJsonString(data);

            HttpContent put = new StringContent(data, Encoding.UTF8, isJson ? "application/json" : "application/x-www-form-urlencoded");
            var response = await m_client.PutAsync(path, put);
            string body = await response.Content.ReadAsStringAsync();
            if (EnsureSuccessStatusCode && !response.IsSuccessStatusCode)
                response.EnsureSuccessStatusCode();
            return body;
        }

        public async Task<string> PUT(string path, object data)
        {
            string _data = "";
            if (data != null)
            {
                _data = JsonConvert.SerializeObject(data, GetDefaultConvertSetting());

            }
            return await PUT(path, _data);
        }

        public async Task<T> PUT<T>(string path, string data)
        {
            string jsonText = await PUT(path, data);
            T obj = JsonConvert.DeserializeObject<T>(jsonText, GetDefaultConvertSetting());
            return obj;
        }

        public async Task<T> PUT<T>(string path, object data)
        {
            string jsonText = await PUT(path, data);
            T obj = JsonConvert.DeserializeObject<T>(jsonText, GetDefaultConvertSetting());
            return obj;
        }
        #endregion


        #region PATCH
        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> Patch(string path, string data = "")
        {
            throw new Exception();
            //bool isJson = IsJsonString(data);

            //HttpContent post = new StringContent(data, Encoding.UTF8, isJson ? "application/json" : "application/x-www-form-urlencoded");


            ////string body = await m_client.GetStringAsync(path);
            //HttpResponseMessage response = await m_client.PatchAsync(path, post);
            //string body = await response.Content.ReadAsStringAsync();


            //if (EnsureSuccessStatusCode && !response.IsSuccessStatusCode)
            //    response.EnsureSuccessStatusCode();

            //return body;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> Patch(string path, object data)
        {

            string _data = "";

            if (data != null)
            {
                _data = JsonConvert.SerializeObject(data, GetDefaultConvertSetting());

            }

            return await Patch(path, _data);
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<string> Patch(string path, Dictionary<string, string> data)
        {
            string s = "";
            if (data != null) s = string.Join(";", data.Select(x => x.Key + "=" + x.Value).ToArray());
            return await Patch(path, s);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<T> Patch<T>(string path, Dictionary<string, string> data)
        {
            string jsonText = await Patch(path, data);
            T obj = JsonConvert.DeserializeObject<T>(jsonText, GetDefaultConvertSetting());
            return obj;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="path"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        /// <exception cref="HttpRequestExceptionAdditional"></exception>
        public async Task<T> Patch<T>(string path, object data)
        {
            string jsonText = await Patch(path, data);
            T obj = JsonConvert.DeserializeObject<T>(jsonText, GetDefaultConvertSetting());
            return obj;
        }
        #endregion


        #region UTILS

        public bool IsJsonString(string data)
        {
            char firstLetter = ' ';

            if (data == null) data = "";

            if (data.Trim().Length > 0)
            {
                firstLetter = data.Trim().First();
            }

            bool isJson = firstLetter == '[' || firstLetter == '{';

            return isJson;
        }



        JsonSerializerSettings GetDefaultConvertSetting()
        {
            JsonSerializerSettings sett = new JsonSerializerSettings
            {
                Formatting = Formatting.None,
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
            };

            if (IgnoreParseJsonError)
            {
                sett.Error = HandleDeserializationError;
            }

            return sett;
        }
        #endregion

        public enum MediaType
        {
            None,
            Json,
            FormData
        }

        public string GetMediaString(MediaType mediaType)
        {
            if (mediaType == MediaType.Json)
                return "application/json";
            else if (mediaType == MediaType.FormData)
                return "application/x-www-form-urlencoded";
            return "text/html";

        }
    }
}
