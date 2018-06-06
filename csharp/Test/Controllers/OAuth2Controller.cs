using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Test.Models;
using Test.Models.Google;

namespace Test.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class OAuth2Controller : ControllerBase
    {
        private static IHttpClientFactory _httpClientFactory;

        public OAuth2Controller(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [HttpPost]
        public async Task<ActionResult<TokenInfo>> Index([FromBody] SignInRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var result = await client.GetStringAsync($"https://www.googleapis.com/oauth2/v3/tokeninfo?id_token={request.Token}");

            return JsonConvert.DeserializeObject<TokenInfo>(result);
        }
    }
}
