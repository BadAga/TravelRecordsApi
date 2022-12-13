using TravelRecordsAPI.Dto;
using TravelRecordsAPI.Models;
using TravelRecordsAPI.Models.ResponseDto;

namespace TravelRecordsAPI.Services
{
    public interface IAzureStorage
    {
        /// <summary>
        /// This method uploads a file submitted with the request
        /// </summary>
        /// <param name="file">File for upload</param>
        /// /// <param name="imageId">imageId</param>
        /// <returns>Blob with status</returns>
        Task<ImageResponseDto> UploadAsync(IFormFile file, string imageId);

        /// <summary>
        /// This method downloads a file with the specified filename
        /// </summary>
        /// <param name="imageId">Filename</param>
        /// <returns>Blob</returns>
        Task<ImageDto> DownloadAsync(string imageId);

        /// <summary>
        /// This method deletes a file with the specified filename
        /// </summary>
        /// <param name="imageId">Filename</param>
        /// <returns>Blob with status</returns>
        Task<ImageResponseDto> DeleteAsync(string imageId);

        /// <summary>
        /// This method returns a list of all files located in the container
        /// </summary>
        /// <returns>Blobs in a list</returns>
        Task<List<ImageDto>> ListAsync();
    }
}
