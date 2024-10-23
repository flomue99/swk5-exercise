using LinqSamples.Data;

var repository = new CustomerRepository();
IEnumerable<Customer> customers = repository.GetCustomers();


Console.WriteLine("===================== Big Customers =====================");
//will be compiled into the code from underneath
IEnumerable<Customer> bigCustomer = from c in customers
                                    where c.Revenue >= 1_000_000
                                    orderby c.Revenue descending
                                    select c;

Print(bigCustomer);

var bigCustomers2 = customers
    .Where(c => c.Revenue >= 1_000_000)
    .OrderByDescending(c => c.Revenue)
    .Select(c => c.Revenue);

Console.WriteLine("===================== Customers with inital letter 'A' =====================");

var customersStartingWithA = from c in customers
                             where c.Name.StartsWith("A", StringComparison.InvariantCultureIgnoreCase)
                             select c;

Print(customersStartingWithA);


Console.WriteLine("===================== Customers with specific name =====================");

//Customer? customer = (from c in customers
//                      where c.Name == "biofix"
//                      select c)
//               .FirstOrDefault();


//Customer? customer = customers.FirstOrDefault(c=>c.Name == "biofix"); //does not check for multiple occurrence
Customer? customer = customers.SingleOrDefault(c => c.Name == "biofix"); //checks for multiple occurrence -> throws exception

Console.WriteLine(customer);

Console.WriteLine("===================== Top 3 customers =====================");

var top3Customers = (from c in customers
                     orderby c.Revenue descending
                     select c).Take(3);

Print(top3Customers);

Console.WriteLine("===================== Show effect of deffered execution =====================");


var customersA = from c in customers
                 where c.Rating == Rating.A
                 select c;

//Print(customersA);

var oldACustomers = from c in customersA
                    where c.YearOfFoundation < 2000
                    select c;

Print(oldACustomers);

Console.WriteLine("============= Average revenue of A customers =============");
var avgRevenueACustomers = (from c in customers
                            where c.Rating == Rating.A
                            select c).Average(c => c.Revenue);

Console.WriteLine($"Avreage revenue {avgRevenueACustomers:F2}");

Console.WriteLine("-----------------------------------------");
var avgRevenueACustomers2 = customers.Where(c => c.Rating == Rating.A).Average(c => c.Revenue);
Console.WriteLine($"Avreage revenue {avgRevenueACustomers2:N2}");

Console.WriteLine("============= Grouping w.r.t country =============");

var countryGroups = from c in customers
                    group c by c.Location.Country into g
                    select new
                    {
                        Country = g.Key,
                        Customers = (IEnumerable<Customer>)g
                    };

foreach (var group in countryGroups.OrderBy(g => g.Country))
{
    Console.WriteLine(group.Country);
    foreach (var c in group.Customers)
    {
        Console.WriteLine($"   {c}");
    }
}

static void Print(IEnumerable<Customer> customers)
{
    foreach (var customer in customers)
    {
        Console.WriteLine(customer);
    }
}