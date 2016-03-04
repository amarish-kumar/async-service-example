using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebGrease.Css.Extensions;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public async Task<string> GetAllTheContents()
        {
            // The asynchronous work in this method is CPU-bound
            // but the same principles apply for IO-bound tasks.

            var contents = new ConcurrentBag<string>();
            var client = new HttpClient();
            var url = "https://httpbin.org/delay/1";
            var work = new List<Task>
            {
                client.GetStringAsync(url).ContinueWith(_ => contents.Add("FINISHED 1...")),
                client.GetStringAsync(url).ContinueWith(_ => contents.Add("FINISHED 2...")),
                client.GetStringAsync(url).ContinueWith(_ => contents.Add("FINISHED 3..."))
            };

            await Task.WhenAll(work);

            return string.Concat(contents);
        }
    }
}