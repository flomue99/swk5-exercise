namespace PersonManagement;

public class PersonRepository
{
    private readonly IList<Person> persons = [];

    public void AddPerson(Person person)
    {
        this.persons.Add(person);
    }

    public void AddPersons(IEnumerable<Person> persons)
    {
        //version 1
        //foreach (var person in persons)
        //{
        //    this.AddPerson(person);
        //}

        //version 2
        //CollectionExtensions.AddAll<Person>(this.persons, persons);

        //version 3
        this.persons.AddAll(persons);
    }

    public void PrintPersons(TextWriter textWriter)
    {
        //version 1
        //foreach (var person in this.persons)
        //{
        //    textWriter.WriteLine(person);
        //}

        //version 2
        //Action<Person> action = delegate (Person p) { textWriter.WriteLine(p); };
        //this.persons.ForEach(action);

        //version 3
        //this.person.ForEach(delegate (Person p) { textWriter.WriteLine(p); };

        //version 4
        //this.persons.ForEach(p => textWriter.WriteLine(p));

        //version 5
        this.persons.ForEach(textWriter.WriteLine);
    }

    public IEnumerable<(string?, string?)> GetPersonNames()
    {
        //version 1
        //foreach (var person in this.persons)
        //{
        //    yield return (person.FirstName, person.LastName);
        //}

        //version 2
        return this.persons.Map((Person P) => (P.FirstName, P.LastName));
    }

    public IEnumerable<Person> FindPersonsByCity(string city)
    {
        IList<Person> personList = [];
        //version 1
        //foreach (Person person in this.persons)
        //{
        //    if(person.City == city)
        //    {
        //        personList.Add(person);
        //    }
        //}
        //return personList;

        //version 2
        //foreach (Person person in this.persons)
        //{
        //    if (person.City == city)
        //    {
        //        yield return person;
        //    }
        //}

        //version 3
        return this.persons.Filter((Person p) => p.City == city);
    }

    public Person FindYoungestPerson()
    {
       return this.persons.MaxBy((Person p1, Person p2) => p2.DateOfBirth.CompareTo(p1.DateOfBirth));
    }


    public IEnumerable<Person> FindPersonsSortedByAgeAscending()
    {
        return null;
    }
}
