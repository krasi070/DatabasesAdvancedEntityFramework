namespace _08.IncreaseMinionsAge
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class IncreaseMinionsAge
    {
        public static void Main()
        {
            Console.Write("Minion IDs: ");
            int[] ids = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(int.Parse)
                .ToArray();
            List<string[]> minions = new List<string[]>();

            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();
            using (connection)
            {
                IncreaseMinionAge(connection, ids);
                minions = GetMinions(connection);
            }

            for (int i = 0; i < minions.Count; i++)
            {
                string name = minions[i][1];
                int id = int.Parse(minions[i][0]);
                if (ids.Contains(id))
                {
                    name = name[0].ToString().ToUpper() + name.Substring(1).ToLower();
                }

                Console.WriteLine($"{name} {minions[i][2]}");
            }
        }

        public static void IncreaseMinionAge(SqlConnection connection, int[] ids)
        {
            string updateMinions = @"USE MinionsDB
                UPDATE Minions
                SET Age = Age + 1
                WHERE ID IN (" + string.Join(", ", ids) + ")";

            SqlCommand updateCmd = new SqlCommand(updateMinions, connection);
            updateCmd.ExecuteNonQuery();
        }

        public static List<string[]> GetMinions(SqlConnection connection)
        {
            List<string[]> minions = new List<string[]>();

            string getMinions = @"USE MinionsDB
                SELECT Id, Name, Age
                FROM Minions
                ORDER BY Id";

            SqlCommand getMinionsCmd = new SqlCommand(getMinions, connection);

            SqlDataReader reader = getMinionsCmd.ExecuteReader();
            while (reader.Read())
            {
                minions.Add(new string[] { reader[0].ToString(), reader[1].ToString(), reader[2].ToString() });
            }

            reader.Close();

            return minions;
        }
    }
}