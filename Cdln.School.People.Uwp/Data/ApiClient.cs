using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Cdln.School.People.Uwp.Messages;

namespace Cdln.School.People.Uwp.Data
{
    public class ApiClient : IDisposable, IApiClient
    {
        public ServiceStatus ServiceStatus
        {
            get => serviceStatus;

            private set
            {
                if (value != serviceStatus)
                {
                    serviceStatus = value;
                    Hub.Dispatch(new SericeStatusChangedEvent(serviceStatus));
                }
            }
        }

        // GET requests must be filtered so that API call(s) will be ignored, except the last one.
        // In theory, this filtering could keep the app UI nimble even in
        // a slow low-bandwidth network connection.
        public void Get(Guid requestId, Uri requestUri, Guid? tokenId = null, object content = null)
        {
            SignalToCopy.WaitOne();
            SignalToCopy.Reset();

            RequestId = requestId;
            RequestUri = requestUri;
            TokenId = tokenId;
            Content = content;

            SignalToCopy.Set();
            SignalToProcess.Set();
        }

        private async void ProcessGetRequests()
        {
            Guid requestId;
            Uri requestUri;
            Guid? tokenId;
            object content;

            while (!disposing)
            {
                SignalToProcess.WaitOne();
                SignalToProcess.Reset();

                try
                {
                    SignalToCopy.WaitOne();
                    SignalToCopy.Reset();

                    requestId = RequestId;
                    requestUri = RequestUri;
                    tokenId = TokenId;
                    content = Content;

                    SignalToCopy.Set();

                    var response = await GetResponseAsync(HttpMethod.Get, RequestUri, TokenId, Content);
                    Hub.Dispatch(new GetResponseReceivedEvent(requestId, response));
                }
                catch (Exception ex)
                {
                    Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
                }
            }
        }

        public async Task<HttpResponseMessage> Delete(Guid tokenId, Uri requestUri, object content = null)
        {
            var response = await GetResponseAsync(HttpMethod.Delete, requestUri, tokenId, content).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> Put(Guid tokenId, Uri requestUri, object content = null)
        {
            var response = await GetResponseAsync(HttpMethod.Put, requestUri, tokenId, content).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> Post(Guid tokenId, Uri requestUri, object content = null)
        {
            var response = await GetResponseAsync(HttpMethod.Post, requestUri, tokenId, content).ConfigureAwait(false);
            return response;
        }

        public async void GetTokenId(Guid requestId, Uri requestUri, UserCredentials credentials)
        {
            try
            {
                Guid? tokenId = null;
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
                        var newId = Guid.NewGuid();
                        Tokens.Add(newId, token);
                        tokenId = newId;
                    }
                }
                Hub.Dispatch(new TokenAcquiredEvent(requestId, tokenId));
            }
            catch (Exception ex)
            {
                Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
            }
        }

        private async Task<HttpResponseMessage> GetResponseAsync(HttpMethod method, Uri requestUri, Guid? tokenId = null, object content = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, requestUri);
                if (content != null)
                {
                    var json = JsonConvert.SerializeObject(content);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                }
                if (tokenId is Guid id)
                {
                    Tokens.TryGetValue(id, out string token);
                    request.Headers.Add("Authorization", token);
                }
                return await Client.SendAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ApiClient(IMessageHub messageHub)
        {
            Tokens = new Dictionary<Guid, string>();
            SignalToCopy = new ManualResetEvent(true);
            SignalToProcess = new ManualResetEvent(true);
            Client = new HttpClient();
            Hub = messageHub;

            GetRequestThread = new Thread(ProcessGetRequests)
            {
                Name = "InboundDataAgentThread",
                Priority = ThreadPriority.BelowNormal
            };
            GetRequestThread.Start();
        }

        public void Dispose()
        {
            disposing = true;
            SignalToCopy.Dispose();
            SignalToProcess.Dispose();
            Client.Dispose();
        }

        [ThreadStatic] private Guid? TokenId;
        [ThreadStatic] private Guid RequestId;
        [ThreadStatic] private Uri RequestUri;
        [ThreadStatic] private object Content;

        private readonly Thread GetRequestThread;

        private readonly Dictionary<Guid, string> Tokens;
        private readonly ManualResetEvent SignalToCopy;
        private readonly ManualResetEvent SignalToProcess;
        private readonly HttpClient Client;
        private readonly IMessageHub Hub;
        private bool disposing;
        private ServiceStatus serviceStatus;
    }
}
