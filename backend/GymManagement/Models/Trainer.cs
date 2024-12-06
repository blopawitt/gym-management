using System.Collections.Generic;

namespace GymManagement.Models
{
    public class Trainer
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Email { get; set; }
        public required string Phone { get; set; }
        public required string Address { get; set; }
        public DateTime JoiningDate { get; set; }
        public decimal Salary { get; set; }
        public required ICollection<Class> Classes { get; set; }
    }
}