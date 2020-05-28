using System;

namespace Lab7
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;

            Console.Clear();
            Console.WriteLine("Выберите задание (4 - Выход)");
            string numTask = String.Empty;
            Task1 taskFirst = new Task1();
            Task2 taskSecond = new Task2();
            try
            {
                numTask = Console.ReadLine();
                switch (numTask)
                {
                    case "1":
                        Console.Clear();
                        taskFirst.Choose(ref taskFirst.data);
                        Console.ReadKey();
                        break;
                    case "2":
                        Console.Clear();
                        taskSecond.ChooseMethodOfSorting();
                        Console.ReadKey();
                        break;
                    case "4":
                        Environment.Exit(0);
                        break;
                }
                Program.Main(args);
            }
            catch
            {
                Program.Main(args);
            }
        }
    }
}
