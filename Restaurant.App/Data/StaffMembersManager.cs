using Restaurant.App.Data.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Restaurant.App.Data
{
    public class StaffMembersManager
    {
        public async Task<List<StaffMember>> GetStaffMembersAsync()
        {
            string query = "SELECT * FROM Staff";
            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    List<StaffMember> members = new List<StaffMember>();
                    while (await reader.ReadAsync())
                    {
                        members.Add(new StaffMember
                        {
                            Id = reader.GetInt32(0),
                            PositionId = reader.GetInt32(1),
                            FullName = reader.GetString(2),
                            Passport = reader.GetInt32(3),
                            City = reader.GetString(4),
                            Street = reader.GetString(5),
                            House = reader.GetString(6),
                            Flat = reader.GetInt32(7),
                            Age = reader.GetInt32(8),
                        });
                    }
                    return members;
                }
            }
        }

        public async Task<IEnumerable<Position>> GetPositionsAsync()
        {
            string query = "SELECT idPositions, name FROM Positions";
            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                using (SqlDataReader reader = await cmd.ExecuteReaderAsync())
                {
                    List<Position> positions = new List<Position>();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            positions.Add(new Position
                            {
                                Id = reader.GetInt32(0),
                                Name = reader.GetString(1),
                            });
                        }
                    }
                    return positions;
                }
            }
        }

        public async Task<bool> EditStaffMemberAsync(
            int id,
            int? positionId,
            string fullName,
            int? passport,
            string city,
            string street,
            string house,
            int? flat,
            int? age)
        {
            string query = string.Format(@"UPDATE Staff SET
                idPositions = {0},
                fullName = '{1}',
                passportNumber = {2},
                city = '{3}',
                street = '{4}',
                house = '{5}',
                flat = {6},
                age = {7}
                WHERE idStaff = {8}",
                positionId,
                fullName,
                passport,
                city,
                street,
                house,
                flat,
                age,
                id);

            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> DeleteStaffMemberAsync(int id)
        {
            string query = "DELETE FROM Staff WHERE idStaff = " + id;
            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }

        public async Task<bool> AddStaffMemberAsync(
            int? positionId,
            string fullName,
            int? passport,
            string city,
            string street,
            string house,
            int? flat,
            int? age)
        {
            string query = string.Format(
          @"INSERT INTO Staff(
                    idPositions,
                    fullName,
                    passportNumber,
                    city,
                    street,
                    house,
                    flat,
                    age)
                VALUES 
                ({0},'{1}',{2}, '{3}','{4}','{5}', {6}, {7})",
                positionId,
                fullName,
                passport,
                city,
                street,
                house,
                flat,
                age);

            using (SqlCommand cmd = new SqlCommand(query, DatabaseManager.Instance.Connection))
            {
                int rowsAffected = await cmd.ExecuteNonQueryAsync();
                return rowsAffected > 0;
            }
        }
    }
}
