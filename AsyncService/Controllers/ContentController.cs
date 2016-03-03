using System.Web.Http;

using AsyncService.Services;

namespace AsyncService.Controllers
{
    public class ContentController : ApiController
    {
        public string Get()
        {
            var fetcher = new ContentFetcher();

            return fetcher.GetAllTheContents();
        }
    }
}
