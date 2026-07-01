using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using restaurantBudweis.Data;
using restaurantBudweis.Model;


namespace restorauntBudweis.Test
{
    public class CreateModelTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task OnPost_ShouldReturnPage_WhenModelStateIsInvalid()
        {
            // Arrange
            var context = GetDbContext();
            var pageModel = new restaurantBudweis.Pages.Dishs.CreateModel(context, null);
            pageModel.ModelState.AddModelError("DishName", "Required");

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            result.Should().BeOfType<PageResult>(); 
            context.Dishs.Count().Should().Be(0);
        }

        [Fact]
        public async Task OnPost_ShouldCreateDish_WhenModelStateIsValid()
        {
            // Arrange
            var context = GetDbContext();
            var pageModel = new restaurantBudweis.Pages.Dishs.CreateModel(context, null)
            {
                Dish = new Dish
                {
                    DishName = "Борщ",
                    Price = 350.50m,
                    DescriptionDish = "Традиционный украинский борщ",
                    Ingredients = "Свекла, капуста, картофель",
                    CookingTimeMinutes = 60,
                    GroupId = 1
                }
            };

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            result.Should().BeOfType<RedirectToPageResult>();
        }
    }
}
