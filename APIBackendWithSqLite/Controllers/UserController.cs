using APIBackend.Data;
using APIBackend.DTOs;
using APIBackend.Models;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers;


[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILoggingService _logger;

    public UserController(ApplicationDbContext context, ILoggingService logger)
    {
        _context = context;
        _logger = logger;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterUserDto dto)
    {
        try
        {
            // Check if username already exists
            if (await _context.Users.AnyAsync(u => u.Username == dto.Username))
            {
                return BadRequest("Username is already taken.");
            }

            // Hash the password using BCrypt
            //string passwordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            string passwordHash = dto.Password;

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = passwordHash,
                FullName = dto.FullName,
                Email = dto.Email,
                CreatedAt = DateTime.UtcNow
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {

            await _logger.LogAsync("Exception caught", "Error", "UserController", ex);
            return StatusCode(500, "Error occurred. Check logs.");
        }

        return Ok(new { message = "User registered successfully" });
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto dto)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == dto.Username);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        // Verify hashed password
        //bool isPasswordValid = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
        bool isPasswordValid = Equals(dto.Password, user.PasswordHash);

        if (!isPasswordValid)
        {
            return Unauthorized(new { message = "Invalid username or password." });
        }

        // Login successful
        return Ok(new
        {
            message = "Login successful",
            user = new
            {
                user.Id,
                user.Username,
                user.FullName,
                user.Email
            }
        });
    }
}
