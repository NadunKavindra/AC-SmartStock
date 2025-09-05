namespace APIBackend.Services.Interfaces;

public interface ILoggingService
{
    Task LogAsync(string message, string level = "Info", string? source = null, Exception? ex = null);
}
