using System.Text.Json.Serialization;

namespace restaurantBudweis.Model
{
    public class Dish : EFModel
    {
        public GroupDish GroupDish { get; set; } = new();
        public string DishName { get; set; } = "";
        [JsonIgnore]
        public decimal Price { get; set; }
        public string DescriptionDish { get; set; } = "";
        [JsonIgnore]
        public string Ingredients { get; set; } = "";
        [JsonIgnore]
        public int CookingTimeMinutes { get; set; }
    }
}
