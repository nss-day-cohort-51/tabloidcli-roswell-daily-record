using System;

namespace TabloidCLI.UserInterfaceManagers
{
    class BackgroundColorManager : IUserInterfaceManager
    {
        private readonly IUserInterfaceManager _parentUI;
        public BackgroundColorManager(IUserInterfaceManager parentUI)
        {
            _parentUI = parentUI;
        }

        public IUserInterfaceManager Execute()
        {
            Console.WriteLine("Please chose a background color");
            Console.WriteLine("1) Black");
            Console.WriteLine("2) Red");
            Console.WriteLine("3) Blue");
            Console.WriteLine("4) DarkMagenta");
            Console.WriteLine("5) Green");
            Console.WriteLine("6) Yellow");
            Console.WriteLine("7) DarkBlue");
            Console.WriteLine("8) DarkGrey");
            Console.WriteLine("0) Go Back");

            Console.Write(">");
            string choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.BackgroundColor = ConsoleColor.Black;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "2":
                    Console.BackgroundColor = ConsoleColor.Red;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "3":
                    Console.BackgroundColor = ConsoleColor.Blue;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "4":
                    Console.BackgroundColor = ConsoleColor.DarkMagenta;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "5":
                    Console.BackgroundColor = ConsoleColor.Green;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "6":
                    Console.BackgroundColor = ConsoleColor.Yellow;
                    Console.ForegroundColor = ConsoleColor.Black;
                    return this;
                case "7":
                    Console.BackgroundColor = ConsoleColor.DarkBlue;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "8":
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                    Console.ForegroundColor = ConsoleColor.White;
                    return this;
                case "0":
                    return _parentUI;
                default:
                    Console.WriteLine("Please chose a provided color");
                    return this;
            }
        }
    }
}
