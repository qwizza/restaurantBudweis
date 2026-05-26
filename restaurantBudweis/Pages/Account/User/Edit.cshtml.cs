using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR; 
using restaurantBudweis.Data;
using restaurantBudweis.Model.AuthApp;
using System.Threading.Tasks;

namespace restaurantBudweis.Pages.Account.User
{
    [Authorize(Roles = "Admin")]
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
        public AuthUser AuthUser { get; set; } = new();

        public async Task<IActionResult> OnGetAsync(int id)
        {
            AuthUser = await _context.AuthUsers.FirstOrDefaultAsync(u => u.Id == id);

            if (AuthUser == null)
                return NotFound();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            ModelState.Remove("AuthUser.Password");

            if (!ModelState.IsValid)
                return Page();

            var userFromDb = await _context.AuthUsers.FindAsync(AuthUser.Id);
            if (userFromDb == null)
                return NotFound();

            userFromDb.Email = AuthUser.Email;
            userFromDb.Role = AuthUser.Role;

            if (!string.IsNullOrWhiteSpace(AuthUser.Password))
            {
                userFromDb.Password = AuthUser.Password;
            }

            await _context.SaveChangesAsync(); 
            await _hubContext.Clients.All.SendAsync("RefreshUsers");

            return RedirectToPage("Index");
        }
    }
}