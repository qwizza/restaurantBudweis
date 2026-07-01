using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using System.Security.Claims;

namespace restaurantBudweis.Pages.Account
{
    [Authorize] // Только для вошедших пользователей
    public class ProfileModel(ApplicationDbContext context, IWebHostEnvironment environment) : PageModel
    {
        public string CurrentAvatarUrl { get; set; } = "/avatars/default.png";

        [BindProperty]
        public IFormFile? Upload { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await context.AuthUsers.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            if (user != null)
            {
                CurrentAvatarUrl = user.AvatarUrl;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await context.AuthUsers.FirstOrDefaultAsync(u => u.Email == User.Identity.Name);
            if (user == null) return NotFound();

            if (Upload != null)
            {
                // Валидация расширения файла (Безопасность на "5")
                var extension = Path.GetExtension(Upload.FileName).ToLowerInvariant();
                var allowedExtensions = new[] { ".jpg", ".jpeg", ".png" };

                if (!allowedExtensions.Contains(extension))
                {
                    ModelState.AddModelError(string.Empty, "Можно загружать только изображения (.jpg, .jpeg, .png)");
                    CurrentAvatarUrl = user.AvatarUrl;
                    return Page();
                }

                // Генерация уникального имени файла
                var uniqueFileName = $"{Guid.NewGuid()}{extension}";
                var folderPath = Path.Combine(environment.WebRootPath, "avatars");

                if (!Directory.Exists(folderPath))
                    Directory.CreateDirectory(folderPath);

                var filePath = Path.Combine(folderPath, uniqueFileName);

                // Сохраняем файл на сервере в wwwroot/avatars/
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await Upload.CopyToAsync(fileStream);
                }

                // Обновляем данные в базе данных
                user.AvatarUrl = $"/avatars/{uniqueFileName}";
                context.AuthUsers.Update(user);
                await context.SaveChangesAsync();

                // Динамически обновляем Cookie-тикет, чтобы аватарка поменялась в Navbar сразу без перезахода
                var identity = (ClaimsIdentity)User.Identity!;
                var existingClaim = identity.FindFirst("AvatarUrl");
                if (existingClaim != null) identity.RemoveClaim(existingClaim);

                identity.AddClaim(new Claim("AvatarUrl", user.AvatarUrl));
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));
            }

            return RedirectToPage();
        }
    }
}
