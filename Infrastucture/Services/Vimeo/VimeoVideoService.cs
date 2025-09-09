using VimeoDotNet;
using VimeoDotNet.Models;
using VimeoDotNet.Net;
using Microsoft.Extensions.Options;
using Infrastucture.Services.Vimeo;

public class VimeoVideoService : IVideoService
{
    private readonly VimeoClient _client;

     // Construtor 2: Usando token dinâmico (OAuth)
    public VimeoVideoService(string accessToken)
    {
        if (string.IsNullOrWhiteSpace(accessToken))
            throw new ArgumentException("AccessToken não informado.");

        _client = new VimeoClient(accessToken);
    }

    public async Task<string> UploadVideoAsync(Stream fileStream, string fileName, string description)
    {
        var binaryContent = new BinaryContent(fileStream, "video/mp4")
        {
            OriginalFileName = fileName
        };

        var uploadRequest = await _client.UploadEntireFileAsync(binaryContent);

        if (uploadRequest?.ClipId == null)
            throw new InvalidOperationException("Falha ao obter ClipId após upload no Vimeo.");

        await _client.UpdateVideoMetadataAsync(uploadRequest.ClipId.Value, new VideoUpdateMetadata
        {
            Name = fileName,
            Description = description
        });

        return uploadRequest.ClipId.Value.ToString();
    }

    public async Task<string> GetVideoUrlAsync(string videoId)
    {
        if (!long.TryParse(videoId, out var id))
            throw new ArgumentException("VideoId inválido.", nameof(videoId));

        var video = await _client.GetVideoAsync(id);
        return video?.Link ?? throw new InvalidOperationException($"Não foi possível obter o link do vídeo {videoId}.");
    }

    public async Task DeleteVideoAsync(string videoId)
    {
        if (!long.TryParse(videoId, out var id))
            throw new ArgumentException("VideoId inválido.", nameof(videoId));

        await _client.DeleteVideoAsync(id);
    }
}
