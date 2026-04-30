using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model;
using System.Collections.Generic;
using System.Linq;

namespace restaurantBudweis.Pages.Clients
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public EditModel(ApplicationDbContext context)
        {
            _context = context;
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

            // Загружаем все блюда для списка
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

            // Загружаем существующего клиента с его блюдами
            var existingClient = _context.Clients
                .Include(c => c.Dishs)
                .FirstOrDefault(c => c.Id == Client.Id);

            if (existingClient != null)
            {
                // Обновляем поля
                existingClient.FullName = Client.FullName;
                existingClient.PhoneNumber = Client.PhoneNumber;
                existingClient.TableNumber = Client.TableNumber;
                existingClient.VisitDate = Client.VisitDate;

                // Обновляем список блюд
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
            }

            return RedirectToPage("Index");
        }
    }
}
