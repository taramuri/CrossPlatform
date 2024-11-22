using lab6.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace lab5_lab6.Controllers
{
    public class EventController : Controller
    {
        private readonly DataContext _context;

        public EventController(DataContext context)
        {
            _context = context;
        }

        // List view for Events
        public async Task<IActionResult> Index()
        {
            var events = await _context.Events
                .Include(e => e.Customer)
                .Include(e => e.Channel)
                .ToListAsync();
            return View(events);
        }

        // Details view for Events
        public async Task<IActionResult> Details(int eventId)
        {
            var eventDetail = await _context.Events
                .Include(e => e.Customer)
                .Include(e => e.Channel)
                .Include(e => e.Location)
                .Include(e => e.Staff)
                .FirstOrDefaultAsync(e => e.Event_ID == eventId);
            return View(eventDetail);
        }
    }
}
