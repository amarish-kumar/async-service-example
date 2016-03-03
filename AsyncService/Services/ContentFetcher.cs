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
            var work = new List<Task>
            {
                Task.Delay(5000).ContinueWith(_ => contents.Add("FINISHED 1...")),
                Task.Delay(5000).ContinueWith(_ => contents.Add("FINISHED 2...")),
                Task.Delay(5000).ContinueWith(_ => contents.Add("FINISHED 3..."))
            };

            await Task.WhenAll(work);

            return string.Concat(contents);
        }
    }
}