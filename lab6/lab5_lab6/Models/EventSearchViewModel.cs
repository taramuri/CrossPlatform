using lab6.Models;
using Microsoft.AspNetCore.Mvc;

namespace lab5_lab6.Models
{
    public class EventSearchViewModel
    {
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public List<int> CustomerIds { get; set; }
        public string ChannelNamePrefix { get; set; }
        public decimal? MinEventAmount { get; set; }
        public List<Event> SearchResults { get; set; }
    }
}
