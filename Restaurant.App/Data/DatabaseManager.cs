using System;
using System.Data;
using System.Data.SqlClient;

namespace Restaurant.App.Data
{
    public class DatabaseManager : IDisposable
    {
        private static DatabaseManager instance;

        private DatabaseManager()
        {
            Connection = new SqlConnection(Config.ConnString);
            Connection.Open();
        }

        public static DatabaseManager Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DatabaseManager();
                }
                return instance;
            }
        }

        public readonly SqlConnection Connection;

        public void Dispose()
        {
            if (Connection.State != ConnectionState.Closed)
            {
                Connection.Close();
            }
        }
    }
}
