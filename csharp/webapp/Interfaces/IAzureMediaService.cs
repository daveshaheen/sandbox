using System.Threading.Tasks;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Rest.Azure;

namespace App.Interfaces
{
    public interface IAzureMediaService
    {
        Task<IPage<Asset>> GetAssets();

        Task<Job> TranscodeVideo(string assetName, string filePath);
    }
}
