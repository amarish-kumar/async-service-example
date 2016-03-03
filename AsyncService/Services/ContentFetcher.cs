using System.Net;
using System.Threading.Tasks;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public async Task<string> GetAllTheContentsAsync()
        {
            var client = new WebClient();

            return await client.DownloadStringTaskAsync("http://www.laterooms.com");
        }
    }
}