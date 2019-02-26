namespace ChangeTownNamesCasing
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;

    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {

            string desiredCountry = Console.ReadLine();
            IList<string> townNames = new List<string>();

            int changedRows = 0;
            using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
            {
                connection.Open();

                int? countryId = GetDesiredCountryId(desiredCountry, connection);
                changedRows = ChangeTownNamesCasing(countryId, connection);
                GetTowns(countryId, connection, townNames);
            }

            Console.WriteLine($"{changedRows} town names were affected.");
            Console.WriteLine($"[{string.Join(", ", townNames)}]");
        }

        public static int? GetDesiredCountryId(string name, SqlConnection connection)
        {
            string cmd = "SELECT Id FROM Countries WHERE Name = @name";

            int? id = null;

            using (SqlCommand getId = new SqlCommand(cmd, connection))
            {
                getId.Parameters.AddWithValue("@name", name);

                id = (int?)getId.ExecuteScalar();
            }

            return id;
        }

        public static int ChangeTownNamesCasing(int? Id, SqlConnection connection)
        {
            string cmd = @"UPDATE Towns
                           SET Name = UPPER(Name)
                           WHERE CountryCode = (SELECT TOP(1) Id FROM Countries WHERE Id = @id)";

            int rowsAffected = 0;

            using (SqlCommand changeCasing = new SqlCommand(cmd, connection))
            {
                changeCasing.Parameters.AddWithValue("@id", Id);
                rowsAffected = changeCasing.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        public static void GetTowns(int? countryId, SqlConnection connection, IList<string> list)
        {
            string cmd = @" SELECT t.Name 
                            FROM Towns as t
                            JOIN Countries AS c ON c.Id = t.CountryCode
                            WHERE c.Id = @id";

            using (SqlCommand getTowns = new SqlCommand(cmd, connection))
            {
                getTowns.Parameters.AddWithValue("@id", countryId);

                using (SqlDataReader reader = getTowns.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        list.Add((string)reader[0]);
                    }
                }
            }
        }
    }
}