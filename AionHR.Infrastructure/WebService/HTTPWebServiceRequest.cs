using AionHR.Infrastructure.JSON;
using AionHR.Infrastructure.Logging;
using AionHR.Infrastructure.WebService;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AionHR.Infrastructure.WebService
{
    /// <summary>
    /// Class responsible for making the request to the webserice using http web request
    /// </summary>
    public class HTTPWebServiceRequest : IWebServiceRequest
    {

        public HTTPWebServiceRequest()
        {
            QueryStringParams = new Dictionary<string, string>();
            Headers = new Dictionary<string, string>();
            Resolver = new CustomResolver();
        }

        /// <summary>
        /// Url of the request
        /// </summary>
        public string URL { get; set; }

        public string Body { get; set; }

        /// <summary>
        /// the method type
        /// </summary>
        public string MethodType { get; set; }

        /// <summary>
        /// the query string params
        /// </summary>
        public Dictionary<string, string> QueryStringParams { get; set; }

        /// <summary>
        /// the additional headers
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

        /// <summary>
        /// the resolver userd to resolve json 
        /// </summary>
        public CustomResolver Resolver { get; set; }

        /// <summary>
        /// the requested url
        /// </summary>
        public string RequestUrl
        {
            get
            {
                string res = URL + "?";
                foreach (var item in QueryStringParams)
                {
                    res = res + item.Key + "=" + item.Value + "&";
                }
                res = res.Remove(res.Length - 1);
                return res;
            }
        }

        /// <summary>
        /// Build the header data from the header list of key values
        /// </summary>
        /// <param name="req"></param>
        protected void BuildHeaders(WebRequest req)
        {
            foreach (var item in Headers)
            {
                req.Headers.Add(item.Key, item.Value);
            }
        }


        /// <summary>
        /// Build an error message for logging
        /// </summary>
        /// <returns></returns>
        protected string BuildLogMessage()
        {
            string message = string.Empty;
            string AccountID = string.Empty;
            string UserID = string.Empty;
            Headers.TryGetValue("AccountId", out AccountID);
            Headers.TryGetValue("UserId", out UserID);
            message += " : | " + AccountID + " | " + UserID;
            return message;
        }

        /// <summary>
        /// Reponsible for making the http request to the webservice
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public T GetAsync<T>()

        {
            try
            {
                WebRequest req = HttpWebRequest.Create(RequestUrl);
                req.Method = MethodType;


                if (Headers.Count > 0)
                    BuildHeaders(req);


                var r = req.GetResponse();
                Stream s = r.GetResponseStream();
                StreamReader reader = new StreamReader(s, true);
                string x = reader.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                if (Resolver != null)
                {
                    settings.ContractResolver = Resolver;
                }

                T response = JsonConvert.DeserializeObject<T>(x, settings);
                return response;
            }
            catch (Exception ex)
            {

                string exception = BuildLogMessage() + " : " + ex.Message;
                LoggingFactory.GetLogger().Log(exception);
                return default(T);
            }
        }

        public PostWebServiceResponse PostAsync<T>(T item)
        {
            PostWebServiceResponse response = new PostWebServiceResponse();
            try
            {
                WebRequest req = HttpWebRequest.Create(RequestUrl);
                req.Method = MethodType;


                if (Headers.Count > 0)
                    BuildHeaders(req);
                Body = JsonConvert.SerializeObject(item);
                Stream stream = req.GetRequestStream();
                StreamWriter wr = new StreamWriter(stream);
                wr.Write(Body);
                wr.Flush();
                wr.Close();
                stream.Close();
                //req.ContentLength = stream.Length;

                var r = req.GetResponse();
                Stream s = r.GetResponseStream();
                StreamReader reader = new StreamReader(s, true);
                string x = reader.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                if (Resolver != null)
                {
                    settings.ContractResolver = Resolver;
                }


                response = JsonConvert.DeserializeObject<PostWebServiceResponse>(x, settings);
                return response;
            }
            catch (Exception ex)
            {

                string exception = BuildLogMessage() + " : " + ex.Message;
                LoggingFactory.GetLogger().Log(exception);
                response.statusId = "0";
                return response;
            }
        }
        public PostWebServiceResponse PostAsyncFormData<T>(T item)
        {
            PostWebServiceResponse response = new PostWebServiceResponse();
            try
            {
                //  HttpClient client = new HttpClient();

                //Defining the unique boundary
                string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
                WebRequest req = HttpWebRequest.Create(RequestUrl);
                if (Headers.Count > 0)
                    BuildHeaders(req);
                req.Method = MethodType;//Post
                req.ContentType = "multipart/form-data; boundary=" + boundary;
                Stream stream = req.GetRequestStream();


                //Body need to be extended for each part of the request 
                // Add header for JSON part


                // Body += "Content-Disposition: form-data; name='entity'\r\n";//entity is relative to the object we r sending
                //Body += "Content-Type: application/json\r\n\r\n";//defining the content type for this part of request
                // Add document object data in JSON
                //Body += JsonConvert.SerializeObject(item);
                Body += "\r\n--" + boundary + "\r\n"; ;


                Body += string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n", "record");

                string jsonString = JsonConvert.SerializeObject(item);
                Body += jsonString;
                Body += "\r\n--" + boundary + "\r\n"; ;

                //Now we need to add the header for the binary part inside the body
                byte[] data = System.Text.Encoding.UTF8.GetBytes(Body);
                stream.Write(data, 0, data.Length);

                // Add binary file to request


                // Finalizing by adding the footer of the request or what we call trailer
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                stream.Write(trailer, 0, trailer.Length);
                stream.Close();


                // Do the post and get the response.

                var r = req.GetResponse();
                Stream s = r.GetResponseStream();
                StreamReader reader = new StreamReader(s, true);
                string x = reader.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                if (Resolver != null)
                {
                    settings.ContractResolver = Resolver;
                }


                response = JsonConvert.DeserializeObject<PostWebServiceResponse>(x, settings);
                return response;
            }
            catch (Exception ex)
            {

                string exception = BuildLogMessage() + " : " + ex.Message;
                LoggingFactory.GetLogger().Log(exception);
                response.statusId = "0";
                response.description = ex.Message;
                return response;
            }

        }
        public PostWebServiceResponse PostAsyncWithBinary<T>(T item, string fileName, byte[] buffer)
        {
            PostWebServiceResponse response = new PostWebServiceResponse();
            try
            {
                //  HttpClient client = new HttpClient();

                //Defining the unique boundary
                string boundary = "----WebKitFormBoundary" + DateTime.Now.Ticks.ToString("x");
                WebRequest req = HttpWebRequest.Create(RequestUrl);
                if (Headers.Count > 0)
                    BuildHeaders(req);
                req.Method = MethodType;//Post
                req.ContentType = "multipart/form-data; boundary=" + boundary;
                Stream stream = req.GetRequestStream();


                //Body need to be extended for each part of the request 
                // Add header for JSON part


                // Body += "Content-Disposition: form-data; name='entity'\r\n";//entity is relative to the object we r sending
                //Body += "Content-Type: application/json\r\n\r\n";//defining the content type for this part of request
                // Add document object data in JSON
                //Body += JsonConvert.SerializeObject(item);
                Body += "\r\n--" + boundary + "\r\n";


               // Body += "Content-Disposition: form-data; name='record'\r\n";
              //  Body += "Content-Type: application/json\r\n\r\n";

                Body += string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n", "record");

                string jsonString = JsonConvert.SerializeObject(item);
                Body += jsonString;

                //Finalized the json part






                if (buffer != null)
                {
                    //Now we need to add the header for the binary part inside the body
                    Body += "\r\n--" + boundary + "\r\n";
                    Body += "Content-Disposition: form-data; name=\"picture\"; filename=\"" + fileName + "\"\r\n";
                    Body += "Content-Type: application/octet-stream\r\n\r\n";
                 

                   // Body += "Content-Type: image/png\r\n\r\n";
                }


                //Now we need  to write the headers to the request 
                // Add header data to request
                byte[] data = System.Text.Encoding.UTF8.GetBytes(Body);
                stream.Write(data, 0, data.Length);

                // Add binary file to request

                if (buffer != null)
                {
                    stream.Write(buffer, 0, buffer.Length);
                }
                // Finalizing by adding the footer of the request or what we call trailer
                byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n");
                stream.Write(trailer, 0, trailer.Length);
                stream.Close();


                // Do the post and get the response.

                var r = req.GetResponse();
                Stream s = r.GetResponseStream();
                StreamReader reader = new StreamReader(s, true);
                string x = reader.ReadToEnd();

                var settings = new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore };
                if (Resolver != null)
                {
                    settings.ContractResolver = Resolver;
                }


                response = JsonConvert.DeserializeObject<PostWebServiceResponse>(x, settings);
                return response;
            }
            catch (Exception ex)
            {

                string exception = BuildLogMessage() + " : " + ex.Message;
                LoggingFactory.GetLogger().Log(exception);
                response.statusId = "0";
                return response;
            }
        }
    }
}
