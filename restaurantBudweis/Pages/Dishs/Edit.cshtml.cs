using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Hubs;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dishs
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;
       

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
           
        }

        [BindProperty]
        public Dish Dish { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs
                        .Where(c => c.Id == id)
                        .Include(b => b.GroupDish)
                        .FirstOrDefault();

            if (Dish == null)
                return NotFound();

            return Page();
        }
            
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
                return Page();

            _context.Dishs.Update(Dish);
            _context.SaveChanges();

          

            return RedirectToPage("Index");
        }
    }
}
