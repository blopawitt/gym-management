using Microsoft.AspNetCore.Mvc;
using GymManagement.Models;
using GymManagement.Data;
using System.Collections.Generic;
using System.Linq;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExpensesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ExpensesController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<Expense>> GetExpenses()
        {
            return _context.Expenses.ToList();
        }

        [HttpPost]
        public ActionResult<Expense> PostExpense(Expense expense)
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
            expense.Date = expense.Date.ToUniversalTime();

            _context.Expenses.Add(expense);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetExpenses), new { id = expense.Id }, expense);
        }
    }
}