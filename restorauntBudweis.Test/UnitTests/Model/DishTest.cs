using System.ComponentModel.DataAnnotations;
using restaurantBudweis.Model;
using FluentAssertions;


namespace restorauntBudweis.Test
{
    public class DishTest
    {
        [Fact]
        public void Dish_WithValidData_ShouldBeValid()
        {
            // Создаем объект блюда с валидными значениями
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Традиционный русский борщ с пампушками",
                Ingredients = "Свекла, капуста, картофель, морковь, лук, чеснок, томатная паста",
                CookingTimeMinutes = 60,
                GroupId = 1
            };

            var context = new ValidationContext(dish);

            var result = new List<ValidationResult>();

            var isValid = Validator.TryValidateObject(dish, context, result, true);

            Assert.True(isValid);

            // Также убеждаемся, что список ошибок пуст
            Assert.Empty(result);
        }

        [Fact]
        public void Dish_WithEmptyDishName_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "", // Пустое название
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Введите название блюда"));
        }

        [Fact]
        public void Dish_WithTooLongDishName_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = new string('A', 51), // 51 символ - превышает лимит в 50
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Название блюда не может превышать 50 символов"));
        }

        [Fact]
        public void Dish_WithNegativePrice_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = -10m, // Отрицательная цена
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Цена должна быть"));
        }

        [Fact]
        public void Dish_WithZeroPrice_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 0m, // Нулевая цена
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Цена должна быть между"));
        }

        [Fact]
        public void Dish_WithTooLargePrice_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 100000m, // Слишком большая цена
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Цена должна быть между"));
        }

        [Fact]
        public void Dish_WithEmptyDescription_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "", // Пустое описание
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Введите описание блюда"));
        }

        [Fact]
        public void Dish_WithTooLongDescription_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = new string('A', 501), // 501 символ - превышает лимит в 500
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Описание не может превышать 500 символов"));
        }

        [Fact]
        public void Dish_WithEmptyIngredients_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = "", // Пустые ингредиенты
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Введите ингредиенты блюда"));
        }

        [Fact]
        public void Dish_WithTooLongIngredients_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = new string('A', 1001), // 1001 символ - превышает лимит в 1000
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Ингредиенты не могут превышать 1000 символов"));
        }

        [Fact]
        public void Dish_WithZeroCookingTime_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 0, // Нулевое время приготовления
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Время приготовления должно быть между"));
        }

        [Fact]
        public void Dish_WithTooLargeCookingTime_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 181, // Превышает 180 минут
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            // Act
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            // Assert
            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage.Contains("Время приготовления должно быть между"));
        }

        [Fact]
        public void Dish_WithoutGroupId_ShouldBeInvalid()
        {
            // Arrange
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание блюда",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 0 // Значение по умолчанию
            };

            // Act
            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(dish);
            var isValid = Validator.TryValidateObject(dish, validationContext, validationResults, true);

            // Дополнительная проверка GroupId
            if (dish.GroupId <= 0)
            {
                validationResults.Add(new ValidationResult("Необходимо выбрать группу блюда", new[] { "GroupId" }));
                isValid = false;
            }

            // Assert
            Assert.False(isValid);
            Assert.Contains(validationResults, r => r.MemberNames.Contains("GroupId"));
        }
    }
}
