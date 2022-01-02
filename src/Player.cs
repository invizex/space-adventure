using System;

namespace SpaceAdventure
{
    class Player
    {
        public int X { get; set; } // Позиция игрока по оси X
        public int Y { get; } // Позиция игрока по оси Y
        public int Health { get; set; } // Здровье
        public int Money { get; set; } // Деньги
        public int MaxHealth { get; set; } // Максимальное здоровье
        public int Score { get; set; } // Игровой счёт

        public Player(int Height)
        {
            X = 0;
            Y = Height - 5;

            Health = 5;
            MaxHealth = 10;
            Money = 0;
            Score = 0;
        }

        // Вывод информации о игроке
        public void PrintInfo(int Height)
        {
            Console.SetCursorPosition(0, Height + 1);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.Write($"Здоровье:    {Health}/{MaxHealth}\t");

            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write($"Деньги:    {Money}\t");

            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write($"Счёт:    {Score}");

            Console.ResetColor();
        }
    }
}
