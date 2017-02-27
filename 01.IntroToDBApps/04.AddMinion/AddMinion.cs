namespace _04.AddMinion
{
    using System;
    using System.Linq;
    using System.Data.SqlClient;

    public class AddMinion
    {
        public static void Main()
        {
            Console.Write("Minion: ");
            string[] minionArgs = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                .ToArray();
            Console.Write("Villain: ");
            string villainName = Console.ReadLine();

            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();

            using (connection)
            {
                int townId = GetTownId(connection, minionArgs[2]);
                if (townId == -1)
                {
                    InsertTown(connection, minionArgs[2]);
                    townId = GetTownId(connection, minionArgs[2]);
                }

                int villainId = GetVillainId(connection, villainName);
                if (villainId == -1)
                {
                    InsertVillain(connection, villainName);
                    villainId = GetVillainId(connection, villainName);
                }

                InsertMinion(connection, minionArgs[0], int.Parse(minionArgs[1]), townId, villainName, villainId);
            }
        }

        //returns -1 if no town is found
        private static int GetTownId(SqlConnection connection, string townName)
        {
            int townId = -1;
            string getTownId = @"USE MinionsDB
                    SELECT Id
                    FROM Towns
                    WHERE TownName = @TownName";

            SqlCommand getTownCmd = new SqlCommand(getTownId, connection);
            SqlParameter townNameParam = new SqlParameter("@TownName", townName);
            getTownCmd.Parameters.Add(townNameParam);
            SqlDataReader getTownReader = getTownCmd.ExecuteReader();

            if (getTownReader.Read())
            {
                townId = (int)getTownReader[0];
            }

            getTownReader.Close();

            return townId;
        }

        private static void InsertTown(SqlConnection connection, string townName)
        {
            string insertTown = @"USE MinionsDB
                        INSERT INTO Towns(TownName, CountryName) VALUES
                        (@TownName, NULL)";

            SqlCommand insertTownCmd = new SqlCommand(insertTown, connection);
            SqlParameter insertTownParam = new SqlParameter("@TownName", townName);
            insertTownCmd.Parameters.Add(insertTownParam);

            if (insertTownCmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Town {townName} was added to the database.");
            }
            else
            {
                throw new Exception("An error occurred while trying to add minion's town.");
            }
        }

        //returns -1 if no villain is found
        private static int GetVillainId(SqlConnection connection, string name)
        {
            int villainId = -1;
            string getVillain = @"USE MinionsDB
                    SELECT Id
                    FROM Villians
                    WHERE Name = @VillainName";

            SqlCommand getVillainCmd = new SqlCommand(getVillain, connection);
            SqlParameter getVillainParam = new SqlParameter("@VillainName", name);
            getVillainCmd.Parameters.Add(getVillainParam);
            SqlDataReader villainReader = getVillainCmd.ExecuteReader();

            if (villainReader.Read())
            {
                villainId = (int)villainReader[0];
            }

            villainReader.Close();

            return villainId;
        }

        private static void InsertVillain(SqlConnection connection, string villainName)
        {
            string insertVillain = @"USE MinionsDB
                    INSERT INTO Villians(Name, EvilnessFactor) VALUES
                    (@VillainName, 'evil')";

            SqlCommand insertVillainCmd = new SqlCommand(insertVillain, connection);
            SqlParameter insertVillainParam = new SqlParameter("@VillainName", villainName);
            insertVillainCmd.Parameters.Add(insertVillainParam);

            if (insertVillainCmd.ExecuteNonQuery() > 0)
            {
                Console.WriteLine($"Villain {villainName} was added to the database.");
            }
            else
            {
                throw new Exception("An error occurred while trying to add minion's villain.");
            }
        }

        //returns -1 if no minion is found
        private static int GetMinionId(SqlConnection connection, string name)
        {
            int minionId = -1;
            string getMinion = @"USE MinionsDB
                    SELECT Id
                    FROM Minions
                    WHERE Name = @MinionName";

            SqlCommand getMinionCmd = new SqlCommand(getMinion, connection);
            SqlParameter getMinionParam = new SqlParameter("@MinionName", name);
            getMinionCmd.Parameters.Add(getMinionParam);
            SqlDataReader minionReader = getMinionCmd.ExecuteReader();

            if (minionReader.Read())
            {
                minionId = (int)minionReader[0];
            }

            minionReader.Close();

            return minionId;
        }

        private static void InsertMinion(SqlConnection connection, string name, int age, int townId, string villainName, int villainId)
        {
            string insertMinion = @"USE MinionsDB
                    INSERT INTO Minions(Name, Age, TownId) VALUES
                    (@MinionName, @MinionAge, @MinionTown)";

            SqlCommand insertMinionCmd = new SqlCommand(insertMinion, connection);

            SqlParameter nameParam = new SqlParameter("@MinionName", name);
            insertMinionCmd.Parameters.Add(nameParam);

            SqlParameter ageParam = new SqlParameter("@MinionAge", age);
            insertMinionCmd.Parameters.Add(ageParam);

            SqlParameter townIdParam = new SqlParameter("@MinionTown", townId);
            insertMinionCmd.Parameters.Add(townIdParam);

            if (insertMinionCmd.ExecuteNonQuery() > 0)
            {
                int minionId = GetMinionId(connection, name);
                if (minionId == -1)
                {
                    throw new Exception("An error occurred while trying to add minion.");
                }

                InsertVillainMinionPair(connection, minionId, villainId);
                Console.WriteLine($"Successfully added {name} to be minion of {villainName}.");
            }
            else
            {
                throw new Exception("An error occurred while trying to add minion.");
            }
        }

        private static void InsertVillainMinionPair(SqlConnection connection, int minionId, int villainId)
        {
            string insertVillainMinion = @"USE MinionsDB
                    INSERT INTO VilliansMinions(VillianId, MinionId) VALUES
                    (@VillainId, @MinionId)";

            SqlCommand insertCmd = new SqlCommand(insertVillainMinion, connection);

            SqlParameter villainParam = new SqlParameter("@VillainId", villainId);
            insertCmd.Parameters.Add(villainParam);

            SqlParameter minionParam = new SqlParameter("@MinionId", minionId);
            insertCmd.Parameters.Add(minionParam);

            if (insertCmd.ExecuteNonQuery() == 0)
            {
                throw new Exception("An error occurred while trying to add minion.");
            }
        }
    }
}