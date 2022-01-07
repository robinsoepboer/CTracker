namespace CTracker.Services;

public interface ISignatureService
{
    string Generate(string key, string timestamp, string method, string url, string body = null);
}