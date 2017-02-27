namespace _07.PrintAllMinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class PrintAllMinionNames
    {
        public static void Main()
        {
            List<string> minions = new List<string>();

            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();
            using (connection)
            {
                minions = GetMinions(connection);
            }

            for (int i = 0; i < minions.Count; i += 2)
            {
                Console.WriteLine(minions[i]);
            }

            for (int i = (minions.Count / 2) * 2 - 1; i >= 0; i -= 2)
            {
                Console.WriteLine(minions[i]);
            }
        }

        public static List<string> GetMinions(SqlConnection connection)
        {
            List<string> minions = new List<string>();

            string getMinions = @"USE MinionsDB
                SELECT Name
                FROM Minions
                ORDER BY Id";

            SqlCommand getMinionsCmd = new SqlCommand(getMinions, connection);

            SqlDataReader reader = getMinionsCmd.ExecuteReader();
            while (reader.Read())
            {
                minions.Add(reader[0].ToString());
            }

            reader.Close();

            return minions;
        }
    }
}
