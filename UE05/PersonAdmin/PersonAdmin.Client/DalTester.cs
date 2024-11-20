using Microsoft.Identity.Client;
using PersonAdmin.Dal.Interface;
using PersonAdmin.Domain;
using System;
using System.Transactions;

namespace PersonAdmin.Client;
internal class DalTester(IPersonDao personDao)
{
    private readonly IPersonDao personDao = personDao;
    public async void TestFindAllAsync()
    {
        (await personDao.FindAllAsync())
              .ToList()
              .ForEach(p => Console.WriteLine($"{p.Id,5} | {p.FirstName,-10} | {p.LastName,-15} | {p.DateOfBirth,10:yyyy-MM-dd} |"));
    }

    public async void TestFindByIdAsync()
    {
        Person? person1 = await personDao.FindByIdAsync(1);
        Console.WriteLine($"person1 -> {person1?.ToString() ?? "<null>"}");


        Person? person2 = await personDao.FindByIdAsync(999);
        Console.WriteLine($"person1 -> {person2?.ToString() ?? "<null>"}");

    }

    public async void TestUpdateAsync()
    {
        Person? person = await personDao.FindByIdAsync(1);
        Console.WriteLine($"Person before update -> {person?.ToString() ?? "<null>"}");
        if (person is null) return;

        person.DateOfBirth = person.DateOfBirth.AddYears(-1);
        await personDao.UpdateAsync(person);

        person = await personDao.FindByIdAsync(1);
        Console.WriteLine($"Person before update -> {person?.ToString() ?? "<null>"}");
    }

    public async void TestTransactionsAsync()
    {
        var person1 = await personDao.FindByIdAsync(1);
        var person2 = await personDao.FindByIdAsync(2);
        if (person1 is null || person2 is null) return;

        Console.WriteLine($"Person1 before transaction -> {person1.ToString() ?? "<null>"}");
        Console.WriteLine($"Person2 before transaction -> {person2.ToString() ?? "<null>"}");
        try
        {
            using (TransactionScope scope = new TransactionScope())
            {
                person1.DateOfBirth = person1.DateOfBirth.AddYears(-10);
                person2.DateOfBirth = person1.DateOfBirth.AddYears(+10);

                await personDao.UpdateAsync(person1);
                throw new Exception("Interrupted transaction.");
                await personDao.UpdateAsync(person2);

                scope.Complete(); //falls man hier her kommt wir ein commit durchgeführt anderseits ein rollback
            }
        }
        catch { }

        person1 = await personDao.FindByIdAsync(1);
        person2 = await personDao.FindByIdAsync(2);
        Console.WriteLine($"Person1 after transaction -> {person1?.ToString() ?? "<null>"}");
        Console.WriteLine($"Person2 after transaction -> {person2?.ToString() ?? "<null>"}");
    }
}
