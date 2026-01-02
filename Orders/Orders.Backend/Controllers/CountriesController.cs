using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Orders.Backend.Data;
using Orders.Shared.Entities;

namespace Orders.Backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CountriesController : ControllerBase
{
    private readonly DataContext _context;

    public CountriesController(DataContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAsync()
    {
        return Ok(await _context.Countries.ToListAsync());
    }

    [HttpPost]
    public async Task<IActionResult> PostAsync(Country country)
    {
        _context.Add(country);
        await _context.SaveChangesAsync();
        return Ok(country);
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> PutAsync(int id, Country country)
    {
        var countryDb = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
        if(countryDb is null)
        {
            return NotFound();
        }

        countryDb.Name = country.Name;
        _context.Update(countryDb);
        await _context.SaveChangesAsync();
        return Ok(countryDb);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteAsync(int id, Country country)
    {
        var countryDb = await _context.Countries.FirstOrDefaultAsync(c => c.Id == id);
        if (countryDb is null)
        {
            return NotFound();
        }

        _context.Remove(countryDb);
        await _context.SaveChangesAsync();
        return NoContent();
    }
}
