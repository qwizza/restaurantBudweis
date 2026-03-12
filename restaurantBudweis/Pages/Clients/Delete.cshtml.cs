using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Clients
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public DeleteModel(ApplicationDbContext context)
        {
            _context = context;
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
            }

            return RedirectToPage("Index");
        }
    }
}
