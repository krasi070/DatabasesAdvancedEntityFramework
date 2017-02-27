namespace _01.InitialSetup
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class InitialSetup
    {
        public static void Main()
        {
            string createDB = "CREATE DATABASE MinionsDB";
            string query = File.ReadAllText("../../MinionsDBInit.sql");
            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();
            SqlCommand createDBCommand = new SqlCommand(createDB, connection);
            SqlCommand createTablesCommand = new SqlCommand(query, connection);

            using (connection)
            {
                createDBCommand.ExecuteNonQuery();
                Console.WriteLine("affected rows ({0})", createTablesCommand.ExecuteNonQuery());
            }
        }
    }
}