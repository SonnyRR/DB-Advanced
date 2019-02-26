namespace RemoveVillain
{
    using System;
    using System.Data.SqlClient;

    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {

            int villainId = int.Parse(Console.ReadLine());

            string villainName = string.Empty;
            int removedMinions = 0;

            try
            {
                using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
                {
                    connection.Open();

                    using (SqlCommand getVillainName = new SqlCommand())
                    {
                        string sqlCmd = "SELECT Name FROM Villains WHERE Id = @villainId";
                        getVillainName.CommandText = sqlCmd;
                        getVillainName.Parameters.AddWithValue("@villainId", villainId);
                        getVillainName.Connection = connection;

                        villainName = (string)getVillainName.ExecuteScalar();
                    }

                    if (villainName == null)
                    {
                        throw new ApplicationException("No such villain was found!");
                    }

                    using (SqlCommand removeFromMappingTable = new SqlCommand())
                    {
                        string sqlCmd = "DELETE FROM MinionsVillains WHERE VillainId = @villainId";
                        removeFromMappingTable.CommandText = sqlCmd;
                        removeFromMappingTable.Parameters.AddWithValue(@"villainId", villainId);
                        removeFromMappingTable.Connection = connection;

                        removedMinions = removeFromMappingTable.ExecuteNonQuery();
                    }

                    using (SqlCommand removeVillain = new SqlCommand())
                    {
                        string sqlCmd = "DELETE FROM Villains WHERE Id = @villainId";
                        removeVillain.CommandText = sqlCmd;
                        removeVillain.Parameters.AddWithValue("@villainId", villainId);
                        removeVillain.Connection = connection;

                        removeVillain.ExecuteNonQuery();
                    }
                }

                Console.WriteLine($"{villainName} was deleted.");
                Console.WriteLine($"{removedMinions} were released.");
            }

            catch (ApplicationException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(0);
            }
        }
    }
}
