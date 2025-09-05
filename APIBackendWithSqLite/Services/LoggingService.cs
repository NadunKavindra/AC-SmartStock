using APIBackend.Data;
using APIBackend.Models;
using APIBackend.Services.Interfaces;

namespace APIBackend.Services;

public class LoggingService : ILoggingService
{
    private readonly ApplicationDbContext _context;
    public LoggingService(ApplicationDbContext context)
    {
        _context = context;
    }
    public async Task LogAsync(string message, string level = "Info", string? source = null, Exception? ex = null)
    {
        var log = new Logs
        {
            Timestamp = DateTime.UtcNow,
            Message = message,
            Level = level,
            Source = source,
            Exception = ex?.ToString()
        };

        _context.Logs.Add(log);
        await _context.SaveChangesAsync();
    }
}
