using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;

namespace restaurantBudweis.Pages.GroupDishs
{
    public class DetailsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DetailsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public Model.Group GroupDish { get; set; }

        public IActionResult OnGet(int id)
        {
            GroupDish = _context.DishsGroupDishs.FirstOrDefault(b => b.Id == id);

            if (GroupDish == null)
                return NotFound();

            return Page();
        }
    }
}
