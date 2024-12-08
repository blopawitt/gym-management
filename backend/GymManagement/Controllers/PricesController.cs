using Microsoft.AspNetCore.Mvc;
using GymManagement.Models;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GymManagement.Data;

namespace GymManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PricesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> SavePrices([FromBody] Price prices)
        {
            _context.Prices.Add(prices);
            await _context.SaveChangesAsync();
            return Ok(prices);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrices(int id, [FromBody] Price prices)
        {
            var existingPrices = await _context.Prices.FindAsync(id);
            if (existingPrices == null)
            {
                return NotFound();
            }

            existingPrices.Monthly = prices.Monthly;
            existingPrices.Trimestral = prices.Trimestral;
            existingPrices.SemiAnnual = prices.SemiAnnual;
            existingPrices.Annual = prices.Annual;

            _context.Entry(existingPrices).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok(existingPrices);
        }

        [HttpGet("latest")]
        public async Task<IActionResult> GetLatestPrice()
        {
            try
            {
                var latestPrice = await _context.Prices
                    .OrderByDescending(p => p.Id)
                    .FirstOrDefaultAsync();

                if (latestPrice == null)
                {
                    Console.WriteLine("No prices found in the database.");
                    return NotFound();
                }

                Console.WriteLine($"Latest price found: {latestPrice.Id}");
                return Ok(latestPrice);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error fetching latest price: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }
    }
}