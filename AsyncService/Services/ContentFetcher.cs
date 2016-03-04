using System.Collections.Concurrent;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public string GetAllTheContents()
        {
            var client = new HttpClient();
            var url = "https://httpbin.org/delay/1";
            var contents = new ConcurrentBag<string>();
            var task1 = Task.Run(() => client.GetStringAsync(url)).ContinueWith(_ => contents.Add("FINISHED 1..."));
            var task2 = Task.Run(() => client.GetStringAsync(url)).ContinueWith(_ => contents.Add("FINISHED 2..."));
            var task3 = Task.Run(() => client.GetStringAsync(url)).ContinueWith(_ => contents.Add("FINISHED 3..."));

            Task.WaitAll(task1, task2, task3);

            return string.Concat(contents);
        }
    }
}