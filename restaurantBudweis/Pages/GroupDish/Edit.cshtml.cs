using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;

namespace restaurantBudweis.Pages.GroupDishs
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.Group GroupDish { get; set; }

        public IActionResult OnGet(int id)
        {
            GroupDish = _context.DishsGroupDishs.FirstOrDefault(b => b.Id == id);

            if (GroupDish == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.DishsGroupDishs.Update(GroupDish);
            _context.SaveChanges();

            return RedirectToPage("Index");
        }
    }
}
