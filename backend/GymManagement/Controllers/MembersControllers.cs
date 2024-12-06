using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GymManagement.Data;
using GymManagement.Models;

namespace GymManagement.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MembersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public MembersController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Member>>> GetMembers()
        {
            return await _context.Members.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Member>> GetMember(int id)
        {
            var member = await _context.Members.FindAsync(id);

            if (member == null)
            {
                return NotFound();
            }

            return member;
        }

        [HttpPost]
        public async Task<ActionResult<Member>> PostMember(Member member)
        {
            // Convert DateTime values to UTC
            member.MembershipStartDate = member.MembershipStartDate.ToUniversalTime();
            member.MembershipEndDate = member.MembershipEndDate.ToUniversalTime();

            _context.Members.Add(member);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMember", new { id = member.Id }, member);
        }

        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Member member)
        {
            var existingMember = _context.Members.Find(id);
            if (existingMember == null)
            {
                return NotFound();
            }
            existingMember.Name = member.Name;
            existingMember.Email = member.Email;
            existingMember.Phone = member.Phone;
            existingMember.MembershipStartDate = member.MembershipStartDate;
            existingMember.MembershipEndDate = member.MembershipEndDate;
            existingMember.Address = member.Address;
            _context.SaveChanges();
            return Ok(existingMember);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var member = _context.Members.Find(id);
            if (member == null)
            {
                return NotFound();
            }

            _context.Members.Remove(member);
            _context.SaveChanges();
            return NoContent();
        }
    }
}