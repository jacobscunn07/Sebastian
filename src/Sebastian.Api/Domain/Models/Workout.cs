using System;

namespace Sebastian.Api.Domain.Models
{
    public class Workout
    {
        public Guid Id { get; set; }
        
        public string Name { get; set; }
        
        public DateTime DateTimeBegan { get; set; }
        
        public DateTime DateTimeFinished { get; set; }
    }
}
