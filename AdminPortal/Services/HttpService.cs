using AdminPortal.Helpers;

namespace AdminPortal.Services;

public class HttpService
{
    private readonly HttpClient _httpClient;

    public HttpService(IHttpClientFactory httpClientFactory)
    {
        _httpClient = httpClientFactory.CreateClient(nameof(Config.API));
    }

    public async Task<HttpResponseMessage> GetAsync(Func<UrlHelper, string> url)
    {
        return await _httpClient.GetAsync(url(new UrlHelper()));
    }

    public async Task<HttpResponseMessage> PostAsync<TRequest>(Func<UrlHelper, string> url, TRequest model)
    {
        return await _httpClient.PostAsJsonAsync(url(new UrlHelper()), model);
    }


    public void SetAuthToken(string token)
    {
        if (_httpClient.DefaultRequestHeaders.Contains("Authorization"))
            _httpClient.DefaultRequestHeaders.Remove("Authorization");

        _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
    }

    public void RemoveAuthToken()
    {
        _httpClient.DefaultRequestHeaders.Remove("Authorization");
    }
}