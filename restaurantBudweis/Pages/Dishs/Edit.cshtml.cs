using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR; 
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Linq;

namespace restaurantBudweis.Pages.Dishs
{
    public class EditDishModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<PageUpdateHub> _hubContext;

        public EditDishModel(ApplicationDbContext context, IHubContext<PageUpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Dish Dish { get; set; } = new();

        public SelectList GroupDishList { get; set; }

        public IActionResult OnGet(int id)
        {
            Dish = _context.Dishs
                .Include(d => d.Group)
                .FirstOrDefault(d => d.Id == id);

            if (Dish == null)
                return NotFound();

            var groups = _context.DishsGroupDishs.ToList();
            GroupDishList = new SelectList(groups, "Id", "Dishs", Dish.GroupId);

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                var groups = _context.DishsGroupDishs.ToList();
                GroupDishList = new SelectList(groups, "Id", "Dishs", Dish.GroupId);
                return Page();
            }

            _context.Dishs.Update(Dish);
            _context.SaveChanges(); 
            _hubContext.Clients.All.SendAsync("RefreshDishes").Wait();

            return RedirectToPage("Index");
        }
    }
}