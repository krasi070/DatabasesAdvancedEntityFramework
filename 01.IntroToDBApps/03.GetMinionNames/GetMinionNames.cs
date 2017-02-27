namespace _03.GetMinionNames
{
    using System;
    using System.Data.SqlClient;
    using System.IO;

    public class GetMinionNames
    {
        public static void Main()
        {
            Console.Write("Villian Id: ");
            int villianId = int.Parse(Console.ReadLine());
            string query = File.ReadAllText("../../GetMinions.sql");
            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();

            using (connection)
            {
                SqlCommand getMinionsCmd = new SqlCommand(query, connection);
                SqlCommand getVillianCmd = new SqlCommand("USE MinionsDB; SELECT Name FROM Villians WHERE Id = @VillianId", connection);
                SqlParameter villianIdParam1 = new SqlParameter("@VillianId", villianId);
                SqlParameter villianIdParam2 = new SqlParameter("@VillianId", villianId);
                getVillianCmd.Parameters.Add(villianIdParam1);
                getMinionsCmd.Parameters.Add(villianIdParam2);

                var villianReader = getVillianCmd.ExecuteReader();
                if (villianReader.Read())
                {
                    Console.WriteLine($"Villian: {villianReader["Name"]}");

                    villianReader.Close();

                    var minionReader = getMinionsCmd.ExecuteReader();
                    int count = 0;
                    while (minionReader.Read())
                    {
                        count++;
                        Console.WriteLine($"{count}. {minionReader["Name"]} {minionReader["Age"]}");
                    }

                    if (count == 0)
                    {
                        Console.WriteLine("(no minions)");
                    }
                }
                else
                {
                    Console.WriteLine($"No villain with ID {villianId} exists in the database.");
                }
            }
        }
    }
}