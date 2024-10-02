using static System.Math;
namespace PrimeCalc.Math 
{
    public class PrimeChecker
    {
        public static bool IsPrime(int number){
            const double EPS = 0.001;
            for(int i = 2; i < Sqrt(number) + EPS; i++)
            {
                if(number % i == 0) return false;
            }
            return true;
        }
    }
}