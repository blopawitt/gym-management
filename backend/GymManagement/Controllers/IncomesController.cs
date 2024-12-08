using Microsoft.AspNetCore.Mvc;
using GymManagement.Models;
using GymManagement.Data;
using System.Collections.Generic;
using System.Linq;

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
    }
}