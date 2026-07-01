using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.GroupDishs
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Model.Group GroupDish { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            GroupDish = _context.DishsGroupDishs.Find(id);

            if (GroupDish == null)
            {
                return NotFound();
            }

            return Page();
        }

        public IActionResult OnPost(int id)
        {
            var groupDish = _context.DishsGroupDishs.Find(id);

            if (groupDish != null)
            {
                _context.DishsGroupDishs.Remove(groupDish);
                _context.SaveChanges();
            }

            return RedirectToPage("./Index");
        }
    }
}
