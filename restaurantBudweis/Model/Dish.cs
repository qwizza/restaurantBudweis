namespace restaurantBudweis.Model
{
    public class Dish : EFModel
    {
        public string? GroupDish { get; set; }
        public int Price { get; set; }
        public int CookingTimeMinutes { get; set; }
    }
}
