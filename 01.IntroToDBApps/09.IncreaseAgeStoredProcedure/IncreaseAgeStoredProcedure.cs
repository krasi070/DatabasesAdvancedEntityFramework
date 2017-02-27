namespace _09.IncreaseAgeStoredProcedure
{
    using System;
    using System.Data.SqlClient;

    public class IncreaseAgeStoredProcedure
    {
        public static void Main()
        {
            Console.Write("Minion ID: ");
            int id = int.Parse(Console.ReadLine());

            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();
            using (connection)
            {
                string executeGetOlder = @"USE MinionsDB; EXEC usp_GetOlder " + id;
                SqlCommand executeProcedureCmd = new SqlCommand(executeGetOlder, connection);
                executeProcedureCmd.ExecuteNonQuery();

                string[] minion = GetMinionById(connection, id);
                Console.WriteLine($"{minion[0]} {minion[1]}");
            }
        }

        public static string[] GetMinionById(SqlConnection connection, int id)
        {
            string getMinion = @"USE MinionsDB
                SELECT Name, Age
                FROM Minions
                WHERE Id = @Id";

            SqlCommand getMinionCmd = new SqlCommand(getMinion, connection);
            SqlParameter idParam = new SqlParameter("@Id", id);
            getMinionCmd.Parameters.Add(idParam);

            SqlDataReader reader = getMinionCmd.ExecuteReader();
            if (reader.Read())
            {
                return new string[] { reader[0].ToString(), reader[1].ToString() };
            }

            reader.Close();

            return null;
        }
    }
}