namespace BookShop.Data
{
    internal class Configuration
    {
        internal static string ConnectionStringWindows => @"Server=DESKTOP-R3F6I64\SQLEXPRESS;Database=BookShop;Integrated Security=True;";
        internal static string ConnectionStringMacOS => @"Server=localhost,1433;Database=BookShop;User Id=sa;Password=EmanuelaTop1.;";
    }
}
