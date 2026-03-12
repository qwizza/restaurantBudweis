using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dishs
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Dish Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs.FirstOrDefault(b => b.Id == id);

            if (Dish == null)
                return NotFound();

            return Page();
        }
    }
}
