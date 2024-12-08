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
    }
}