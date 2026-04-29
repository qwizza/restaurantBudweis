using System.ComponentModel.DataAnnotations;

namespace restaurantBudweis.Model
{
    public class Client : EFModel
    {
        [Required(ErrorMessage = "Введите ФИО клиента")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "ФИО должно быть от 2 до 100 символов")]
        [RegularExpression(@"^[а-яА-Яa-zA-Z\s\-]+$", ErrorMessage = "ФИО может содержать только буквы, пробелы и дефисы")]
        public string? FullName { get; set; }

        [Required(ErrorMessage = "Введите номер телефона")]
        [RegularExpression(@"^(\+7|8)?[\s\-]?\(?[0-9]{3}\)?[\s\-]?[0-9]{3}[\s\-]?[0-9]{2}[\s\-]?[0-9]{2}$",
            ErrorMessage = "Введите корректный номер телефона (например: +7 999 123 45-67)")]
        [StringLength(20, MinimumLength = 10, ErrorMessage = "Номер телефона должен содержать от 10 до 20 символов")]
        public string? PhoneNumber { get; set; }

        [Required(ErrorMessage = "Введите номер столика")]
        [Range(1, 50, ErrorMessage = "Номер столика должен быть от 1 до 50")]
        public int TableNumber { get; set; }

        [Required(ErrorMessage = "Выберите дату посещения")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [CustomValidation(typeof(Client), nameof(ValidateVisitDate))]
        public DateTime VisitDate { get; set; } = DateTime.Today;

        // Валидация даты чтобы нельзя было поставить дату в прошлом 
        public static ValidationResult? ValidateVisitDate(DateTime visitDate, ValidationContext context)
        {
            if (visitDate < DateTime.Today)
            {
                return new ValidationResult("Дата посещения не может быть в прошлом");
            }
            return ValidationResult.Success;
        }
        [Required(ErrorMessage = "Выберите хотя бы одно блюдо")]
        [MinLength(1, ErrorMessage = "Необходимо выбрать хотя бы одно блюдо")]
        public List<Dish>? Dishs { get; set; } = new List<Dish>();
    }
}