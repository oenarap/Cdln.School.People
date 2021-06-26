using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Cdln.School.People.Uwp
{
    public interface IApiClient
    {
        Task<HttpResponseMessage> Delete(Guid tokenId, Uri requestUri, object content = null);
        void Get(Guid requestId, Uri requestUri, Guid? tokenId = null, object content = null);
        void GetTokenId(Guid requestId, Uri requestUri, UserCredentials credentials);
        Task<HttpResponseMessage> Post(Guid tokenId, Uri requestUri, object content = null);
        Task<HttpResponseMessage> Put(Guid tokenId, Uri requestUri, object content = null);
    }
}
