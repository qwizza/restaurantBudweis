using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model.AuthApp;
using System.Security.Claims;

namespace restaurantBudweis.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public RegisterModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Model.AuthApp.Register Input { get; set; }

        public void OnGet() { }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
                return Page();

            bool isFirstUser = !_context.AuthUsers.Any();

            var user = _context.AuthUsers.FirstOrDefault(u => u.Email == Input.Email);

            if (user == null)
            {
                user = new AuthUser { Email = Input.Email, Password = Input.Password, Role = isFirstUser ? "Admin" : "User", AvatarUrl = "/avatars/default.png" };
                _context.AuthUsers.Add(user);
                await _context.SaveChangesAsync();

                await Authenticate(user.Email, user.Role, user.AvatarUrl);
                return RedirectToPage("/Index");
            }

            ModelState.AddModelError(string.Empty, "Пользователь уже есть!");
            return Page();
        }

        private async Task Authenticate(string userName, string role, string avatarUrl)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, role),
                // Сохраняем аватарку в сессию
                new Claim("AvatarUrl", avatarUrl)
            };

            var identity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
