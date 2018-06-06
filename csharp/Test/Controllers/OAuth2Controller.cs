using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OAuth2Controller : ControllerBase
    {
        [HttpPost]
        public ActionResult<string> Index([FromBody] SignInRequest request)
        {
            return "Success!";
        }
    }
}
