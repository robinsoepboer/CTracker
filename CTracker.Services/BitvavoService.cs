using System.Text.Json;
using CTracker.Models.Bitvavo;

namespace CTracker.Services;

public class BitvavoService : IBitvavoService
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ISignatureService _signatureService;
    
    public BitvavoService(IHttpClientFactory httpClientFactory, ISignatureService signatureService)
    {
        _httpClientFactory = httpClientFactory;
        _signatureService = signatureService;
    }

    public async Task<List<AccountResponseItem>?> Balance()
    {
        return await Get<List<AccountResponseItem>>("/v2/balance");
    }
    
    public async Task<List<TradeResponse>?> Trades(string coinSymbol)
    {
        return await Get<List<TradeResponse>>($"/v2/trades?market={coinSymbol}-EUR");
    }
    
    public async Task<TickerResponse?> Ticker(string coinSymbol)
    {
        return await Get<TickerResponse>($"/v2/ticker/price?market={coinSymbol}-EUR");
    }
    
    public async Task<TimeResponse?> Time()
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,"https://api.bitvavo.com/v2/time");

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<TimeResponse>(responseBody);
        }
        else
        {
            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        return null;
    }

    private async Task<T?> Get<T>(string url) where T : class
    {
        var method = HttpMethod.Get;
        var httpRequestMessage = new HttpRequestMessage(method,$"https://api.bitvavo.com{url}");

        var httpClient = _httpClientFactory.CreateClient();
        await SetHeaders(httpClient, method, url);

        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            var responseBody = await httpResponseMessage.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseBody);
        }
        else
        {
            var contentStream = await httpResponseMessage.Content.ReadAsStringAsync();
        }

        return null;
    }
    
    
    private async Task SetHeaders(HttpClient httpClient, HttpMethod method, string url)
    {
        httpClient.DefaultRequestHeaders.Add("Bitvavo-Access-Key", "");
        httpClient.DefaultRequestHeaders.Add("Bitvavo-Access-Window", "60000");

        var timeResponse = await Time();

        httpClient.DefaultRequestHeaders.Add("Bitvavo-Access-Timestamp", timeResponse?.time.ToString());
        
        var signature = _signatureService.Generate(
            "",
            timeResponse!.time.ToString(),
            method.ToString().ToUpper(),
            url
        );
        httpClient.DefaultRequestHeaders.Add("Bitvavo-Access-Signature", signature);
    }
}