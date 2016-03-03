using System.Collections.Generic;
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
            var work = new List<Task<string>>
            {
                client.GetStringAsync("http://www.laterooms.com"),
                client.GetStringAsync("http://www.laterooms.com"),
                client.GetStringAsync("http://www.laterooms.com")
            };

            await Task.WhenAll(work);

            work.ForEach(t => contents.Append(t.Result));

            return contents.ToString();
        }
    }
}