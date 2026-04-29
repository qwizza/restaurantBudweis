using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using static System.Reflection.Metadata.BlobBuilder;

namespace restaurantBudweis.Pages.Dishs
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Dish> Dishs { get; set; }

        public async Task OnGet()
        {
            Dishs = await _context.Dishs
                .Include(d => d.Group) !
                .ToListAsync();

        }
    }
}
