using System;
using System.Threading;

namespace SpaceAdventure
{
    class Menu
    {
        delegate void Function(); 

        // Плавный вывод заголовка на экран
        public void PrintTitle(string title)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;

            for(int i = 0; i < title.Length; i++)
            {
                Console.Write(title[i]);

                Thread.Sleep(20);
            }

            Console.ResetColor();
        }

        // Выбор сложности
        public void GetСomplexity()
        {
            string[] options =
            {
                "Лёгкая",
                "Средняя",
                "Сложная",
            };

            int chance = 120;
            int speed = 100;
            int pos = Show("Выберите уровень сложности", options);

            StartGame(chance - 20 * pos, speed - 20 * pos);
        }

        void StartGame(int chance, int speed)
        {
            bool isGameAvalible = true;
            Game game;

            while (isGameAvalible)
            {
                game = new Game(speed, chance);
                game.Start();
                
                isGameAvalible = GameOver();
            }

            Exit();
        }

        public void ShowStartMenu()
        {
            string[] options =
            {
                "Играть",
                "Выйти",
            };

            int length = options.Length;

            Function[] funcs =
            {
                new Function(GetСomplexity),
                new Function(Exit),
            };

            int option = Show(">>>>| Space Adventure |<<<<", options);

            funcs[option]();
        }

        // Передвижение по меню
        int MenuMove(int pos, int max, ConsoleKey key)
        {
            int newPos = pos;

            switch (key)
            {
                case ConsoleKey.UpArrow:
                {
                    if (newPos == 0) newPos = max;
                    else newPos--;
                } 
                    break;

                case ConsoleKey.DownArrow:
                {
                    if (newPos == max) newPos = 0;
                    else newPos++;
                } 
                    break;
            }

            return newPos;
        }

        // Вывод на экран опций меню
        void PrintOptions(string[] options, int position)
        {
            Console.SetCursorPosition(0, 3);

            for (int i = 0; i < options.Length; i++)
            {
                if (position == i) Console.WriteLine($" >  {options[i]}");
                else Console.WriteLine("   " + options[i] + " ");
            }
        }

        // Выбор опций меню
        int Show(string title, string[] options)
        {
            int position = 0;
            bool isEnterPressed = false;

            Console.Clear();
            PrintTitle(title);

            while (!isEnterPressed)
            {
                PrintOptions(options, position);
                ConsoleKey key = Console.ReadKey().Key;

                if (key == ConsoleKey.Enter) isEnterPressed = true;

                else
                {
                    position = MenuMove(position, options.Length - 1, key);
                }
            }

            return position;
        }

        bool GameOver()
        {
            string[] option =
            {
                "Играть снова",
                "Выход",
            };

            if (Show($"<<<<| Игра окончена |>>>>", option) == 0) return true; 

            return false;
        }

        void Exit()
        {
            Environment.Exit(0);
        }
    }
}
