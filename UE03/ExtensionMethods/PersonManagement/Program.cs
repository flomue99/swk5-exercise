using PersonManagement;
using System.Text.Json;

PersonRepository personRepository = new PersonRepository();

try
{
    //using block for Dispose otherwise it will not be called
    using (var reader = new StreamReader("persons.json"))
    {
        string json = reader.ReadToEnd();

        IEnumerable<Person>? persons = JsonSerializer.Deserialize<IEnumerable<Person>>(
            json,
            new JsonSerializerOptions //Define serialize options
            {
                PropertyNamingPolicy = JsonNamingPolicy.KebabCaseLower
            });

        if (persons is null)
        {
            throw new Exception("Probems parsing persons.json");
        }

        personRepository.AddPersons(persons);
    }// reader.Dispose
}
catch (FileNotFoundException fnfEx)
{
    Console.WriteLine(fnfEx.Message);
    return;
}

using TextWriter textWriter = Console.Out;

textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Person list");
textWriter.WriteLine("=====================================================");
personRepository.PrintPersons(textWriter);

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Persons in Hagenberg");
textWriter.WriteLine("=====================================================");
foreach(Person person in personRepository.FindPersonsByCity("Hagenberg"))
{
    textWriter.WriteLine(person);
}


textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine("Person names");
textWriter.WriteLine("=====================================================");
foreach (var(first,last) in personRepository.GetPersonNames())
{
    textWriter.WriteLine($"{first}\t{last}");
}

textWriter.WriteLine();
textWriter.WriteLine("=====================================================");
textWriter.WriteLine($"Youngest person");
textWriter.WriteLine("=====================================================");
textWriter.WriteLine(personRepository.FindYoungestPerson());

//textWriter.WriteLine();
//textWriter.WriteLine("=====================================================");
//textWriter.WriteLine("Persons sorted by age ascending");
//textWriter.WriteLine("=====================================================");
//
// TODO
//
