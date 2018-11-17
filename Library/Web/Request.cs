using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Library.Web
{
    public static class Request
    {
        private static string _webUrl;

        public static event EventHandler<OnHttpPostRequestEventArgs> OnHttpPostRequestStart;
        public static event EventHandler<OnHttpPostRequestEventArgs> OnHttpPostRequestComplete;
        public static event EventHandler<OnHttpPostRequestEventArgs> OnHttpPostRequestFailed;

        private static int _identifier;

        public static void Init(string webUrl)
        {
            _webUrl = webUrl;
        }

        public static async Task RunHttpPostRequestAsync(string urlPath, string json, int specificCode = -1)
        {
            var reqInternalIdentifier = (specificCode == -1) ? _identifier++ : specificCode;
            OnHttpPostRequestStart?.Invoke(null, new OnHttpPostRequestEventArgs(reqInternalIdentifier, true, urlPath));

            var url = _webUrl + urlPath; // http://127.0.0.1 + /ruta/request
            url = (url.Contains("http")) ? url : "http://" + url;

            if (string.IsNullOrWhiteSpace(_webUrl)) throw new RequestException();

            HttpWebRequest request;
            try
            {
                request = (HttpWebRequest) WebRequest.Create(url);
            }
            catch (UriFormatException e)
            {
#if (DEBUG)
                Console.WriteLine(e + "\n" + url);
#endif
                throw new UriFormatException(url);
            }

            request.ContentType = "application/json";
            request.Accept = "application/json";
            request.Method = WebRequestMethods.Http.Post;

            using (var streamWriter = new StreamWriter(request.GetRequestStream()))
            {
                streamWriter.Write(json);
            }

            var result = string.Empty;
            try
            {
                var response = await Task.Run(() => request.GetResponse());
                using (var streamReader = new StreamReader(response.GetResponseStream() ?? throw new InvalidOperationException()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (Exception e)
            {
#if (DEBUG)
                Console.WriteLine($"{e.ToString()}\nJson: {json}");
#endif
                var data = JsonConvert.SerializeObject(false);
                OnHttpPostRequestFailed?.Invoke(null, new OnHttpPostRequestEventArgs(reqInternalIdentifier, false, data));
            }

            OnHttpPostRequestComplete?.Invoke(null, new OnHttpPostRequestEventArgs(reqInternalIdentifier, true, result));
        }
    }



    public class RequestException : Exception
    {
        public RequestException()
        {
            Console.WriteLine("-----Advertencia-----");
            Console.WriteLine("Hace falta inicializar la variable <_webUrl>. Ejemplo: 'http://127.0.0.1'");
        }

        public RequestException(string message) : base(message)
        {
            Console.WriteLine(message);
        }
    }

    public class OnHttpPostRequestEventArgs : EventArgs
    {
        public string Data { get; }
        public bool Succes { get; }
        public int Code { get; }

        public OnHttpPostRequestEventArgs(int code, bool succes, string data = null)
        {
            Code = code;
            Succes = succes;
            Data = data;
        }
    }
}