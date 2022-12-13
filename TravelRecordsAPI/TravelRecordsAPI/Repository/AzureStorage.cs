﻿using Azure.Storage.Blobs.Models;
using Azure.Storage.Blobs;
using TravelRecordsAPI.Dto;
using TravelRecordsAPI.Models.ResponseDto;
using TravelRecordsAPI.Services;
using Azure;

//more info: https://blog.christian-schou.dk/how-to-use-azure-blob-storage-with-asp-net-core/

namespace TravelRecordsAPI.Repository
{
    public class AzureStorage : IAzureStorage
    {
        #region Dependency Injection / Constructor

        private readonly string _storageConnectionString;
        private readonly string _storageContainerName;
        private readonly ILogger<AzureStorage> _logger;

        public AzureStorage(IConfiguration configuration, ILogger<AzureStorage> logger)
        {
            _storageConnectionString = configuration.GetValue<string>("BlobConnectionString");
            _storageContainerName = configuration.GetValue<string>("BlobContainerName");
            _logger = logger;
        }

        public async Task<ImageResponseDto> DeleteAsync(string imageId)
        {
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            BlobClient file = client.GetBlobClient(imageId+".jpg");

            try
            {
                // Delete the file
                await file.DeleteAsync();
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // File did not exist, log to console and return new response to requesting method
                _logger.LogError($"File {imageId + ".jpg"} was not found.");
                return new ImageResponseDto { Error = true, Status = $"File with name {imageId + ".jpg"} not found.", };
            }

            // Return a new BlobResponseDto to the requesting method
            return new ImageResponseDto { Error = false, Status = $"File: {imageId + ".jpg"} has been successfully deleted." };

        }

        public async Task<ImageDto> DownloadAsync(string imageId)
        {
            // Get a reference to a container named in appsettings.json
            BlobContainerClient client = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            try
            {
                // Get a reference to the blob uploaded earlier from the API in the container from configuration settings
                BlobClient file = client.GetBlobClient(imageId + ".jpg");

                // Check if the file exists in the container
                if (await file.ExistsAsync())
                {
                    var data = await file.OpenReadAsync();
                    Stream blobContent = data;

                    // Download the file details async
                    var content = await file.DownloadContentAsync();

                    // Add data to variables in order to return a BlobDto
                    string name = imageId + ".jpg";
                    string contentType = content.Value.Details.ContentType;

                    // Create new BlobDto with blob data from variables
                    return new ImageDto { Content = blobContent, Name = name, ContentType = contentType };
                }
            }
            catch (RequestFailedException ex)
                when (ex.ErrorCode == BlobErrorCode.BlobNotFound)
            {
                // Log error to console
                _logger.LogError($"File {imageId + ".jpg"} was not found.");
            }

            // File does not exist, return null and handle that in requesting method
            return null;
        }

        public async Task<List<ImageDto>> ListAsync()
        {
            // Get a reference to a container named in appsettings.json
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);

            // Create a new list object for 
            List<ImageDto> files = new List<ImageDto>();

            await foreach (BlobItem file in container.GetBlobsAsync())
            {
                // Add each file retrieved from the storage container to the files list by creating a BlobDto object
                string uri = container.Uri.ToString();
                var name = file.Name;
                var fullUri = $"{uri}/{name}";

                files.Add(new ImageDto
                {
                    Uri = fullUri,
                    Name = name,
                    ContentType = file.Properties.ContentType
                });
            }

            // Return all files to the requesting method
            return files;
        }

        public async Task<ImageResponseDto> UploadAsync(IFormFile blob,string imageId)
        {
            // Create new upload response object that we can return to the requesting method
            ImageResponseDto response = new();
            // Get a reference to a container named in appsettings.json and then create it
            BlobContainerClient container = new BlobContainerClient(_storageConnectionString, _storageContainerName);
            //await container.CreateAsync();
            try
            {          
                if(blob!=null)
                {
                    Guid guid=Guid.NewGuid();
                    string extension=Path.GetExtension(blob.FileName);
                    
                    string uploadpath=Path.Combine(Path.GetDirectoryName(blob.FileName),imageId+extension);
                    var stream=new FileStream(uploadpath, FileMode.Create);

                    blob.CopyTo(stream);
                    FormFile blobRenamed = new FormFile(stream, 0, stream.Length, null, Path.GetFileName(stream.Name));
                    blob = blobRenamed;
                }
                // Get a reference to the blob just uploaded from the API in a container from configuration settings
                BlobClient client = container.GetBlobClient(blob.FileName);

                // Open a stream for the file we want to upload
                await using (Stream? data = blob.OpenReadStream())
                {
                    // Upload the file async
                    await client.UploadAsync(data);
                }

                // Everything is OK and file got uploaded
                response.Status = $"File {blob.FileName} Uploaded Successfully";
                response.Error = false;
                response.Image.Uri = client.Uri.AbsoluteUri;
                response.Image.Name = client.Name;

            }
            // If the file already exists, we catch the exception and do not upload it
            catch (RequestFailedException ex)
               when (ex.ErrorCode == BlobErrorCode.BlobAlreadyExists)
            {
                _logger.LogError($"File with name {blob.FileName} already exists in container. Set another name to store the file in the container: '{_storageContainerName}.'");
                response.Status = $"File with name {blob.FileName} already exists. Please use another name to store your file.";
                response.Error = true;
                return response;
            }
            // If we get an unexpected error, we catch it here and return the error message
            catch (RequestFailedException ex)
            {
                // Log error to console and create a new response we can return to the requesting method
                _logger.LogError($"Unhandled Exception. ID: {ex.StackTrace} - Message: {ex.Message}");
                response.Status = $"Unexpected error: {ex.StackTrace}. Check log with StackTrace ID.";
                response.Error = true;
                return response;
            }

            // Return the BlobUploadResponse object
            return response;
        }

        #endregion

    }
}
