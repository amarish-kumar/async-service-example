using System.Threading.Tasks;

using AsyncService.Services;

namespace AsyncService.Middleware
{
    public class AsyncNotNeededHere
    {
        private readonly ContentFetcher contentFetcher;

        public AsyncNotNeededHere()
        {
            contentFetcher = new ContentFetcher();
        }

        public Task<string> GetContent()
        {
            return contentFetcher.GetAllTheContents();
        }
    }
}