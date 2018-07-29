using System;
using Microsoft.Extensions.Configuration;

namespace App.Models
{
    public class AzureConfig
    {
        private readonly IConfiguration _configuration;

        public AzureConfig(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public string ClientId => _configuration["Azure:clientId"];

        public string ClientSecret => _configuration["Azure:clientSecret"];

        public Uri ManagementUri => new Uri(_configuration["Azure:managementEndpoint"]);

        public string MediaServiceAccountName => _configuration["Azure:mediaServiceAccountName"];

        public string ResourceGroup => _configuration["Azure:resourceGroup"];

        public string SubscriptionId => _configuration["Azure:subscriptionId"];

        public string TenantId => _configuration["Azure:tenantId"];
    }
}
