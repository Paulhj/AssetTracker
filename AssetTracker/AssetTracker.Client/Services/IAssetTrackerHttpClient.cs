using System.Net.Http;
using System.Threading.Tasks;

namespace AssetTracker.Client.Services
{
    public interface IAssetTrackerHttpClient
    {
        Task<HttpClient> GetClient();
    }
}