using System.Threading.Tasks;
using System.Web.Http;
using AsyncService.Middleware;
using AsyncService.Services;

namespace AsyncService.Controllers
{
    public class ContentController : ApiController
    {
        public async Task<string> Get()
        {
            var middleware = new AsyncNotNeededHere();

            return await middleware.GetContent();
        }
    }
}
