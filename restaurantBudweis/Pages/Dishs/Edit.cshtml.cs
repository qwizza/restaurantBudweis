using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dishs
{
    public class EditDishModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditDishModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs.Find(id);

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

            return RedirectToPage("Index");
        }
    }
}
