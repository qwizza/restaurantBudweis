using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Linq;

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
        public Dish Dish { get; set; } = new();

        public SelectList GroupDishList { get; set; }

        public void OnGet()
        {
            var groups = _context.DishsGroupDishs.ToList();
            GroupDishList = new SelectList(groups, "Id", "Name");
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var groups = _context.DishsGroupDishs.ToList();
                GroupDishList = new SelectList(groups, "Id", "Name");
                return Page();
            }

            _context.Dishs.Add(Dish);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}