using CloudinaryDotNet.Actions;
using NetflixCloneWeb.Dtos;
using NetflixCloneWeb.Services;

namespace NetflixCloneWeb.Repositories
{
    public class VideoRepository : IVideoRepository
    {
        HttpClient _httpClient = new HttpClient() { BaseAddress = new Uri("https://localhost:7015/api/") };
        private readonly IUserRepository _userRepository;
        private readonly IUploadService _uploadService;

        public VideoRepository(IUserRepository userRepository, IUploadService uploadService)
        {
            _userRepository = userRepository;
            _uploadService = uploadService;
        }

        public VideoDto CreatePostVideo(VideoDto video, IFormFile fileImage, IFormFile fileVideo)
        {
            video.ContentVideo = _uploadService.UploadVideo(fileVideo);
            video.ThumbVideo = _uploadService.UploadImage(fileImage);

            var response = _httpClient.PostAsJsonAsync<VideoDto>(_httpClient.BaseAddress + "Videos/", video);
            response.Wait();

            if (response.Status == TaskStatus.RanToCompletion)
            {
                return null;
            }

            return video;

        }

        public bool DeleteVideo(int? id)
        {
            HttpResponseMessage responseMessage = _httpClient.DeleteAsync(_httpClient.BaseAddress + "Videos/" + id).Result;

            if (!responseMessage.IsSuccessStatusCode)
            {
                return false;
            }

            return true;
        }

        public List<VideoDto> GetAllVideos(string? searchString)
        {
            List<VideoDto> videos = new List<VideoDto>();

            try
            {
                var response = _httpClient.GetFromJsonAsync<List<VideoDto>>(_httpClient.BaseAddress + "Videos");
                response.Wait();

                if(!String.IsNullOrEmpty(searchString))
                {
                    videos = response.Result;
                }

                if (response.Status == TaskStatus.RanToCompletion) 
                {
                    videos = response.Result;
                }

                return videos;
            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public VideoDto GetVideo(int? id)
        {
            if (id == null)
            {
                return null;
            }

            VideoDto video = new VideoDto();

            try
            {
                var response = _httpClient.GetFromJsonAsync<VideoDto>(_httpClient.BaseAddress + "Videos/" + id);
                response.Wait();

                if (response.Status == TaskStatus.RanToCompletion)
                {
                    video = response.Result;
                }

                if (video == null)
                {
                    return null;
                }

                return video;

            }
            catch (Exception ex)
            {
                return null;
            }

        }

        public VideoDto? UpdateVideo(int id, VideoDto video, IFormFile fileImage, IFormFile fileVideo)
        {
            if (id != video.Id)
            {
                return null;
            }

            video.ContentVideo = _uploadService.UploadVideo(fileVideo);
            video.ThumbVideo = _uploadService.UploadImage(fileImage);

            try
            {
                var response = _httpClient.PostAsJsonAsync<VideoDto>(_httpClient.BaseAddress + "Videos/", video);
                response.Wait();

                if (response.Status == TaskStatus.RanToCompletion)
                {
                    return null;
                }

                return video;
            }
            catch (Exception ex)
            {
                return null;
            }

            return video;
        }
    }
}
