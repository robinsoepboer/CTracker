using System.Text.Json;
using CTracker.Models;
using CTracker.Models.CryptoCompareAPI;

namespace CTracker.Services;

public class PriceService : IPriceService
{
    private readonly IHttpClientFactory _httpClientFactory;
    
    public PriceService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
    }

    public async Task<Coin> Get()
    {
        var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,"https://min-api.cryptocompare.com/data/price?fsym=ETH&tsyms=EUR");

        var httpClient = _httpClientFactory.CreateClient();
        var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            await using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            
            var response = await JsonSerializer.DeserializeAsync<PriceCCAM>(contentStream);

            return new Coin
            {
                Symbol = "ETH",
                Price = response?.EUR ?? 0.00m
            };
        }
        
        return new Coin
        {
            Symbol = "ETH",
            Price = 0.00m
        };
    }
}