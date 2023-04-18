using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace test2
{
    class Bank
    {
        private int money;
        private string name;
        private int percent;
        private string FileName = "bank.txt";

        private readonly object fileLock = new object();

        private Thread moneyThread;
        private Thread nameThread;
        private Thread percentThread;

        public int Money
        {
            get { return money; }
            set
            {
                money = value;
                if (moneyThread == null || !moneyThread.IsAlive)
                {
                    moneyThread = new Thread(() => Record("Money", money));
                    moneyThread.Start();
                }
            }
        }
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                if (nameThread == null || !nameThread.IsAlive)
                {
                    nameThread = new Thread(() => Record("Name", name));
                    nameThread.Start();
                }
            }
        }
        public int Percent
        {
            get { return percent; }
            set
            {
                percent = value;
                if (percentThread == null || !percentThread.IsAlive)
                {
                    percentThread = new Thread(() => Record("Percent", percent));
                    percentThread.Start();
                }
            }
        }

        private void Record(string propertyName, object value)
        {
            lock (fileLock)
            {
                using (StreamWriter writer = new StreamWriter(FileName, true))
                {
                    writer.WriteLine($"{propertyName} -> {value}");
                }
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            Bank bank = new Bank();

            bank.Money = 100;
            bank.Name = "Privat Bank";
            bank.Percent = 10;
            bank.Money = 350;
        }
    }
}
