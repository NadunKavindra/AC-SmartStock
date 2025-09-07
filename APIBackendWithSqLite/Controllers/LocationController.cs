using APIBackend.Data;
using APIBackend.Models;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class LocationController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILoggingService _logger;

    public LocationController(ApplicationDbContext context, ILoggingService logger)
    {
        _context = context;
        _logger = logger;
    }

    //GET: api/location
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var locations = await _context.Locations.ToListAsync();
        return Ok(locations);
    }

    // GET: api/location
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
            return NotFound("No location for given id");

        return Ok(location);
    }

    // POST: api/location
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Location location)
    {
        // Check if the customer exists
        var customerExists = await _context.Customers
            .AnyAsync(c => c.CustomerIdCode == location.CustomerIdCode);

        if (!customerExists)
            return BadRequest($"Customer with {location.CustomerIdCode} does not exist.");

        _context.Locations.Add(location);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = location.LocationId }, location);
    }

    // DELETE: api/location/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var location = await _context.Locations.FindAsync(id);
        if (location == null)
            return NotFound();

        _context.Locations.Remove(location);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
