using System.Linq;
using System.Text;

namespace SoftUni
{
    using System;
    using SoftUni.Data;
    using SoftUni.Models;

    public class StartUp
    {
        public static void Main()
        {
            // DB Scaffold powershell cmdlet:
            // Scaffold-DbContext -Connection "Server=DESKTOP-R3F6I64\SQLEXPRESS;Database=SoftUni;Integrated Security=True;" -Provider Microsoft.EntityFrameworkCore.SqlServer -OutputDir Data/Models

            using (SoftUniContext context = new SoftUniContext())
            {
                string output = GetEmployeesFullInformation(context);
                Console.WriteLine(output);               
            }
        }

        public static string GetEmployeesFullInformation(SoftUniContext context)
        {
            StringBuilder builder = new StringBuilder();

            context.Employees
                .Select(x => new
                {
                    FirstName = x.FirstName,
                    MiddleName = x.MiddleName,
                    LastName = x.LastName,
                    JobTitle = x.JobTitle,
                    Salary = x.Salary
                })
                .ToList()
                .ForEach(
                    x => builder.AppendLine($"{x.FirstName} {x.LastName} {x.MiddleName} {x.JobTitle} {x.Salary:F2}")
                );

            return builder.ToString();
        }
    }
}
