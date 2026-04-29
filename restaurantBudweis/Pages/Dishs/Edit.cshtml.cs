using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Linq;

namespace restaurantBudweis.Pages.Dishs
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Dish Dish { get; set; } = new();

        public SelectList GroupDishList { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs
                .Include(d => d.Group)
                .FirstOrDefault(d => d.Id == id);

            if (Dish == null)
                return NotFound();

            var groups = _context.DishsGroupDishs.ToList();
            GroupDishList = new SelectList(groups, "Id", "Name", Dish.GroupId);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var groups = _context.DishsGroupDishs.ToList();
                GroupDishList = new SelectList(groups, "Id", "Name", Dish.GroupId);
                return Page();
            }

            _context.Dishs.Update(Dish);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
