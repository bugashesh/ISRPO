using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Restaurant.App.Data
{
    public class AuthManager
    {
        public async Task<bool> AuthorizeUserAsync(string login, string password)
        {
            string query = string.Format(
                "SELECT * FROM Users WHERE login='{0}' AND password='{1}'",
                login,
                password);

            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    return reader.HasRows;
                }
            }
        }

        public async Task<bool> RegisterUserAsync(string login, string password)
        {
            string query = string.Format(
                "INSERT INTO Users(login,password) VALUES ('{0}','{1}')",
                login,
                password);

            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected == 1;
            }
        }
    }
}
