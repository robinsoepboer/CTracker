using System.Security.Cryptography;
using System.Text;

namespace CTracker.Services;

public class SignatureService : ISignatureService
{
    public string Generate(string key, string timestamp, string method, string url, string body = "")
    {
        return HashHmac(key, $"{timestamp}{method}{url}{body}");
    }

    private string HashHmac(string secret, string message)
    {
        var encoding = new ASCIIEncoding();
        var keyBytes = encoding.GetBytes(secret);
        var messageBytes = encoding.GetBytes(message);
        var cryptographer = new HMACSHA256(keyBytes);

        byte[] bytes = cryptographer.ComputeHash(messageBytes);

        return BitConverter.ToString(bytes).Replace("-", "").ToLower();
    }
}