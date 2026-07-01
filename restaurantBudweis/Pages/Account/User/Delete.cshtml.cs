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
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<PageUpdateHub> _hubContext;

        public DeleteModel(ApplicationDbContext context, IHubContext<PageUpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public AuthUser User { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            User = await _context.AuthUsers.FindAsync(id);

            if (User == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _context.AuthUsers.FindAsync(User.Id);

            if (user != null)
            {
                _context.AuthUsers.Remove(user);
                await _context.SaveChangesAsync(); 
                await _hubContext.Clients.All.SendAsync("RefreshUsers");
            }

            return RedirectToPage("Index");
        }
    }
}