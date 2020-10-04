using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Console;

namespace AgentExample
{
    class Program
    {
        static void Main(string[] args)
        {
            // Stateless Agent
            IAgent<string> logger = Agent.Start((string msg) => WriteLine(msg));
            logger.Tell("Agent X");

            IAgent<string> ping, pong = null;
            // Stateless Agent
            ping = Agent.Start((string msg) =>
            {
                if (msg == "STOP")
                {
                    logger.Tell($"Received '{msg}'");
                    return;
                }

                logger.Tell($"Received '{msg}'; Sending 'PING'");
                Task.Delay(500).Wait();
                pong.Tell("PING");
            });

            // Stateful Agent
            pong = Agent.Start(0, (int count, string msg) =>
            {
                int newCount = count + 1;
                string nextMsg = (newCount < 5) ? "PONG" : "STOP";

                logger.Tell($"Received '{msg}' #{newCount}; Sending '{nextMsg}'");
                Task.Delay(500).Wait();
                ping.Tell(nextMsg);

                return newCount;
            });

            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ping.Tell("START");

            stopwatch.Stop();
            Console.WriteLine($"{stopwatch.Elapsed}");

            Console.WriteLine("Press any key to continue. . .");
            Console.ReadKey();
        }
    }
}
