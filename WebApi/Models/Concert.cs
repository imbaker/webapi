using System;

namespace WebApi.Models
{
    public class Concert
    {
        public int Id { get; set; }
        public string Artist { get; set; }
        public string Venue { get; set; }
        public DateTime Date { get; set; }
        public int Duration { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}