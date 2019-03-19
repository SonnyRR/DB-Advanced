namespace EmployeesMapping.Data
{
    internal class ConnectionInfo
    {
        internal static string ConnectionStringWindows => @"Server=DESKTOP-R3F6I64\SQLEXPRESS;Database=EmployeeMapping;Integrated Security=True;";
        internal static string ConnectionStringMacOS => @"Server=localhost,1433;Database=EmployeeMapping;User Id=sa;Password=EmanuelaTop1.;";

    }
}
