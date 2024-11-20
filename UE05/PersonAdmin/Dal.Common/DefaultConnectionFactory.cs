namespace Dal.Common;

using System.Data.Common;
using System.Threading.Tasks;
using Dal.Common;
using Microsoft.Extensions.Configuration;

public class DefaultConnectionFactory : IConnectionFactory
{
    private readonly DbProviderFactory dbProviderFactory;

    public static IConnectionFactory FromConfiguration(IConfiguration configuration, string connectionConfigName, string providerConfigName)
    {
        (string connectionString, string providerName) =
          ConfigurationUtil.GetConnectionParameters(configuration, connectionConfigName, providerConfigName);
        return new DefaultConnectionFactory(connectionString, providerName);
    }

    public DefaultConnectionFactory(string connectionString, string providerName)
    {
        this.ConnectionString = connectionString;
        this.ProviderName = providerName;

        DbUtil.RegisterAdoProviders();
        this.dbProviderFactory = DbProviderFactories.GetFactory(providerName);
    }

    public string ConnectionString { get; }

    public string ProviderName { get; }

    public async Task<DbConnection> CreateConnectionAsync()
    {
        var connection = dbProviderFactory.CreateConnection();
        if(connection is null)
        {
            throw new InvalidOperationException("dbProviderFactory.CreateConnection is null.");
        }
        connection.ConnectionString = ConnectionString;
        await connection.OpenAsync();

        return connection;
    }
}
