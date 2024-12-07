using Microsoft.EntityFrameworkCore;
using GymManagement.Models;

namespace GymManagement.Data
{
    public class ApplicationDbContext : DbContext
    {
        public required DbSet<Member> Members { get; set; }
        public required DbSet<Trainer> Trainers { get; set; }
        public required DbSet<Class> Classes { get; set; }
        public required DbSet<User> Users { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            Members = Set<Member>();
            Trainers = Set<Trainer>();
            Classes = Set<Class>();
            Users = Set<User>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Trainer>(entity =>
            {
                entity.Property(e => e.Salary).HasColumnType("decimal(18,2)");
            });
        }
    }
}