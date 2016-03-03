using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public async Task<string> GetAllTheContents()
        {
            var client = new HttpClient();

            var contents = new StringBuilder();
            contents.Append(await client.GetStringAsync("http://www.laterooms.com"));
            contents.Append(await client.GetStringAsync("http://www.laterooms.com"));
            contents.Append(await client.GetStringAsync("http://www.laterooms.com"));

            return contents.ToString();
        }
    }
}