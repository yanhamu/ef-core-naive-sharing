namespace ShardedApp.DataAccess
{
    public class AppTenant
    {
        public string ConnectionString { get; }

        public AppTenant(string baseConnectionString)
        {
            this.ConnectionString = baseConnectionString;
        }
    }
}
