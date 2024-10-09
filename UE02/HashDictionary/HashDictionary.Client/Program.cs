using HashDictionary.Impl;

IDictionary<string, int> testIndexerAndAdd()
{
    var cityInfo = new HashDictionary<string, int>();
    try
    {
        cityInfo["Hagenberg"] = 2_500;
        cityInfo["Linz"] = 180_000;
        cityInfo["Linz"] = 200_000;
        cityInfo.Add("Wien", 1_500_000);
        cityInfo.Add("Wien", 2_000_000);
    }
    catch (ArgumentException ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }

    try
    {
        Console.WriteLine($"cityInfo[\"Hagenberg\"] = {cityInfo["Hagenberg"]}");
        Console.WriteLine($"cityInfo[\"Linz\"] = {cityInfo["Linz"]}");
        Console.WriteLine($"cityInfo[\"Wien\"] = {cityInfo["Wien"]}");
        Console.WriteLine($"cityInfo[\"Graz\"] = {cityInfo["Graz"]}");
    }
    catch (KeyNotFoundException ex)
    {
        Console.WriteLine($"Exception: {ex.Message}");
    }

    return cityInfo;
}


void TestEnumerator(IDictionary<string, int> cityInfo)
{
    Console.WriteLine("------------------------------------");

    var e = cityInfo.GetEnumerator();
    while (e.MoveNext())
    {
        Console.WriteLine($"{e.Current.Key} -> {e.Current.Value}");
    }

    Console.WriteLine("------------------------------------");

    foreach (KeyValuePair<string, int> pair in cityInfo)
    {
        Console.WriteLine($"{pair.Key} -> {pair.Value}");
    }

    Console.WriteLine("------------------------------------");

    foreach (var (k, v) in cityInfo)
    {
        Console.WriteLine($"{k} -> {v}");
    }
}

var cityInfo = testIndexerAndAdd();
TestEnumerator(cityInfo);