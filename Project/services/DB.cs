using System;
using System.Data.SQLite;
using System.IO;

namespace Project.services
{
    public sealed class DB : IDisposable
    {
        private static readonly Lazy<DB> instance = new Lazy<DB>(() => new DB());
        private readonly string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        private readonly string fullpath;
        private readonly string connectionString;
        private readonly SQLiteConnection conn;

        private DB()
        {
            fullpath = Path.Combine(dbPath, "database.db");
            connectionString = $"Data Source={fullpath};Version=3;";

            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }
            if (!File.Exists(fullpath))
            {
                using (File.Create(fullpath)) { }
            }

            conn = new SQLiteConnection(connectionString);
            conn.Open();
        }

        /// <summary>
        ///  Gets the instance of the database
        /// </summary>
        public static DB Instance => instance.Value;

        /// <summary>
        /// Executes a command on the db z.B. INSERT INTO User (username, password) VALUES ('John Doe', 'supersecred')
        /// </summary>
        /// <param name="query">comand Query z.B. CREATE TABLE (...)</param>
        /// <param name="parameters">(Optional) params</param>
        public void Execute(string query, params string[] parameters)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"@param{i}", parameters[i]);
                }

                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// looks up if the table already exists in the database
        /// </summary>
        /// <param name="tableName">name of the table you want to check</param>
        /// <returns>Returns if it exists or not</returns>
        public bool TableExists(string tableName)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            string query = "SELECT COUNT(*) FROM sqlite_master WHERE type='table' AND name=@tableName;";

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                cmd.Parameters.AddWithValue("@tableName", tableName);
                long count = (long)cmd.ExecuteScalar();
                return count > 0;
            }
        }

        /// <summary>
        /// Gets the data out of the database with the query
        /// </summary>
        /// <param name="query">the query to get the data z.B. SELECT * FROM ...</param>
        /// <param name="parameters">(Optional) params</param>
        /// <returns>returns string[,] of all the found database entries</returns>
        public string[,] Get(string query, params string[] parameters)
        {
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"@param{i}", parameters[i]);
                }
                SQLiteDataReader reader;

                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        int rowCount = 0;
                        int columnCount = reader.FieldCount;

                        while (reader.Read())
                        {
                            rowCount++;
                        }

                        reader.Close();
                        reader = cmd.ExecuteReader();

                        string[,] result = new string[rowCount, columnCount];

                        int rowIndex = 0;
                        while (reader.Read())
                        {
                            for (int colIndex = 0; colIndex < columnCount; colIndex++)
                            {
                                result[rowIndex, colIndex] = reader[colIndex]?.ToString() ?? string.Empty;
                            }
                            rowIndex++;
                        }

                        return result;
                    }
                }
            }

            return new string[0, 0]; 
        }


        public void Dispose()
        {
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
