using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace restaurantBudweis.Pages.Chat
{
    [Authorize]
    public class SupportChatModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
