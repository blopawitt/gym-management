using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagement.Data;
using GymManagement.Models;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrainersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TrainersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Trainer>>> GetTrainers()
        {
            return await _context.Trainers.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Trainer>> GetTrainer(int id)
        {
            var trainer = await _context.Trainers.FindAsync(id);

            if (trainer == null)
            {
                return NotFound();
            }

            return trainer;
        }

        [HttpPost]
        public async Task<ActionResult<Trainer>> PostTrainer(Trainer trainer)
        {
            _context.Trainers.Add(trainer);
            await _context.SaveChangesAsync();

            return StatusCode(201, trainer);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Trainer trainer)
        {
            var existingTrainer = _context.Trainers.Find(id);
            if (existingTrainer == null)
            {
                return NotFound();
            }
            existingTrainer.Name = trainer.Name;
            existingTrainer.Email = trainer.Email;
            existingTrainer.Phone = trainer.Phone;
            existingTrainer.Address = trainer.Address;
            existingTrainer.JoiningDate = trainer.JoiningDate;
            existingTrainer.Salary = trainer.Salary;
            _context.SaveChanges();
            return Ok(existingTrainer);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var trainer = _context.Trainers.Find(id);
            if (trainer == null)
            {
                return NotFound();
            }

            _context.Trainers.Remove(trainer);
            _context.SaveChanges();
            return NoContent();
        }
    }
}