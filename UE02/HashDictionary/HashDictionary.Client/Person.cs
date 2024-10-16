namespace HashDictionary.Client
{
    internal class PersonClass (string name, DateTime dob)
    {
        public string Name { get; init; } = name;
        public DateTime DateOfBirth { get; init; } = dob;
    }
}
