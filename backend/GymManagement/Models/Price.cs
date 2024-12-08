
using System;

namespace GymManagement.Models
{
    public class Price
    {
        public int Id { get; set; }
        public decimal Monthly { get; set; }
        public decimal Trimestral { get; set; }
        public decimal SemiAnnual { get; set; }
        public decimal Annual { get; set; }
    }
}