using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using App.Interfaces;
using App.Models;
using Microsoft.Azure.Management.Media;
using Microsoft.Azure.Management.Media.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Microsoft.Rest;
using Microsoft.Rest.Azure;
using Microsoft.Rest.Azure.Authentication;
using Microsoft.WindowsAzure.Storage.Blob;

namespace App.Services
{
    public class AzureMediaService : IAzureMediaService
    {
        private readonly AzureConfig _azureConfig;
        private readonly ClientCredential _clientCredential;
        private readonly ILogger _logger;
        private AzureMediaServicesClient _azureMediaServicesClient;
        private ServiceClientCredentials _serviceClientCredentials;

        /// <summary>
        ///     Initializes a new instance of the AzureMediaService class.
        /// </summary>
        /// <param name="configuration">
        ///     Required. The configuration.
        /// </param>
        /// <param name="logger">
        ///     Required. The logger
        /// </param>
        public AzureMediaService(IConfiguration configuration, ILogger logger)
        {
            _azureConfig = new AzureConfig(configuration);
            _clientCredential = new ClientCredential(_azureConfig.ClientId, _azureConfig.ClientSecret);
            _logger = logger;

            Task.Run(async () =>
            {
                await GetServiceClientCredentials();

                _azureMediaServicesClient = new AzureMediaServicesClient(
                    _azureConfig.ManagementUri,
                    _serviceClientCredentials)
                {
                    SubscriptionId = _azureConfig.SubscriptionId
                };
            });
        }

        /// <summary>
        ///     Transcodes a video using Azure Media Services.
        /// </summary>
        /// <param name="assetName">
        ///     Required. The asset name.
        /// </param>
        /// <param name="filePath">
        ///     Required. The file path of the video to transcode.
        /// </param>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when a required parameter is whitespace or null.
        /// </exception>
        public async Task<Job> TranscodeVideo(string assetName, string filePath)
        {
            // validation
            if (string.IsNullOrWhiteSpace(assetName))
            {
                throw new ArgumentNullException(nameof(assetName));
            }

            if (string.IsNullOrWhiteSpace(filePath))
            {
                throw new ArgumentNullException(nameof(filePath));
            }

            // create asset
            var asset = await CreateAsset(assetName);

            // retrieve storage container
            var assetContainer = await GetAssetStorageContainer(assetName);

            // get storage container uri
            var containerUri = new Uri(assetContainer.AssetContainerSasUrls.First());
            _logger.LogInformation($"Retrieved storage container uri {containerUri.AbsoluteUri}.");

            // retrieve local file reference
            _logger.LogInformation($"Retrieving file reference ${filePath}");
            var blobContainer = new CloudBlobContainer(containerUri);
            var blob = blobContainer.GetBlockBlobReference(Path.GetFileName(filePath));

            // upload file
            _logger.LogInformation($"Uploading file {filePath}.");
            await blob.UploadFromFileAsync(filePath);

            // create output asset
            var outputAssetName = $"{assetName}-output";
            var outputAsset = await CreateAsset(outputAssetName);

            // get standard transform
            var transform = await GetStandardVideoTransform();

            // create job
            var jobOutputs = new JobOutput[] { new JobOutputAsset(outputAssetName) };
            var job = await _azureMediaServicesClient.Jobs.CreateAsync(
                _azureConfig.ResourceGroup,
                _azureConfig.MediaServiceAccountName,
                transform.Name,
                $"{assetName}-{Guid.NewGuid()}",
                new Job {
                    Input = new JobInput(assetName),
                    Outputs = jobOutputs
                }
            );

            return job;
        }

        public async Task<IPage<Asset>> GetAssets()
        {
            _logger.LogInformation("Retrieving Azure media service assets.");
            return await _azureMediaServicesClient.Assets
                .ListAsync(_azureConfig.ResourceGroup, _azureConfig.MediaServiceAccountName);
        }

        private async Task<Asset> CreateAsset(string assetName)
        {
            // create asset
            _logger.LogInformation($"Creating asset {assetName}.");
            var asset = await _azureMediaServicesClient.Assets
                .CreateOrUpdateAsync(
                    _azureConfig.ResourceGroup,
                    _azureConfig.MediaServiceAccountName,
                    assetName,
                    new Asset());

            return asset;
        }

        private async Task<AssetContainerSas> GetAssetStorageContainer(string assetName)
        {
            _logger.LogInformation("Retrieving asset storage container.");
            var assetContainer = await _azureMediaServicesClient.Assets
                .ListContainerSasAsync(
                    _azureConfig.ResourceGroup,
                    _azureConfig.MediaServiceAccountName,
                    assetName,
                    AssetContainerPermission.ReadWrite,
                    DateTime.UtcNow.AddHours(4).ToUniversalTime());

            return assetContainer;
        }

        private async Task GetServiceClientCredentials()
        {
            _logger.LogInformation("Retrieving service client credentials from Azure.");
            _serviceClientCredentials = await ApplicationTokenProvider
                .LoginSilentAsync(
                    _azureConfig.TenantId,
                    _clientCredential,
                    ActiveDirectoryServiceSettings.Azure);
        }

        private async Task<Transform> GetStandardVideoTransform()
        {
            var transformName = "Standard-480p";
            var transformDescription = "A H.264 video encoding at 480p, 1mbps, and 30fps with AAC audio at 192kbps and 48kHz.";
            var transform = await _azureMediaServicesClient.Transforms.GetAsync(_azureConfig.ResourceGroup, _azureConfig.MediaServiceAccountName, transformName);
            if (transform == null)
            {
                var transformOutputs = new TransformOutput[] {
                    new TransformOutput(
                        new StandardEncoderPreset(
                            codecs: new Codec[] {
                                new AacAudio(
                                    channels: 2,
                                    samplingRate: 48000,
                                    bitrate: 192000,
                                    profile: AacAudioProfile.AacLc
                                ),
                                new H264Video(
                                    keyFrameInterval: TimeSpan.FromSeconds(2),
                                    layers: new H264Layer[] {
                                        new H264Layer(
                                            width: "480",
                                            height: "854",
                                            label: "480p",
                                            bitrate: 1000000,
                                            frameRate: "30",
                                            profile: H264VideoProfile.Main
                                        )
                                    }
                                ),
                                new PngImage(
                                    start: "25%",
                                    step: "25%",
                                    range: "80%",
                                    layers: new PngLayer[]{
                                        new PngLayer(
                                            width: "50%",
                                            height: "50%"
                                        )
                                    }
                                )
                            },
                            formats: new Format[] {
                                new Mp4Format(
                                    filenamePattern: "Video-{Basename}-{Label}-{Bitrate}{Extension}"
                                ),
                                new PngFormat(
                                    filenamePattern: "Thumbnail-{Basename}-{Index}{Extension}"
                                )
                            }
                        ),
                        onError: OnErrorType.StopProcessingJob,
                        relativePriority: Priority.Normal
                    )
                };

                transform = await _azureMediaServicesClient.Transforms
                    .CreateOrUpdateAsync(
                        _azureConfig.ResourceGroup,
                        _azureConfig.MediaServiceAccountName,
                        transformName,
                        transformOutputs,
                        transformDescription);
            }

            return transform;
        }
    }
}
