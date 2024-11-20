using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System.Data;

namespace PersonAdmin.Dal.Simple;

public class SimplePersonDao : IPersonDao
{
    private static IList<Person> personList = new List<Person>
   {
     new (id: 1, firstName: "John", lastName: "Doe",        dateOfBirth: DateTime.Now.AddYears(-10)),
     new (id: 2, firstName: "Jane", lastName: "Doe",        dateOfBirth: DateTime.Now.AddYears(-20)),
     new (id: 3, firstName: "Max",  lastName: "Mustermann", dateOfBirth: DateTime.Now.AddYears(-30))
   };

    public async Task<IEnumerable<Person>> FindAllAsync()
    {
        return await Task.FromResult<IEnumerable<Person>>(personList);
    }

    public async Task<Person?> FindByIdAsync(int id)
    {
        return await Task.FromResult<Person?>(personList.SingleOrDefault(p => p.Id == id));
    }

    public async Task<bool> UpdateAsync(Person person)
    {
        var p = await FindByIdAsync(person.Id);
        if (p is null) return false;

        p.FirstName = person.FirstName;
        p.LastName = person.LastName;
        p.DateOfBirth = person.DateOfBirth;
        return true;
    }
}
