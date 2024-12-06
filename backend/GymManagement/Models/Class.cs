namespace GymManagement.Models
{
    public class Class
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int TrainerId { get; set; }
        public Trainer? Trainer { get; set; }
    }
}