using APIBackend.Data;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;

namespace APIBackend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LogsController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILoggingService _logger;
    public LogsController(ApplicationDbContext context, ILoggingService logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet]
    public IActionResult GetLogs()
    {
        var logs = _context.Logs.OrderByDescending(l => l.Timestamp).Take(50).ToList();
        return Ok(logs);
    }

    // DELETE: api/log/delete-oldest/50
    [HttpDelete("delete-oldest/{count:int}")]
    public async Task<IActionResult> DeleteOldestLogs(int count)
    {
        if (count <= 0)
            return BadRequest("Count must be greater than zero.");

        try
        {
            var logsToDelete = await _context.Logs
                .OrderBy(l => l.Timestamp)
                .Take(count)
                .ToListAsync();

            if (!logsToDelete.Any())
                return NotFound("No logs available to delete.");

            _context.Logs.RemoveRange(logsToDelete);
            await _context.SaveChangesAsync();

            return Ok(new { message = $"Deleted {count} oldest log(s)." });
        }
        catch (Exception ex)
        {
            await _logger.LogAsync("Exception caught", "Error", "LogsController", ex);
            return StatusCode(500, "Error occurred. Check logs.");
        }      
    }
}
