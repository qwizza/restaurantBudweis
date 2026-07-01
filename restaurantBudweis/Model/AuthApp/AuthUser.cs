using System.ComponentModel.DataAnnotations;

namespace restaurantBudweis.Model.AuthApp
{
    public class AuthUser
    {
        [Key] // Обязательно для Entity Framework
        public int Id { get; set; }

        [Required]
        public string Email { get; set; } = string.Empty;

        [Required]
        public string Password { get; set; } = string.Empty;

        public string Role { get; set; } = "User";

        // Пункт 5 (на 5): Поле под путь к изображению
        public string AvatarUrl { get; set; } = "/avatars/default.png";
    }
}
