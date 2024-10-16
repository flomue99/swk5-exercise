using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Delegates
{

    public delegate void ExpiredEventHandler(DateTime expireTime);
    public class Timer
    {
        private const int DEFAULT_INTERVAL = 1000;
        private Thread ticker;

        public event ExpiredEventHandler? Expired;

        public Timer() => this.ticker = new Thread(OnTick);
        public void Start() => ticker.Start();
        public int Interval { get; init; } = DEFAULT_INTERVAL;
        private void OnTick()
        {
            while (true)
            {
                Thread.Sleep(Interval);
                Expired?.Invoke(DateTime.Now);
            }
        }

    }
}
