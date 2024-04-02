using NetflixCloneWeb.Dtos;

namespace NetflixCloneWeb.Repositories
{
    public interface IVideoRepository
    {
        List<VideoDto> GetAllVideos(string? searchString);
        VideoDto GetVideo(int? id);
        VideoDto CreatePostVideo(VideoDto video, IFormFile fileImage, IFormFile fileVideo);
        VideoDto UpdateVideo(int id, VideoDto video, IFormFile fileImage, IFormFile fileVideo);
        bool DeleteVideo(int? id);
    }
}
