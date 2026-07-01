using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR; 
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Clients
{
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
        public Client Client { get; set; }

        public IActionResult OnGet(int id)
        {
            Client = _context.Clients.Find(id);

            if (Client == null)
                return NotFound();

            return Page();
        }

        public IActionResult OnPost()
        {
            var clients = _context.Clients.Find(Client.Id);

            if (clients != null)
            {
                _context.Clients.Remove(clients);
                _context.SaveChanges(); 
                _hubContext.Clients.All.SendAsync("RefreshClients").Wait();
            }

            return RedirectToPage("Index");
        }
    }
}