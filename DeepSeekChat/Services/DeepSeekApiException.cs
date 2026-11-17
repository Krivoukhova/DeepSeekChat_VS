using System.Net;

namespace DeepSeekChat.Services;

/// <summary>
/// An error returned by the DeepSeek server.
/// </summary>
internal sealed class DeepSeekApiException : Exception
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeepSeekApiException"/> class.
    /// </summary>
    public DeepSeekApiException(HttpStatusCode statusCode, string responseBody)
        : base($"DeepSeek API returned error {(int)statusCode}:\n{responseBody}")
    {
        StatusCode = statusCode;
        ResponseBody = responseBody;
    }

    /// <summary>
    /// Gets status code.
    /// </summary>
    public HttpStatusCode StatusCode { get; }

    /// <summary>
    /// Gets response body.
    /// </summary>
    public string ResponseBody { get; }
}
