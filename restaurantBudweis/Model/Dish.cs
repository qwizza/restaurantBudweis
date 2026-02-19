namespace restaurantBudweis.Model
{
    public class Dish : EFModel
    {
        public GroupDish? GroupDish { get; set; }
        public int Price { get; set; }
        public int CookingTimeMinutes { get; set; }
        public int DishID { get; set; }
    }
}
