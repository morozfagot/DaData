namespace DaData.Domain.Errors
{
    public record ErrorResponse(int StatusCode, string Title, string Message)
    {
    }
}
