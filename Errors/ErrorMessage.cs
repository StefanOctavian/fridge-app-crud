using System.Net;
using System.Text.Json.Serialization;

namespace Crud.Errors;

/// <summary>
/// This is a simple class to transmit the error information to the client.
/// It includes the message and the HTTP status code to be set on the HTTP response.
/// </summary>
public class ErrorMessage(HttpStatusCode status, string message)
{
    public string Message { get; } = message;

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public HttpStatusCode Status { get; } = status;

    public static ErrorMessage FromException(ServerException exception) => 
        new(exception.Status, exception.Message);
}
