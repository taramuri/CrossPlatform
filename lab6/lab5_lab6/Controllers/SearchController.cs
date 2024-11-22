using lab5_lab6.Models;
using lab6.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace lab5_lab6.Controllers
{
    public class SearchController : Controller
    {
        private readonly DataContext _context;

        public SearchController(DataContext context)
        {
            _context = context;
        }

        public IActionResult EventSearch()
        {
            var customers = _context.Customers.ToList();
            ViewBag.Customers = new SelectList(customers, "Customer_ID", "Customer_Name");
            return View(new EventSearchViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> EventSearch(EventSearchViewModel model)
        {
            var query = _context.Events
                .Include(e => e.Customer)
                .Include(e => e.Channel)
                .AsQueryable();

            // Date filtering
            if (model.StartDate.HasValue)
                query = query.Where(e => e.Event_Date_Time >= model.StartDate.Value);
            if (model.EndDate.HasValue)
                query = query.Where(e => e.Event_Date_Time <= model.EndDate.Value);

            // Customer list filtering
            if (model.CustomerIds?.Any() == true)
                query = query.Where(e => model.CustomerIds.Contains(e.Customer_ID));

            // Channel name prefix
            if (!string.IsNullOrEmpty(model.ChannelNamePrefix))
                query = query.Where(e =>
                    e.Channel.Channel_Name.StartsWith(model.ChannelNamePrefix));

            // Amount filtering
            if (model.MinEventAmount.HasValue)
                query = query.Where(e => e.Event_Amount >= model.MinEventAmount.Value);

            model.SearchResults = await query.ToListAsync();
            return View(model);
        }
    }
}
