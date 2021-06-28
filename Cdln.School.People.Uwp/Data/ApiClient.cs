using System;
using System.Net.Http;
using Newtonsoft.Json;
using System.Threading;
using System.Threading.Tasks;
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
        public void Get(Guid requestId, Guid requestorId, Uri requestUri, object content = null)
        {
            SignalToCopy.WaitOne();
            SignalToCopy.Reset();

            RequestorId = requestorId;
            RequestId = requestId;
            RequestUri = requestUri;
            Content = content;

            SignalToCopy.Set();
            SignalToProcess.Set();
        }

        private async void ProcessGetRequests()
        {
            Guid requestId;
            Uri requestUri;
            Guid requestorId;
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
                    requestorId = RequestorId;
                    content = Content;

                    SignalToCopy.Set();

                    var response = await GetResponseAsync(HttpMethod.Get, requestUri, content);
                    Hub.Dispatch(new GetResponseReceivedEvent(requestId, requestorId, response));
                }
                catch (Exception ex)
                {
                    Hub.Dispatch(new RequestErrorEvent(requestId, ex.Message));
                }
            }
        }

        public async Task<HttpResponseMessage> Delete(Uri requestUri, object content = null)
        {
            var response = await GetResponseAsync(HttpMethod.Delete, requestUri, content).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> Put(Uri requestUri, object content = null)
        {
            var response = await GetResponseAsync(HttpMethod.Put, requestUri, content).ConfigureAwait(false);
            return response;
        }

        public async Task<HttpResponseMessage> Post(Guid id, Uri requestUri, object content = null)
        {
            var response = await GetResponseAsync(HttpMethod.Post, requestUri, content).ConfigureAwait(false);
            return response;
        }

        private async Task<string> GetToken(Uri requestUri, UserCredentials credentials)
        {
            try
            {
                var content = JsonConvert.SerializeObject(credentials);
                var message = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    RequestUri = requestUri,
                    Content = new StringContent(content, System.Text.Encoding.UTF8, "application/json")
                };
                var response = await Client.SendAsync(message);
                if (response.IsSuccessStatusCode) { return await response.Content.ReadAsStringAsync(); }
                return null;
            }
            catch { return null; }
        }

        private async Task<HttpResponseMessage> GetResponseAsync(HttpMethod method, Uri requestUri, object content = null)
        {
            try
            {
                var request = new HttpRequestMessage(method, requestUri);

                if (content != null)
                {
                    var json = JsonConvert.SerializeObject(content);
                    request.Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json");
                }

                if (token == null) { token = await GetToken(new Uri("https://localhost:44397/token"), Credentials); }

                request.Headers.Add("Authorization", token);
                return await Client.SendAsync(request).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public ApiClient(IMessageHub hub, UserCredentials credentials)
        {
            Credentials = credentials;
            Hub = hub;

            SignalToCopy = new ManualResetEvent(true);
            SignalToProcess = new ManualResetEvent(true);
            Client = new HttpClient();
            GetRequestThread = new Thread(ProcessGetRequests)
            {
                Name = $"THREAD_{ nameof(ApiClient) }",
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

        [ThreadStatic] private object Content;
        [ThreadStatic] private Guid RequestorId;
        [ThreadStatic] private Guid RequestId;
        [ThreadStatic] private Uri RequestUri;

        private readonly Thread GetRequestThread;

        private readonly UserCredentials Credentials;
        private readonly ManualResetEvent SignalToCopy;
        private readonly ManualResetEvent SignalToProcess;
        private readonly HttpClient Client;
        private readonly IMessageHub Hub;

        private string token;
        private bool disposing;
        private ServiceStatus serviceStatus;
    }
}
