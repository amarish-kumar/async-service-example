using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace AsyncService.Services
{
    public class ContentFetcher
    {
        public async Task<string> GetAllTheContents()
        {
            var contents = new ConcurrentBag<string>();

            await Task.Delay(5000).ContinueWith(_ => contents.Add("FINISHED 1..."));
            await Task.Delay(5000).ContinueWith(_ => contents.Add("FINISHED 2..."));
            await Task.Delay(5000).ContinueWith(_ => contents.Add("FINISHED 3..."));

            return string.Concat(contents);
        }
    }
}