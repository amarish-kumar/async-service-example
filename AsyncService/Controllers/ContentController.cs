using System.Threading.Tasks;
using System.Web.Http;

using AsyncService.Services;

namespace AsyncService.Controllers
{
    public class ContentController : ApiController
    {
        public async Task<string> Get()
        {
            var fetcher = new ContentFetcher();

            return await fetcher.GetAllTheContents();
        }
    }
}
