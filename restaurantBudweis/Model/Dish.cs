namespace restaurantBudweis.Model
{
    public class Dish : EFModel
    {
        public GroupDish? GroupDish { get; set; }
        public int DishName { get; set; }
        public int Price { get; set; }
        public int CookingTimeMinutes { get; set; }
        public string? DescriptionDish { get; set; } 
        public string? Ingredients { get; set; } 
    }
}
