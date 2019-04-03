namespace VaporStore.Data
{
    public static class Configuration
    {

#warning Connection strings should be stored in config files, but since this is for an exercis and it does not contain sensitive information I keep them here;

        public static string ConnectionString =
            @"Server=DESKTOP-R3F6I64\SQLEXPRESS;Database=VaporStore;Trusted_Connection=True";

        public static string ConnectionStringMacOS =
            @"Server=localhost,1433;Database=VaporStore;User Id=sa;Password=EmanuelaTop1.";

    }
}