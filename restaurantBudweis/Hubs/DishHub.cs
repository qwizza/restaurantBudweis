using Microsoft.AspNetCore.SignalR;
using restaurantBudweis.Model;
namespace restaurantBudweis.Hubs

{
    public class DishHub : Hub
    {
        public async Task SendBookUpdate(Dish dish)
        {
            await Clients.All.SendAsync("DishUpdated", dish);
        }
    }
}
