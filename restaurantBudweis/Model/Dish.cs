using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace restaurantBudweis.Model
{
    public class Dish : EFModel
    {
        [Required(ErrorMessage = "Введите название блюда")]
        [StringLength(50, ErrorMessage = "Название блюда не может превышать 50 символов")]
        public string DishName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите цену блюда")]
        [Range(0.01, 99999.99, ErrorMessage = "Цена должна быть между 0.01 и 99999.99")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "Введите описание блюда")]
        [StringLength(500, ErrorMessage = "Описание не может превышать 500 символов")]
        public string DescriptionDish { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите ингредиенты блюда")]
        [StringLength(1000, ErrorMessage = "Ингредиенты не могут превышать 1000 символов")]
        public string Ingredients { get; set; } = string.Empty;

        [Required(ErrorMessage = "Введите время приготовления")]
        [Range(1, 180, ErrorMessage = "Время приготовления должно быть между 1 и 180 минутами")]
        public int CookingTimeMinutes { get; set; }

        [Required(ErrorMessage = "Необходимо выбрать группу блюда")]
        public int GroupId { get; set; }  

        public Group? Group { get; set; }  
    }
}
    

