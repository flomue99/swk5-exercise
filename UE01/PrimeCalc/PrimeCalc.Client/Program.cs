using PrimeCalc.Math;
using Newtonsoft.Json;
using PrimeCalc.Client.Resources;
using System.Globalization;
using PrimeCalc.Client;

const int MAX = 50;

static void PrintPrimesAsJson(int limit)
{
    IList<int> primes = new List<int>();
    for (int i = 2; i <= limit; i++)
    {

        if (PrimeChecker.IsPrime(i))
        {
            primes.Add(i);
        }
    }

    string json = JsonConvert.SerializeObject(
        new
        {
            Count = primes.Count,
            Primes = primes
        });

    Console.WriteLine($"{Messages.PrimeInRange} [{2}], {limit}: {json}]");
}

Console.WriteLine($"{Messages.WelcomeMessage}");
var limit = args.Length == 1 ? int.Parse(args[0]) : MAX;
PrintPrimesAsJson(limit);
