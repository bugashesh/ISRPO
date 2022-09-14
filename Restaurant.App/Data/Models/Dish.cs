using System;

namespace Restaurant.App.Data.Models
{
    public class Dish
    {
        public int? Id { get; set; }
        public int? IngredientId { get; set; }
        public string Name { get; set; }
        public int? ServingSize { get; set; }
        public int? Cost { get; set; }
        public TimeSpan? CookingTime { get; set; }
    }
}
