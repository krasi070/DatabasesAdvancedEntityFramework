namespace _02.GetVilliansNames
{
    using System;
    using System.IO;
    using System.Data.SqlClient;

    public class GetVilliansNames
    {
        public static void Main()
        {
            string query = File.ReadAllText("../../GetVillianNames.sql");
            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();
            SqlCommand villianNamesQuery = new SqlCommand(query, connection);

            using (connection)
            {
                var reader = villianNamesQuery.ExecuteReader();

                while (reader.Read())
                {
                    Console.WriteLine($"{(string)reader["Name"]} {(int)reader["MinionsCount"]}");
                }
            }
        }
    }
}