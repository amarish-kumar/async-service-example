using System.Collections.Concurrent;
using System.Threading;
using System.Threading.Tasks;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public string GetAllTheContents()
        {
            var contents = new ConcurrentBag<string>();
            var task1 = Task.Run(() => Thread.Sleep(5000)).ContinueWith(_ => contents.Add("FINISHED 1..."));
            var task2 = Task.Run(() => Thread.Sleep(5000)).ContinueWith(_ => contents.Add("FINISHED 2..."));
            var task3 = Task.Run(() => Thread.Sleep(5000)).ContinueWith(_ => contents.Add("FINISHED 3..."));

            Task.WaitAll(task1, task2, task3);

            return string.Concat(contents);
        }
    }
}