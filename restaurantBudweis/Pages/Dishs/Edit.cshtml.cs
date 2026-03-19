using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Hubs;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dishs
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<DishHub> _hubContext;

        public EditModel(ApplicationDbContext context, IHubContext<DishHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Dish Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs
                        .Where(c => c.Id == id)
                        .Include(b => b.GroupDish)
                        .FirstOrDefault();

            if (Dish == null)
                return NotFound();

            return Page();
        }
            
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Dishs.Update(Dish);
            _context.SaveChanges();

            _hubContext.Clients.All.SendAsync("BookUpdated", Dish);

            return RedirectToPage("Index");
        }
    }
}
