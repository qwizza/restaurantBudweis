using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.GroupDishs
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public IndexModel(ApplicationDbContext context)
        {
            _context = context;
        }
        public List<Group> GroupDishs { get; set; } = new();  

        public async Task OnGetAsync()
        {
            GroupDishs = await _context.DishsGroupDishs.ToListAsync();  
        }
    }
}