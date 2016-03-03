using System.Net;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public string GetAllTheContents()
        {
            var client = new WebClient();

            return client.DownloadString("http://www.laterooms.com");
        }
    }
}