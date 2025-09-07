using APIBackend.Data;
using APIBackend.Models;
using APIBackend.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace APIBackend.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CustomerController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ILoggingService _logger;

    public CustomerController(ApplicationDbContext context, ILoggingService logger)
    {
        _context = context;
        _logger = logger;
    }

    //GET: api/customer
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var customers = await _context.Customers.ToListAsync();
        return Ok(customers);
    }

    // GET: api/customer/
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound("No customer for given id");

        return Ok(customer);
    }

    // POST: api/customer
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Customer customer)
    {
        _context.Customers.Add(customer);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = customer.CustomerIdCode }, customer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(string id, [FromBody] Customer updatedCustomer)
    {
        if (id != updatedCustomer.CustomerIdCode)
            return BadRequest("ID mismatch");

        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();

        customer.Name = updatedCustomer.Name;
        customer.PhoneNumber = updatedCustomer.PhoneNumber;
        customer.Address = updatedCustomer.Address;
        customer.Email = updatedCustomer.Email;

        await _context.SaveChangesAsync();
        return NoContent();
    }


    // DELETE: api/customer/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var customer = await _context.Customers.FindAsync(id);
        if (customer == null)
            return NotFound();

        _context.Customers.Remove(customer);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}
