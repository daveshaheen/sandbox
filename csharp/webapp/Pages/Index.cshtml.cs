using System;
using System.Text;
using System.Threading.Tasks;
using App.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace App.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IAzureMediaService _azureMediaService;
        private readonly IConfiguration _configuration;

        public IndexModel(IAzureMediaService azureMediaService, IConfiguration configuration)
        {
            _azureMediaService = azureMediaService;
            _configuration = configuration;
        }

        public string Message { get; set; }

        public async Task OnGet()
        {
            var assets = await _azureMediaService.GetAssets();

            var sb = new StringBuilder();
            sb.AppendLine($"<p>Server time is {DateTime.Now}</p>");

            foreach(var asset in assets)
            {
                sb.AppendLine($"<p>{asset.Name}</p>");
            }

            Message = sb.ToString();
        }
    }
}
