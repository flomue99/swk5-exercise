using Dal.Common;
using Microsoft.Extensions.Configuration;
using PersonAdmin.Client;
using PersonAdmin.Dal.Ado;
using PersonAdmin.Dal.Simple;

static void PrintTitle(string text = "", int length = 60, char ch = '-')
{
    int preLen = (length - (text.Length + 2)) / 2;
    int postLen = length - (preLen + text.Length + 2);
    Console.WriteLine($"{new String(ch, preLen)} {text} {new String(ch, postLen)}");
}


PrintTitle("SimplePersonDao.FindALl");
var tester1 = new DalTester(new SimplePersonDao());
tester1.TestFindAllAsync();

PrintTitle("SimplePersonDao.FindById");
tester1.TestFindByIdAsync();

PrintTitle("SimplePersonDao.Update");
tester1.TestUpdateAsync();

IConfiguration configuration = ConfigurationUtil.GetConfiguration();
IConnectionFactory connectionFactory = DefaultConnectionFactory.FromConfiguration(configuration, "PersonDbConnection", "ProviderName");

PrintTitle("AdoPersonDao.FindAll");
var tester2 = new DalTester(new AdoPersonDao(connectionFactory)); //Dependency Injection -> Verwende ist zuständign dem DalTester zus agen welche impl, er verwenden soll
tester2.TestFindAllAsync();


PrintTitle("AdoPersonDao.FindById");
tester2.TestFindByIdAsync();

PrintTitle("AdoPersonDao.Update");
tester2.TestUpdateAsync();

PrintTitle("AdoPersonDao.Transaction");
tester2.TestTransactionsAsync();