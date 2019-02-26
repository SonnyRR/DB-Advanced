namespace MinionNames
{
    using System;
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using System.Linq;
    using SetupInfo;

    public class EntryPoint
    {
        public static void Main()
        {
            IList<string> names = new List<string>();

            using (SqlConnection connection = new SqlConnection(Setup.CON_STRING))
            {
                connection.Open();
            
                string getMinionsSql = @"SELECT [Name] FROM Minions";
            
                using (SqlCommand command = new SqlCommand(getMinionsSql, connection))
                {
                    using (SqlDataReader rowSetReader = command.ExecuteReader())
                    {
                        while (rowSetReader.Read())
                        {
                            names.Add((string)rowSetReader[0]);
                        }
                    }
                }
            }

            names = OrderNamesInList(names);
            string output = string.Join(Environment.NewLine, names);
            Console.WriteLine(output);
        }

        public static IList<string> OrderNamesInList(IList<string> input)
        {
            IList<string> result = new List<string>();
            IList<string> reversed = input.Reverse().ToList();

            for (int i = 0; i < input.Count / 2; i++)
            {
                result.Add(input[i]);
                result.Add(reversed[i]);
            }

            if (input.Count % 2 != 0)
            {
                result.Add(input[input.Count / 2]);
            }
            
            return result;
        }
    }
}
