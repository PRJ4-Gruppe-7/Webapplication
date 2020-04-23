using System.Net.Http;
using System.Threading.Tasks;

namespace web_app_backend2_API.Interfaces
{
    public interface IHttpHandler
    {
        HttpResponseMessage Get(string url, HttpClient _client);
        Task<HttpResponseMessage> GetAsync(string url, HttpClient _client);
    }
}