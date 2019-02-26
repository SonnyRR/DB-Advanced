namespace VillianNames
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

                string commandAsString = @"SELECT v.Name, COUNT(mv.VillainId) AS MinionsCount  
                                           FROM Villains AS v 
                                           JOIN MinionsVillains AS mv ON v.Id = mv.VillainId 
                                           GROUP BY v.Id, v.Name 
                                           HAVING COUNT(mv.VillainId) > 3 
                                           ORDER BY COUNT(mv.VillainId)";
                                
                using (SqlCommand command = new SqlCommand(commandAsString, connection))
                {
                    SqlDataReader data = command.ExecuteReader();

                    while (data.Read())
                    {
                        var name = (string)data[0];
                        var count = (int)data[1];
                        Console.WriteLine($"{name} - {count}");
                    }
                }
            }
        }
    }
}
