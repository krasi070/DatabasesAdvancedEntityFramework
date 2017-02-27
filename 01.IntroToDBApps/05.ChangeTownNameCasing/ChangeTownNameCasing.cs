namespace _05.ChangeTownNameCasing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Data.SqlClient;

    public class ChangeTownNameCasing
    {
        public static void Main()
        {
            Console.Write("Country: ");
            string country = Console.ReadLine();

            SqlConnection connection = new SqlConnection("Server=DESKTOP-1ML7UC9;Integrated Security=true;");
            connection.Open();
            using (connection)
            {
                var towns = GetTownsThatHaveLowerCaseLetters(connection, country).Select(t => t.ToUpper());
                int affectedRows = ChangeTownsToUpperCase(connection, country);
                Console.WriteLine($"{affectedRows} town names were affected.\n[{string.Join(", ", towns)}]");
            }
        }

        private static List<string> GetTownsThatHaveLowerCaseLetters(SqlConnection connection, string country)
        {
            string getTowns = @"USE MinionsDB
                SELECT TownName
                FROM Towns
                WHERE CountryName = @Country AND CONVERT(varbinary(max), TownName) != CONVERT(varbinary(max), UPPER(TownName))";

            SqlCommand getTownsCmd = new SqlCommand(getTowns, connection);
            SqlParameter countryParam = new SqlParameter("@Country", country);
            getTownsCmd.Parameters.Add(countryParam);

            List<string> towns = new List<string>();
            SqlDataReader reader = getTownsCmd.ExecuteReader();
            while (reader.Read())
            {
                towns.Add(reader[0].ToString());
            }

            reader.Close();

            return towns;
        }

        private static int ChangeTownsToUpperCase(SqlConnection connection, string country)
        {
            string update = @"USE MinionsDB
                UPDATE Towns
                SET TownName = UPPER(TownName)
                WHERE CountryName = @Country AND CONVERT(varbinary(max), TownName) != CONVERT(varbinary(max), UPPER(TownName))";

            SqlCommand updateCmd = new SqlCommand(update, connection);
            SqlParameter countryParam = new SqlParameter("@Country", country);
            updateCmd.Parameters.Add(countryParam);

            return updateCmd.ExecuteNonQuery();
        }
    }
}