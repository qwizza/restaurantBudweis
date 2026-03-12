using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dishs
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public CreateModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; }

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Dishs.Add(Dish);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
