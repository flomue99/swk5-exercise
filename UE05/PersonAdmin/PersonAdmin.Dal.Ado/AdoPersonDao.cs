using Dal.Common;
using PersonAdmin.Common;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System.Data;
using System.Data.Common;

namespace PersonAdmin.Dal.Ado;
public class AdoPersonDao(IConnectionFactory connectionFactory) : IPersonDao
{
    private readonly AdoTemplate template = new AdoTemplate(connectionFactory);

    private RowMapper<Person> RowToPersonMapper =
        (IDataRecord row) =>
          new(
            id: (int)row["Id"],
            firstName: (string)row["first_name"],
            lastName: (string)row["last_name"],
            dateOfBirth: (DateTime)row["date_of_birth"]
            );

    public async Task<IEnumerable<Person>> FindAllAsync()
    {
        return await template.QueryAsync("select * from person", RowToPersonMapper);
    }

    public async Task<Person?> FindByIdAsync(int id)
    {
        return await template.QuerySingleAsync(
            $"select * from person where id = @id",
            RowToPersonMapper, new QueryParameter("id", id));
    }

    public async Task<bool> UpdateAsync(Person person)
    {
        const string SQL_UPDATE = "update person set first_name = @fn, last_name = @ln, date_of_birth = @dob where id = @id";
        return await template.ExecuteAsync(SQL_UPDATE,
                          new QueryParameter("@id", person.Id),
                          new QueryParameter("@fn", person.FirstName),
                          new QueryParameter("@ln", person.LastName),
                          new QueryParameter("@dob", person.DateOfBirth)) == 1;
    }
}