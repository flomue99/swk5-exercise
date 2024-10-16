using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{
    internal class Introduction
    {
        delegate void Procedure(); //dahinter steht ein Datentyp
        delegate int StringToIntFunction(string s1);

        static  void SayHello() => Console.WriteLine("Hello world");
        
        public static void Test()
        {
            Procedure p1 = SayHello;

            p1(); //calls all from invokation list
            Procedure? p2 = null; // ? null values are ok for p2
            p2?.Invoke(); //only call Invoke if p2 is not null
            p2 = SayHello;
            p2?.Invoke();

            Action? p3 = null; // Action is a predefined Delegate
            p3?.Invoke();
            p3 = SayHello;
            p3?.Invoke();

            StringToIntFunction f1 = int.Parse;
            Console.WriteLine(f1("12345"));

            Func<string, int> f2 = int.Parse;
            Console.WriteLine(f2("12345"));
        }
    }
}
