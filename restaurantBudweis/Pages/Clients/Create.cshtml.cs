using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace restaurantBudweis.Pages.Clients
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _context;
        private readonly IHubContext<PageUpdateHub> _hubContext;

        public CreateModel(ApplicationDbContext context, IHubContext<PageUpdateHub> hubContext)
        {
            _context = context;
            _hubContext = hubContext;
        }

        [BindProperty]
        public Client Client { get; set; } = new();

        public List<SelectListItem> AvailableDishes { get; set; } = new();

        [BindProperty]
        public List<int> SelectedDishIds { get; set; } = new();

        public void OnGet()
        {
            AvailableDishes = _context.Dishs
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DishName
                })
                .ToList();
        }

        public IActionResult OnPost()
        {
            var count = SelectedDishIds?.Count ?? 0;
            TempData["Debug"] = $"Выбрано блюд: {count}";
            if (SelectedDishIds == null || !SelectedDishIds.Any())
            {
                AvailableDishes = _context.Dishs
                    .Select(d => new SelectListItem
                    {
                        Value = d.Id.ToString(),
                        Text = d.DishName
                    })
                    .ToList();
                return Page();
            }

            if (SelectedDishIds != null && SelectedDishIds.Any())
            {
                Client.Dishs = _context.Dishs
                    .Where(d => SelectedDishIds.Contains(d.Id))
                    .ToList();
            }

            _context.Clients.Add(Client);
            _context.SaveChanges(); 
            _hubContext.Clients.All.SendAsync("RefreshClients").Wait();

            return RedirectToPage("Index");
        }
    }
}