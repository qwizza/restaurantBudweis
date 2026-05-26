using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.GroupDishs
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<PageUpdateHub> _hubContext;

        public CreateModel(ApplicationDbContext context, IHubContext<PageUpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Model.Group GroupDish { get; set; } = new();

        public void OnGet() { }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.DishsGroupDishs.Add(GroupDish);
            _context.SaveChanges(); 
            _hubContext.Clients.All.SendAsync("RefreshGroups").Wait();

            return RedirectToPage("Index");
        }
    }
}