using System.Net.Http;
using System.Net.Http.Headers;
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
        public async Task<ActionResult<string>> Index([FromBody] SignInRequest request)
        {
            var response = "";
            var tokenInfoURL = "https://www.googleapis.com/oauth2/v3/tokeninfo";

            using (var client = _httpClientFactory.CreateClient())
            {
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var idTokenResult = await client.GetStringAsync($"{tokenInfoURL}?id_token={request.id_token}");
                var tokenInfo = JsonConvert.DeserializeObject<TokenInfo>(idTokenResult);

                response = $"[{idTokenResult}]";

                if (!string.IsNullOrWhiteSpace(request.access_token))
                {
                    var accessTokenResult = await client.GetStringAsync($"{tokenInfoURL}?access_token={request.access_token}");

                    response = $"[{idTokenResult}, {accessTokenResult}]";

                    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {request.access_token}");
                    var groupInfoResult = await client.GetAsync($"https://www.googleapis.com/admin/directory/v1/groups?userKey={tokenInfo.sub}");

                    response = $"[{idTokenResult}, {accessTokenResult}, {groupInfoResult.Content.ReadAsStringAsync().Result}]";
                }

                return response;
            }
        }
    }
}
