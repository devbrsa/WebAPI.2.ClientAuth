using System.Web.Http;

namespace WebAPI._2.ClientAuth.Controllers
{
    [Authorize]
    public class TestController : ApiController
    {
        public string Get()
        {
            return "Hello";
        }
    }
}