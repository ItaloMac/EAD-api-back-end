public interface IVideoService
{
    Task<string> UploadVideoAsync(Stream fileStream, string fileName, string description);
    Task<string> GetVideoUrlAsync(string videoId);
    Task DeleteVideoAsync(string videoId); // NOVO
}
