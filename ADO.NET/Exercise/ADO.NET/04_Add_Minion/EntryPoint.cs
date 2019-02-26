namespace AddMinion
{
    using System;
    using System.Data.SqlClient;

    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {
            string[] minionArgs = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            string villainName = Console.ReadLine()
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)[1];

            string minionTown = minionArgs[3];
            int minionAge = int.Parse(minionArgs[2]);
            string minionName = minionArgs[1];

            using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
            {

                connection.Open();

                bool townExists = CheckIfTownExists(minionTown, connection);

                if (townExists == false)
                {
                    int affectedRows = AddTown(minionTown, connection);
                    Console.WriteLine($"Town {minionTown} was added to the database.");
                }

                if (CheckIfVillainExists(villainName, connection) == false)
                {
                    int affectedRows = AddVillain(villainName, connection);
                    Console.WriteLine($"Villain {villainName} was added to the database.");
                }

                // It's good to check if null, but don't have time for that, it's a simople exercise.
                int? townId = GetTownId(minionTown, connection);
                int? villainId = GetVillainId(villainName, connection);

                int minionInsertRowsAffected = InsertMinion(minionName, minionAge, townId, connection);

                int? minionId = GetMinionId(minionName, connection);

                int mapMinionToVillainRowsAffected = MapMinionToVillain(minionId, villainId, connection);

                if (mapMinionToVillainRowsAffected > 0)
                {
                    Console.WriteLine($"Successfully added {minionName} to be minion of {villainName}.");
                    Environment.Exit(0);
                }

            }
        }

        public static bool CheckIfTownExists(string townName, SqlConnection connection)
        {
            bool exists = false;
            string findTownCmd = "SELECT Id FROM Towns WHERE Name = @name";

            using (SqlCommand findTown = new SqlCommand(findTownCmd, connection))
            {
                findTown.Parameters.AddWithValue("@name", townName);
                int? id = (int?)findTown.ExecuteScalar();

                exists = id == null ? false : true;
            
            }

            return exists;
        }

        public static int AddTown(string townName, SqlConnection connection)
        {
            string addTownCmd = "INSERT INTO Towns(Name) VALUES(@townName)";
            int rowsAffected = 0;

            using (SqlCommand addTown = new SqlCommand(addTownCmd, connection))
            {
                addTown.Parameters.AddWithValue("@townName", townName);

                rowsAffected = addTown.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        public static bool CheckIfVillainExists(string villainName, SqlConnection connection)
        {
            bool exists = false;

            string findVillainCmd = "SELECT Id FROM Villains WHERE Name = @Name";

            using (SqlCommand findVillain = new SqlCommand(findVillainCmd, connection))
            {
                findVillain.Parameters.AddWithValue("@Name", villainName);
                int? id = (int?)findVillain.ExecuteScalar();

                exists = id == null ? false : true;
            }

            return exists;
        }

        public static int AddVillain(string villainName, SqlConnection connection)
        {
            string addVillainCmd = "INSERT INTO Villains(Name, EvilnessFactorId)  VALUES(@villainName, 4)";
            int rowsAffected = 0;

            using (SqlCommand addVillain = new SqlCommand(addVillainCmd, connection))
            {
                addVillain.Parameters.AddWithValue("@villainName", villainName);

                rowsAffected = addVillain.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        public static int? GetTownId(string townName, SqlConnection connection)
        {
            string getTownCmd = "SELECT Id FROM Towns WHERE Name = @townName";

            int? id = null;

            using (SqlCommand getTown = new SqlCommand(getTownCmd, connection))
            {
                getTown.Parameters.AddWithValue("@townName", townName);
                id = (int?)getTown.ExecuteScalar();
            }

            return id;
        }

        public static int? GetVillainId(string villainName, SqlConnection connection)
        {
            string getVillainCmd = "SELECT Id FROM Villains WHERE Name = @villainName";

            int? id = null;

            using (SqlCommand getVillain = new SqlCommand(getVillainCmd, connection))
            {
                getVillain.Parameters.AddWithValue("@villainName", villainName);
                id = (int?)getVillain.ExecuteScalar();
            }

            return id;
        }

        public static int? GetMinionId(string minionName, SqlConnection connection)
        {
            string getMinionCmd = "SELECT Id FROM Minions WHERE Name = @minionName";

            int? id = null;

            using (SqlCommand getMinion = new SqlCommand(getMinionCmd, connection))
            {
                getMinion.Parameters.AddWithValue("@minionName", minionName);
                id = (int?)getMinion.ExecuteScalar();
            }

            return id;
        }

        public static int InsertMinion(string name, int age, int? townId, SqlConnection connection)
        {
            string insertCmd = "INSERT INTO Minions(Name, Age, TownId) VALUES(@name, @age, @townId)";

            int rowsAffected = 0;

            using (SqlCommand insertMinion = new SqlCommand(insertCmd, connection))
            {
                insertMinion.Parameters.AddWithValue("@name", name);
                insertMinion.Parameters.AddWithValue("@age", age);
                insertMinion.Parameters.AddWithValue("@townId", townId);

                rowsAffected = insertMinion.ExecuteNonQuery();
            }

            return rowsAffected;
        }

        public static int MapMinionToVillain(int? minionId, int? villainId, SqlConnection connection)
        {
            string mapCmd = "INSERT INTO MinionsVillains(MinionId, VillainId) VALUES(@minionId, @villainId)";

            int rowsAffected = 0;

            using (SqlCommand map = new SqlCommand(mapCmd, connection))
            {
                map.Parameters.AddWithValue("@villainId", villainId);
                map.Parameters.AddWithValue("@minionId", minionId);

                rowsAffected = map.ExecuteNonQuery();
            }

            return rowsAffected;
        }
    }
}
