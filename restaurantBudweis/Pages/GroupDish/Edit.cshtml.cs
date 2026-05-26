using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Linq;

namespace restaurantBudweis.Pages.GroupDishs
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<PageUpdateHub> _hubContext;

        public EditModel(ApplicationDbContext context, IHubContext<PageUpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
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
            _hubContext.Clients.All.SendAsync("RefreshGroups").Wait();

            return RedirectToPage("Index");
        }
    }
}