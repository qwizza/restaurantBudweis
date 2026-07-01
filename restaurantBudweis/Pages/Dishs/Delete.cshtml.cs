using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dishs
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs
                        .Where(c => c.Id == id)
                        .Include(b => b.Group)
                        .FirstOrDefault();

            if (Dish == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            var dish = _context.Dishs.Find(Dish.Id);

            if (dish != null)
            {
                _context.Dishs.Remove(dish);
                _context.SaveChanges();
            }

            return RedirectToPage("Index");
        }
    }
}
