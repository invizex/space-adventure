using System;

namespace SpaceAdventure
{
    class Program
    {
        //Точка входа в программу
        static void Main(string[] args)
        {
            try
            {
                Console.Clear();
                Console.CursorVisible = false;

                Menu startMenu = new Menu();

                startMenu.ShowStartMenu();
            }
            catch
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("!!!ERROR!!!");
                Console.ReadKey();
                Environment.Exit(0);
            }
        }
    }
}
