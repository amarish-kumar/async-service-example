using System.Net;
using System.Text;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public string GetAllTheContents()
        {
            var client = new WebClient();

            var contents = new StringBuilder();
            contents.Append(client.DownloadString("http://www.laterooms.com"));
            contents.Append(client.DownloadString("http://www.laterooms.com"));
            contents.Append(client.DownloadString("http://www.laterooms.com"));

            return contents.ToString();
        }
    }
}