namespace _09_Increase_Age_SP
{
    using System;
    using System.Data.SqlClient;

    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {
            int desiredId = int.Parse(Console.ReadLine());

            using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
            {
                connection.Open();

                // useless in this exercise
                SqlTransaction tran = connection.BeginTransaction();

                using (SqlCommand command = new SqlCommand())
                {
                    string sqlCmd = @"EXEC usp_GetOlder @id";
                    command.Transaction = tran;
                    command.Parameters.AddWithValue("@id", desiredId);
                    command.Connection = connection;
                    command.CommandText = sqlCmd;
                    try
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            reader.Read();
                            Console.WriteLine($"{reader[0]} - {reader[1]} years old");
                        }

                        tran.Commit();
                    }

                    catch (Exception ex)
                    {
                        tran.Rollback();
                    }
                }
            }
        }
    }
}
