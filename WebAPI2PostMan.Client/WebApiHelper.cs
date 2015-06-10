using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace WebAPI2PostMan.Client
{
    /// <summary>
    ///     WebAPI帮助类
    /// </summary>
    public class WebApiHelper
    {
        public static T1 CallPostWebApi<T1, T2>(string url, T2 request, string serviceUrl, int? timeOut = 10)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, timeOut.HasValue ? timeOut.Value : 10);
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content = new ObjectContent<T2>(request, jsonFormatter);
                var taskRes = client.PostAsync(url, content);
                var response = taskRes.Result;
                T1 result = response.Content.ReadAsAsync<T1>().Result;
                return result;
            }
        }

        public static T1 CallGetWebApi<T1>(string url, string serviceUrl, int? timeOut = 10)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, timeOut.HasValue ? timeOut.Value : 10);
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var taskRes = client.GetAsync(url);
                var response = taskRes.Result;
                T1 result = response.Content.ReadAsAsync<T1>().Result;
                return result;
            }
        }

        public static List<TResponse> CallWebApiBatch<TRequest, TResponse>(HttpMethod method, string endpoint, List<TRequest> batchRequestModels, string url, string serviceUrl, int? timeOut = 10)
        {
            var result = new List<TResponse>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var multiContents = new MultipartContent("mixed");
                if (method == HttpMethod.Post)
                {
                    foreach (var batchRequestModel in batchRequestModels)
                    {
                        multiContents.Add(new HttpMessageContent(new HttpRequestMessage(HttpMethod.Post, serviceUrl + url)
                        {
                            Content = new ObjectContent<TRequest>(batchRequestModel, new JsonMediaTypeFormatter())
                        }));
                    }
                }
                if (method == HttpMethod.Get)
                {
                    foreach (var batchRequestModel in batchRequestModels)
                    {
                        multiContents.Add(new HttpMessageContent(new HttpRequestMessage(HttpMethod.Get, serviceUrl + url + batchRequestModel)));
                    }
                }
                var batchRequest = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = multiContents
                };
                var batchResponse = client.SendAsync(batchRequest).Result;
                var streamProvider = batchResponse.Content.ReadAsMultipartAsync().Result;
                foreach (var content in streamProvider.Contents)
                {
                    var responseMessage = content.ReadAsHttpResponseMessageAsync().Result;
                    var response = responseMessage.Content.ReadAsAsync<TResponse>(new[] { new JsonMediaTypeFormatter() }).Result;
                    result.Add(response);
                }
                return result;
            }
        }

        public static async Task<T1> CallPostWebApiAsync<T1, T2>(string url, T2 request, string serviceUrl, int? timeOut = 10)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, timeOut.HasValue ? timeOut.Value : 10);
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                MediaTypeFormatter jsonFormatter = new JsonMediaTypeFormatter();
                HttpContent content = new ObjectContent<T2>(request, jsonFormatter);
                var taskRes = client.PostAsync(url, content);
                var response = await taskRes;
                T1 result = await response.Content.ReadAsAsync<T1>();
                return result;
            }
        }

        public static async Task<T1> CallGetWebApiAsync<T1>(string url, string serviceUrl, int? timeOut = 10)
        {
            using (var client = new HttpClient())
            {
                client.Timeout = new TimeSpan(0, 0, timeOut.HasValue ? timeOut.Value : 10);
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));
                var taskRes = client.GetAsync(url);
                var response = await taskRes;
                T1 result = await response.Content.ReadAsAsync<T1>();
                return result;
            }
        }

        public static async Task<List<TResponse>> CallWebApiBatchAsync<TRequest, TResponse>(HttpMethod method, string endpoint, List<TRequest> batchRequestModels, string url,string serviceUrl, int? timeOut = 10)
        {
            var result = new List<TResponse>();
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(serviceUrl);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var multiContents = new MultipartContent("mixed");
                if (method == HttpMethod.Post)
                {
                    foreach (var batchRequestModel in batchRequestModels)
                    {
                        multiContents.Add(
                            new HttpMessageContent(new HttpRequestMessage(HttpMethod.Post,serviceUrl + url)
                            {
                                Content = new ObjectContent<TRequest>(batchRequestModel, new JsonMediaTypeFormatter())
                            }));
                    }
                }
                if (method == HttpMethod.Get)
                {
                    foreach (var batchRequestModel in batchRequestModels)
                    {
                        multiContents.Add(new HttpMessageContent(new HttpRequestMessage(HttpMethod.Get,serviceUrl + url + batchRequestModel)));
                    }
                }
                var batchRequest = new HttpRequestMessage(HttpMethod.Post, endpoint)
                {
                    Content = multiContents
                };
                var batchResponse = await client.SendAsync(batchRequest);
                var streamProvider = await batchResponse.Content.ReadAsMultipartAsync();
                foreach (var content in streamProvider.Contents)
                {
                    var responseMessage = await content.ReadAsHttpResponseMessageAsync();
                    var response =
                        responseMessage.Content.ReadAsAsync<TResponse>(new[] {new JsonMediaTypeFormatter()}).Result;
                    result.Add(response);
                }
                return result;
            }
        }
    }
}
