using Microsoft.AspNetCore.Mvc;
using GymManagement.Models;
using GymManagement.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IncomesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IncomesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Income>> GetIncomes()
        {
            return _context.Incomes.ToList();
        }

        [HttpPost]
        public ActionResult<Income> PostIncome(Income income)
        {
            if (!ModelState.IsValid)
            {
                foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.WriteLine(error.ErrorMessage);
                }
                return BadRequest(ModelState);
            }

            // Convert DateTime values to UTC
            income.Date = income.Date.ToUniversalTime();

            _context.Incomes.Add(income);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetIncomes), new { id = income.Id }, income);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutIncome(int id, Income income)
        {
            if (id != income.Id)
            {
                return BadRequest();
            }

            _context.Entry(income).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Incomes.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpGet("total-income")]
        public async Task<ActionResult<decimal>> GetTotalIncome()
        {
            try
            {
                var subscriptionIncome = await _context.Members
                    .GroupBy(m => m.SubscriptionPlan)
                    .Select(g => new
                    {
                        SubscriptionPlan = g.Key,
                        TotalMembers = g.Count()
                    })
                    .ToListAsync();

                var totalSubscriptionIncome = subscriptionIncome.Sum(s => s.TotalMembers * GetSubscriptionPrice(s.SubscriptionPlan));

                var totalIncomeAmount = await _context.Incomes.SumAsync(i => i.Amount);

                var totalIncome = totalSubscriptionIncome + totalIncomeAmount;

                return Ok(totalIncome);
            }
            catch (Exception)
            {
                return StatusCode(500, "Internal server error");
            }
        }

        private decimal GetSubscriptionPrice(string subscriptionPlan)
        {
            var price = _context.Prices.FirstOrDefault();
            if (price == null)
            {
                return 0;
            }

            return subscriptionPlan switch
            {
                "Monthly" => price.Monthly,
                "Trimestral" => price.Trimestral,
                "SemiAnnual" => price.SemiAnnual,
                "Annual" => price.Annual,
                _ => 0
            };
        }
    }
}