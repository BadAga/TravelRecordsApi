using TravelRecordsAPI.Dto;
namespace TravelRecordsAPI.Models.ResponseDto
{
    public class ImageResponseDto
    {
        public string? Status { get; set; }
        public bool Error { get; set; }
        public ImageDto Image { get;set; }

        public ImageResponseDto()
        {
            Image = new ImageDto();
        }

    }
}
