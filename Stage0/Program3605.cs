using System;
namespace Stage0
{
    partial class Program
    {
        static void Main(string[] args)
        {
            Welcome3605();
            Welcome9370();

            Console.ReadKey();
        }
        static partial void Welcome9370();

        private static void Welcome3605()
        {
            Console.WriteLine("Enter your name: ");
            string? name = Console.ReadLine();
            Console.WriteLine("{0},welcome to my first console application", name);
        }
    }
}