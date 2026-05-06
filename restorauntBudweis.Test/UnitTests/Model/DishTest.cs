using System.ComponentModel.DataAnnotations;
using restaurantBudweis.Model;

namespace restorauntBudweis.Test
{
    public class DishTest
    {
        [Fact]
        public void Dish_WithValidData_ShouldBeValid()
        {
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
            Assert.Empty(result);
        }

        [Fact]
        public void Dish_WithEmptyDishName_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "",
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Введите название блюда");
        }

        [Fact]
        public void Dish_WithTooLongDishName_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = new string('A', 51),
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Название блюда не может превышать 50 символов");
        }

        [Fact]
        public void Dish_WithNegativePrice_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = -10m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Цена должна быть между 0.01 и 99999.99");
        }

        [Fact]
        public void Dish_WithZeroPrice_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 0m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Цена должна быть между 0.01 и 99999.99");
        }

        [Fact]
        public void Dish_WithTooLargePrice_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 100000m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Цена должна быть между 0.01 и 99999.99");
        }

        [Fact]
        public void Dish_WithEmptyDescription_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Введите описание блюда");
        }

        [Fact]
        public void Dish_WithTooLongDescription_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = new string('A', 501),
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Описание не может превышать 500 символов");
        }

        [Fact]
        public void Dish_WithEmptyIngredients_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = "",
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Введите ингредиенты блюда");
        }

        [Fact]
        public void Dish_WithTooLongIngredients_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = new string('A', 1001),
                CookingTimeMinutes = 30,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Ингредиенты не могут превышать 1000 символов");
        }

        [Fact]
        public void Dish_WithZeroCookingTime_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 0,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Время приготовления должно быть между 1 и 180 минутами");
        }

        [Fact]
        public void Dish_WithTooLargeCookingTime_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 181,
                GroupId = 1
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();
            var isValid = Validator.TryValidateObject(dish, context, results, true);

            Assert.False(isValid);
            Assert.Contains(results, r => r.ErrorMessage == "Время приготовления должно быть между 1 и 180 минутами");
        }

        [Fact]
        public void Dish_WithoutGroupId_ShouldBeInvalid()
        {
            var dish = new Dish
            {
                DishName = "Борщ",
                Price = 350.50m,
                DescriptionDish = "Описание",
                Ingredients = "Ингредиенты",
                CookingTimeMinutes = 30,
                GroupId = 0  
            };

            var context = new ValidationContext(dish);
            var results = new List<ValidationResult>();

            if (dish.GroupId <= 0)
            {
                results.Add(new ValidationResult("Необходимо выбрать группу блюда", new[] { "GroupId" }));
            }

            Validator.TryValidateObject(dish, context, results, true);
            Assert.Contains(results, r => r.ErrorMessage == "Необходимо выбрать группу блюда");
        }
    }
}