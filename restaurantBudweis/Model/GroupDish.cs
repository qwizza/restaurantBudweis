namespace restaurantBudweis.Model
{
    public class GroupDish : EFModel
    {
        public List<Dish> Dishs { get; set; } = new List<Dish>();
    }
}
