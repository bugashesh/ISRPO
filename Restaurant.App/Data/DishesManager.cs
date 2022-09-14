using Restaurant.App.Data.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Restaurant.App.Data
{
    public class DishesManager
    {
        public async Task<IEnumerable<Ingredient>> GetIngredientsAsync()
        {
            string query = "SELECT idIngredient, name FROM Warehouse";
            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                List<Ingredient> ingredients = new List<Ingredient>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        ingredients.Add(new Ingredient
                        {
                            Id = reader.GetInt32(0),
                            Name = reader.GetString(1),
                        });
                    }
                    return ingredients;
                }
            }
        }

        public async Task<bool> CreateDishAsync(
            int? ingredientId,
            string name,
            int? servingSize,
            int? cost,
            int? cookingTimeMins
            )
        {
            string query = string.Format(@"INSERT INTO Menu(
                idIngredient,
                name,
                servingSize,
                cost,
                cookingTime) VALUES 
                ({0}, '{1}', {2}, {3}, '{4}')",
                ingredientId,
                name,
                servingSize,
                cost,
                TimeSpan.FromMinutes(cookingTimeMins ?? 0));

            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<List<Dish>> GetDishesAsync()
        {
            string query = "SELECT * FROM Menu";
            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                List<Dish> dishes = new List<Dish>();
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        dishes.Add(new Dish
                        {
                            Id = reader.GetInt32(0),
                            IngredientId = reader.GetInt32(1),
                            Name = reader.GetString(2),
                            ServingSize = reader.GetInt32(3),
                            Cost = reader.GetInt32(4),
                            CookingTime = reader.GetTimeSpan(5),
                        });
                    }
                    return dishes;
                }
            }
        }

        public async Task<bool> UpdateDishAsync(
            int id,
            int? ingredientId,
            string name,
            int? servingSize,
            int? cost,
            int? cookingTimeMins)
        {
            string query = string.Format(@"UPDATE Menu SET
                idIngredient = {0},
                name = '{1}',
                servingSize = {2},
                cost = {3},
                cookingTime = '{4}'
                WHERE idDishes = {5}",
               ingredientId,
               name,
               servingSize,
               cost,
               TimeSpan.FromMinutes(cookingTimeMins ?? 0),
               id);

            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteDishAsync(int id)
        {
            string query = "DELETE FROM Menu WHERE idDishes = " + id;
            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
