using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;

namespace Delegates
{
    internal class TimerTest
    {
        //with $"{}" : can used as formater
        private static void OnExpired1(DateTime t) => Console.WriteLine($"OnExpired1: Timer expired at {t:HH:mm:ss:fff}");
        private static void OnExpired2(DateTime t) => Console.WriteLine($"OnExpired2: Hello from timer");
        public static void Test()
        {
            var timer = new Timer { Interval = 500 };

            timer.Expired += OnExpired1;
            timer.Expired += OnExpired2;
            timer.Expired += TimerExpired;
            // NOT POSSIBLE AFTER ADDING event to the ExpiredEventHandler
            //timer.Expired = OnExpired1;
            //timer.Expired?.Invoke(new DateTime(1800, 1, 1));

            timer.Expired += delegate (DateTime t)
            {
                Console.WriteLine("Anonymous method invoked");
            };

            timer.Expired += (DateTime t) => Console.WriteLine($"Lamda expression: Time = {t:HH:mm:ss:fff}");

            timer.Start();

            Console.WriteLine("After starting timer");
        }

        private static void TimerExpired(DateTime t)
        {
            Console.WriteLine($"TimerExpired: Timer expired at {t:HH:mm:ss:fff}");
        }
    }
}
