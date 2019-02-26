namespace MinionNames
{
    using System;
    using System.Data.SqlClient;

    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {                      

            using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
            {
                connection.Open();

                int desiredId = int.Parse(Console.ReadLine());

                string searchVillainCmdAsString = "SELECT Name FROM Villains WHERE Id = @Id";
                string getResultsCmdAsString = @"SELECT ROW_NUMBER() OVER (ORDER BY m.Name) as RowNum,
                                                         m.Name, 
                                                         m.Age
                                                    FROM MinionsVillains AS mv
                                                    JOIN Minions As m ON mv.MinionId = m.Id
                                                   WHERE mv.VillainId = @Id
                                                ORDER BY m.Name";

                
                string villainName = string.Empty;

                using (SqlCommand getVillain = new SqlCommand(searchVillainCmdAsString, connection))
                {
                    getVillain.Parameters.AddWithValue("@Id", desiredId);

                    string data = (string)getVillain.ExecuteScalar();

                    if (data == null)
                    {
                        Console.WriteLine($"No villain with ID {desiredId} exists in the database.");
                        return;
                    }

                    villainName = data;
                }

                using (SqlCommand getResults = new SqlCommand(getResultsCmdAsString, connection))
                {
                    getResults.Parameters.AddWithValue("@Id", desiredId);

                    SqlDataReader data = getResults.ExecuteReader();
                    Console.WriteLine($"Villain name: {villainName}");
                    bool hasRows = data.HasRows;
                     
                    if (hasRows == false)
                    {
                        Console.WriteLine("(no minions)");
                        Environment.Exit(0);
                    }

                    while (data.Read())
                    { 
                        Console.WriteLine($"{(long)data[0]}. {(string)data[1]} {(int)data[2]}");
                    }

                }

            }
        }
    }
}
