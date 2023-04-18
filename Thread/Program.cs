using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Thread_
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<string> numbers = new List<string>() { "Привет",", ", "как", "дела","?" };

            Thread thread = new Thread(() =>
            {
                foreach (string number in numbers)
                {
                    Console.Write($" {number}");
                }
            });

            thread.Start();

            thread.Join();

            Console.ReadLine();
        }
    }
}
