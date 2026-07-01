using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Clients
{
    public class Index1Model(ApplicationDbContext context) : PageModel
    {
        private readonly ApplicationDbContext _context = context;

        public Client Client { get; set; }

        public IActionResult OnGet(int id)
        {
            Client = _context.Clients
                .Include(c => c.Dishs)
                .FirstOrDefault(s => s.Id == id);

            if (Client == null)
                return NotFound();

            return Page();
        }
    }
}
