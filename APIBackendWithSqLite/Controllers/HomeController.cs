using APIBackend.Data;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;

namespace APIBackend.Controllers;

[ApiController]
[Route("[controller]")]
public class HomeController : ControllerBase
{
    private readonly ILoggingService _logger;
    public HomeController(ILoggingService logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index()
    {
        try
        {
            await _logger.LogAsync("Test log message", "Info", "HomeController");
        }
        catch (Exception ex)
        {
            await _logger.LogAsync("Exception caught", "Error", "HomeController", ex);
            return StatusCode(500, "Error occurred. Check logs.");
        }

        return Ok("Logs created");
    }

    

}
