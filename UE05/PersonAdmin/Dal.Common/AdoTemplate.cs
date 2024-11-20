using Dal.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PersonAdmin.Common;

public delegate T RowMapper<T>(IDataRecord row);
public class AdoTemplate(IConnectionFactory connectionFactory)
{

    private void AddParameters(DbCommand command, QueryParameter[] parameters)
    {
        foreach (var p in parameters)
        {
            DbParameter dbParam = command.CreateParameter();
            dbParam.ParameterName = p.Name;
            dbParam.Value = p.Value;

            command.Parameters.Add(dbParam);
        }

    }

    public async Task<IEnumerable<T>> QueryAsync<T>(string sql, RowMapper<T> rowMapper, params QueryParameter[] parameters)
    {
        await using DbConnection connection = await connectionFactory.CreateConnectionAsync();

        //using SqlCommand command = new SqlCommand("select * from person", connection);
        await using DbCommand command = connection.CreateCommand();
        command.Connection = connection;
        command.CommandText = sql;
        AddParameters(command, parameters);

        //using SqlDataReader reader = command.ExecuteReader();
        await using DbDataReader reader = await command.ExecuteReaderAsync();

        IList<T> persons = [];
        while (await reader.ReadAsync())
        {
            persons.Add(rowMapper(reader));
        }

        return persons;
    }

    public async Task<T?> QuerySingleAsync<T>(string sql, RowMapper<T> rowMapper, params QueryParameter[] parameters)
    {
        return (await QueryAsync(sql, rowMapper, parameters)).SingleOrDefault();
    }

    public async Task<int> ExecuteAsync(string sql, params QueryParameter[] parameters)
    {
        await using DbConnection connection = await connectionFactory.CreateConnectionAsync();

        await using DbCommand command = connection.CreateCommand();
        command.Connection = connection;
        command.CommandText = sql;
        AddParameters(command, parameters);

        return await command.ExecuteNonQueryAsync();
    }
}
