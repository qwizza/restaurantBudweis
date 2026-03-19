namespace restaurantBudweis.Model
{
    public class Client : EFModel
    {
        public string? FullName { get; set; }
        public string? PhoneNumber { get; set; } 
        public int TableNumber { get; set; }
        public DateTime VisitDate { get; set; }
        public List<Dish>? Dishs { get; set; }
        
    }
}
