using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TravelRecordsAPI.Dto;
using TravelRecordsAPI.Models;
using TravelRecordsAPI.Models.ResponseDto;
using TravelRecordsAPI.Services;

namespace TravelRecordsAPI.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class StorageController : ControllerBase
    {
        private readonly IAzureStorage _storage;
        public StorageController(IAzureStorage storage)
        {
            _storage = storage;
        }

        [HttpGet(nameof(Get))]
        public async Task<IActionResult> Get()
        {
            // Get all files at the Azure Storage Location and return them
            List<ImageDto>? files = await _storage.ListAsync();

            // Returns an empty array if no files are present at the storage container
            return StatusCode(StatusCodes.Status200OK, files);
        }

        [HttpPost("{imageId}")]
        public async Task<IActionResult> Upload(IFormFile file,string imageId)
        {
            ImageResponseDto? response = await _storage.UploadAsync(file,imageId);

            // Check if we got an error
            if (response.Error == true)
            {
                // We got an error during upload, return an error with details to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // Return a success message to the client about successfull upload
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }


        //if profile pic ids should be: travelId=0,stageId=0,postId=0
        //if travel profile pic ids should be: stageId=0,postId=0
        [HttpPost("{userId}/{travelId}/{stageId}/{postId}")]
        public async Task<IActionResult> Upload(IFormFile file, int userId, int travelId, int stageId, int postId)
        {
            String imageId = GetImageId(userId, travelId, stageId, postId);
            ImageDto? checkFile = await _storage.DownloadAsync(imageId);
            //file with that name already exists
            if(checkFile != null)
            {
                return Conflict();
            }
            ImageResponseDto? response = await _storage.UploadAsync(file,imageId);

            // Check if we got an error
            if (response.Error == true)
            {
                // We got an error during upload, return an error with details to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // Return a success message to the client about successfull upload
                return StatusCode(StatusCodes.Status200OK, response);
            }
        }

        [HttpGet("{userId}/{travelId}/{stageId}/{postId}")]
        public async Task<IActionResult> Download(int userId, int travelId, int stageId, int postId)
        {
            String imageId = GetImageId(userId, travelId, stageId, postId);
            ImageDto? file = await _storage.DownloadAsync(imageId);

            // Check if file was found
            if (file == null)
            {
                // Was not, return error message to client
                return StatusCode(StatusCodes.Status404NotFound, $"File {imageId} doesn't exist.");
            }
            else
            {
                // File was found, return it to client
                // return File(file.Content, file.ContentType, file.Name,file.Uri);
                return StatusCode(StatusCodes.Status200OK, file);
            }
        }

        [HttpDelete("{userId}/{travelId}/{stageId}/{postId}")]
        public async Task<IActionResult> Delete(int userId, int travelId, int stageId, int postId)
        {
            ImageResponseDto response = await _storage.DeleteAsync(GetImageId(userId, travelId, stageId, postId));

            // Check if we got an error
            if (response.Error == true)
            {
                // Return an error message to the client
                return StatusCode(StatusCodes.Status500InternalServerError, response.Status);
            }
            else
            {
                // File has been successfully deleted
                return StatusCode(StatusCodes.Status200OK, response.Status);
            }
        }

        private string GetImageId(int userId, int travelId, int stageId, int postId)
        {
            return userId.ToString() + "_" + travelId.ToString() + "_" + stageId.ToString() + "_" + postId.ToString();
        }

    }
}
