using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Model;
using System.Collections.Generic;

namespace restaurantBudweis.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Dish> Dishs { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Group> DishsGroupDishs { get; set; }
    }
}
