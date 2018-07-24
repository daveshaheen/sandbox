using System;
using System.Text;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;

namespace App.Pages
{
    public class IndexModel : PageModel
    {
        public IndexModel(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string Message { get; set; }

        public void OnGet()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"<p>Server time is {DateTime.Now}</p>");

            // dotnet user-secrets set "Secret:code" "UP, UP, DOWN, DOWN, LEFT, RIGHT, LEFT, RIGHT, B, A, START"
            sb.AppendLine($"<p>Secret code is {_configuration["Secret:code"]}</p>");

            Message = sb.ToString();
        }

        private IConfiguration _configuration { get; }
    }
}
