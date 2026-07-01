using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Collections.Generic;
using System.Linq;

namespace restaurantBudweis.Pages.Clients
{
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
        public Client Client { get; set; } = new();

        public List<SelectListItem> AvailableDishes { get; set; } = new();

        [BindProperty]
        public List<int> SelectedDishIds { get; set; } = new();

        public IActionResult OnGet(int id)
        {
            Client = _context.Clients
                .Include(c => c.Dishs)
                .FirstOrDefault(c => c.Id == id);

            if (Client == null)
                return NotFound();

            SelectedDishIds = Client.Dishs?.Select(d => d.Id).ToList() ?? new List<int>();

            AvailableDishes = _context.Dishs
                .Select(d => new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.DishName,
                    Selected = SelectedDishIds.Contains(d.Id)
                })
                .ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
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

            var existingClient = _context.Clients
                .Include(c => c.Dishs)
                .FirstOrDefault(c => c.Id == Client.Id);

            if (existingClient != null)
            {
                existingClient.FullName = Client.FullName;
                existingClient.PhoneNumber = Client.PhoneNumber;
                existingClient.TableNumber = Client.TableNumber;
                existingClient.VisitDate = Client.VisitDate;

                if (SelectedDishIds != null && SelectedDishIds.Any())
                {
                    existingClient.Dishs = _context.Dishs
                        .Where(d => SelectedDishIds.Contains(d.Id))
                        .ToList();
                }
                else
                {
                    existingClient.Dishs = new List<Dish>();
                }

                _context.SaveChanges();
                _hubContext.Clients.All.SendAsync("RefreshClients").Wait();
            }

            return RedirectToPage("Index");
        }
    }
}