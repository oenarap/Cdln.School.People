using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Apps.Communication.Core;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Events;

namespace Cdln.School.People.Uwp.Data
{
    public class ApiClient : IDisposable
    {
        public async void Get(Guid requestId, Uri requestUri, object content = null)
        {
            try
            {
                var response = await GetResponseAsync(HttpMethod.Get, requestId, requestUri, content).ConfigureAwait(false);
                Hub.Dispatch(new GetResponseReceivedEvent(requestId, response));
            }
            catch (Exception ex)
            {
                Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
            }
        }

        public async void Delete(Guid requestId, Uri requestUri, object content = null)
        {
            try
            {
                var response = await GetResponseAsync(HttpMethod.Delete, requestId, requestUri, content).ConfigureAwait(false);
                Hub.Dispatch(new DeleteResponseReceivedEvent(requestId, response));
            }
            catch (Exception ex)
            {
                Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
            }
        }

        public async void Put(Guid requestId, Uri requestUri, object content = null)
        {
            try
            {
                var response = await GetResponseAsync(HttpMethod.Put, requestId, requestUri, content).ConfigureAwait(false);
                Hub.Dispatch(new PutResponseReceivedEvent(requestId, response));
            }
            catch (Exception ex)
            {
                Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
            }
        }

        public async void Post(Guid requestId, Uri requestUri, object content = null)
        {
            try
            {
                var response = await GetResponseAsync(HttpMethod.Post, requestId, requestUri, content).ConfigureAwait(false);
                Hub.Dispatch(new PostResponseReceivedEvent(requestId, response));
            }
            catch (Exception ex)
            {
                Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
            }
        }

        public async Task<Guid?> GetToken(Uri requestUri, UserCredentials credentials)
        {
            try
            {
                var content = JsonConvert.SerializeObject(credentials);
                var message = new HttpRequestMessage(HttpMethod.Post, requestUri)
                {
                    Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json")
                };
                var response = await Client.SendAsync(message);
                if (response.IsSuccessStatusCode)
                {
                    if (await response.Content.ReadAsStringAsync() is string token)
                    {
                        var id = Guid.NewGuid();
                        Tokens.Add(id, token);
                        return id;
                    }
                }
                return null;
            }
            catch { return null; }
        }

        private async Task<HttpResponseMessage> GetResponseAsync(HttpMethod method, Guid requestId, Uri requestUri, object content = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, requestUri);
                if (content != null)
                {
                    var json = JsonConvert.SerializeObject(content);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                }
                Tokens.TryGetValue(requestId, out string token);
                request.Headers.Add("Authorization", token);
                return await Client.SendAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ApiClient(IEventHub hub)
        {
            Tokens = new Dictionary<Guid, string>();
            Client = new HttpClient();
            Hub = hub;
        }

        public void Dispose() => Client.Dispose();

        private readonly Dictionary<Guid, string> Tokens;
        private readonly HttpClient Client;
        private readonly IEventHub Hub;
    }
}
