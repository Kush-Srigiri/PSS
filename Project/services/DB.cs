using System;
using System.Data.SQLite;
using System.IO;

namespace Project.services
{
    public sealed class DB : IDisposable
    {
        // fields
        private static readonly Lazy<DB> instance = new Lazy<DB>(() => new DB());
        private readonly string dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "data");
        private readonly string fullpath;
        private readonly string connectionString;
        private readonly SQLiteConnection conn;
        
        // Instance
                
        /// <summary>
        ///  Gets the instance of the database
        /// </summary>
        public static DB Instance => instance.Value;

        // Constructor
        private DB()
        {
            /// Constructor for the DB Class
            /// Creates an instance of an connection for the db to be single ton
            ///
            /// Execute for commands and Get for Results
            // Creates the full path of the storing for the db file
            fullpath = Path.Combine(dbPath, "database.db");
            // Creates the connection string for the sqlite db
            connectionString = $"Data Source={fullpath};Version=3;";

            // Creates the db file if it dosenet exist
            if (!Directory.Exists(dbPath))
            {
                Directory.CreateDirectory(dbPath);
            }
            if (!File.Exists(fullpath))
            {
                using (File.Create(fullpath)) { }
            }

            // Opens an connection to de db
            conn = new SQLiteConnection(connectionString);
            conn.Open();
        }

        // functions

        /// <summary>
        /// Executes a command on the db z.B. INSERT INTO User (username, password) VALUES ('John Doe', 'supersecred')
        /// </summary>
        /// <param name="query">comand Query z.B. CREATE TABLE (...)</param>
        /// <param name="parameters">(Optional) params</param>
        public void Execute(string query, params string[] parameters)
        {
            /// Execute function that has a query and params for that query
            ///
            /// Opens an connection if the connection is lost somehow
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }
            
            /// Execute the command with all the params on to the db
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
            /// Get function is there to get data from the database
            ///
            /// Opens an connection if the connection is lost somehow
            if (conn.State != System.Data.ConnectionState.Open)
            {
                conn.Open();
            }

            /// Creates a SQLite Command for the execution of a command like SELECT * FROM ...
            using (SQLiteCommand cmd = new SQLiteCommand(query, conn))
            {
                // Add all the parameters to the command
                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.AddWithValue($"@param{i}", parameters[i]);
                }
                SQLiteDataReader reader;

                // start reading from the Database
                using (reader = cmd.ExecuteReader())
                {
                    if (reader.HasRows)
                    {
                        // Get the count of the rows and collumns
                        int rowCount = 0;
                        int columnCount = reader.FieldCount;

                        while (reader.Read())
                        {
                            rowCount++;
                        }

                        reader.Close();
                        reader = cmd.ExecuteReader();

                        // Start reading from the Databases data
                        string[,] result = new string[rowCount, columnCount];

                        int rowIndex = 0;
                        while (reader.Read())
                        {
                            // Add all the data to the result
                            for (int colIndex = 0; colIndex < columnCount; colIndex++)
                            {
                                result[rowIndex, colIndex] = reader[colIndex]?.ToString() ?? string.Empty;
                            }
                            rowIndex++;
                        }

                        // return the result
                        return result;
                    }
                }
            }
            // if something is not working the return en empty string[0,0] array
            return new string[0, 0]; 
        }


        /// <summary>
        /// Disposes the db class
        /// </summary>
        public void Dispose()
        {
            // Delets the connection
            if (conn != null)
            {
                conn.Close();
                conn.Dispose();
            }
        }
    }
}
