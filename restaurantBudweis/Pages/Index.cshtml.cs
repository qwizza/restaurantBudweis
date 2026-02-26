using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using restaurantBudweis.Data;
using restaurantBudweis.Model;

namespace restaurantBudweis.Pages.Dish
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly ApplicationDbContext _context; 

        public IndexModel(/*ILogger<IndexModel> logger*/, ApplicationDbContext context)
        {
            /*_logger = logger;*/
            _context = context; 
        }

        public List<GroupDish> GroupDishes { get; set; }
        public void OnGet()
        {
           /* var dish = new Dish { GroupDish = new() { Name = "Горячие блюда" }, Dishs = "Крем-суп" };
            _context.Dishs.Add(dish);
            _context.SaveChanges();*/


            var Dishs = _context.Dishs.ToList();
        }
    }
}
