using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Data;
using restaurantBudweis.Model.AuthApp;
using System.Threading.Tasks;

namespace restaurantBudweis.Pages.Account.User
{
    [Authorize(Roles = "Admin")]
    public class CreateModel(ApplicationDbContext context, IHubContext<PageUpdateHub> hubContext) : PageModel
    {
        private readonly ApplicationDbContext _context = context;
        private readonly IHubContext<PageUpdateHub> _hubContext = hubContext; 

        [BindProperty]
        public AuthUser User { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.AuthUsers.Add(User);
            await _context.SaveChangesAsync(); 
            await _hubContext.Clients.All.SendAsync("RefreshUsers");

            return RedirectToPage("Index");
        }
    }
}