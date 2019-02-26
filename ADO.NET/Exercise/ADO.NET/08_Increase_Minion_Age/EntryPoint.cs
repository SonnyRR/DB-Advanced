namespace IncreaseMinionAge
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {
            // Trim to forbid special characters because I am lazy
            string minionIdentificators = Console.ReadLine()
                .Replace(" ", ", ")
                .Trim('O', 'R', '=', 'A', 'N', 'D', '\'', ',', ' ');


            int ageUpdateRowsAffected = 0;

            List<string> minions = new List<string>();

            using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
            {
                connection.Open();

                using (SqlCommand increaseAge = new SqlCommand())
                {
                    string sqlCmd = 
                        $@"UPDATE Minions SET Name = UPPER(LEFT(Name, 1)) + SUBSTRING(Name, 2, LEN(Name)), Age += 1 WHERE Id IN ({minionIdentificators})";

                    increaseAge.CommandText = sqlCmd;
                    increaseAge.Connection = connection;
                    //increaseAge.Parameters.AddWithValue("@params", minionIdentificators);
                    ageUpdateRowsAffected = increaseAge.ExecuteNonQuery();
                }

                using (SqlCommand getMinions = new SqlCommand())
                {
                    string sqlCmd = @"SELECT [Name], Age FROM Minions";
                    getMinions.CommandText = sqlCmd;
                    getMinions.Connection = connection;

                    using (SqlDataReader rowSetReader = getMinions.ExecuteReader())
                    {
                        while (rowSetReader.Read())
                        {
                            minions.Add($"{rowSetReader[0]} {rowSetReader[1]}");
                        }
                    }
                }
            }

            Console.WriteLine(string.Join(Environment.NewLine, minions));
        }
    }
}
