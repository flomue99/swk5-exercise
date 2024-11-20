using NullableReferenceTypes;

static IEnumerable<Person> GetPersons()
{
    return null;
}

static void PrintPersons(IEnumerable<Person> persons)
{
    foreach (var person in persons)
    {
        Console.WriteLine(person);
    }
}


var person = new Person("Franz", "Huber");
//person.FirstName = "Franzi";
person.LastName = "Huber-Mayr";

if (person.FirstName is not null)
{
    var firstNameUpper = person.FirstName.ToUpper();
}

var lastNameUpper = person.LastName.ToUpper();

