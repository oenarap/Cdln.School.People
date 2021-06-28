using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cdln.School.People.Uwp
{
    public interface IApiClient
    {
        ServiceStatus ServiceStatus { get; }

        Task<HttpResponseMessage> Delete(Uri requestUri, object content = null);
        void Get(Guid requestId, Guid requestorId, Uri requestUri, object content = null);
        Task<HttpResponseMessage> Post(Guid id, Uri requestUri, object content = null);
        Task<HttpResponseMessage> Put(Uri requestUri, object content = null);
    }
}