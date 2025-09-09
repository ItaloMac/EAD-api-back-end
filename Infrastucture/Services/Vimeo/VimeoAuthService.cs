using Microsoft.Extensions.Configuration;
using VimeoDotNet;

public class VimeoAuthService
{
    private readonly string _clientId;
    private readonly string _clientSecret;
    private readonly string _redirectUrl;

    public VimeoAuthService(IConfiguration config)
    {
        _clientId = config["Vimeo:ClientId"]!;
        _clientSecret = config["Vimeo:ClientSecret"]!;
        _redirectUrl = config["Vimeo:RedirectUrl"]!;
    }

    public VimeoClient CreateClient()
    {
        return new VimeoClient(_clientId, _clientSecret);
    }

    public string GetLoginUrl(string state = null!)
    {
        var client = CreateClient();
        return $"https://api.vimeo.com/oauth/authorize?response_type=code&client_id={_clientId}&redirect_uri={Uri.EscapeDataString(_redirectUrl)}&scope=public+private+create+edit+delete+interact+upload+video_files&state={state}";
    }

    public async Task<string> GetAccessTokenAsync(string authCode)
    {
        var client = CreateClient();
        var token = await client.GetAccessTokenAsync(authCode, _redirectUrl);
         if (token?.AccessToken == null)
        throw new Exception("Falha ao obter access token");
        return token.AccessToken;
    }
}
