using System;
using System.Threading;

namespace SpaceAdventure
{
    class Display
    {
        public string[] map; // Карта
        public int Height { get; } // Высота
        public int Width { get; } // Ширина
        int ChanceMeteor { get; } // Шанс спавна метеоритов
        int ChanceHealth { get; } // Шанс спавна жизней
        int ChanceMoney { get; } // Шанс спавна денег
        public char PlayerSymbol { get; } // Символ игрока
        public char MeteorSymbol { get; } // Символ метеорита
        public char MoneySymbol { get; } // Символ денег
        public char HealthSymbol { get; } // Символ здоровья
        int Speed { get; set; } // Скорость обновления карты
 		public int Score { private get; set; } // Игровой счёт для определения скорости обновления карты
        Random random { get; }

        public Display(int speed, int chance)
        {
            Height = Console.WindowHeight - 3;
            Width = Console.WindowWidth - 4;
            map = new string[Height];
            
            random = new Random();

            ChanceMeteor = chance;
            ChanceHealth = 800;
            ChanceMoney = 800;

            PlayerSymbol = '▲';
            HealthSymbol = '♥';
            MoneySymbol = '♦';
            MeteorSymbol = '|';
            Speed = speed;
        }

        // Генерация карты (вызвается один раз)
        public void CreateMap()
        {
            for (int i = 0; i < Height; i++)
            {
                map[i] = NewRow();
            }
        }

        // Вывод карты на экран
        public void PrintMap(int x, int y)
        {
            Console.SetCursorPosition(0, 0);
            Console.CursorVisible = false;

            for (int i = 0; i < map.Length; i++)
            {
                Console.WriteLine(map[i]);
            }

            Console.SetCursorPosition(x, y);
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(PlayerSymbol);
            Console.ResetColor();
        }

        // Обновление карты
        public void UpdateMap()
        {
            while(true)
            {
            	if(Score % 1000 == 0 && Speed != 5) Speed--;

                for (int i = Height - 2; i > 0; i--)
                {
                    map[i + 1] = map[i];
                    map[i] = map[i - 1];
                }

                map[0] = NewRow();

                Thread.Sleep(Speed);
            }
        }

        // Создание нового ряда
        string NewRow()
        {
            string newString = "";

            for (int i = 0; i < Width; i++)
            {
                if (random.Next(ChanceMeteor) == 1) newString += MeteorSymbol;
                else if (random.Next(ChanceHealth) == 1) newString += HealthSymbol;
                else if (random.Next(ChanceMoney) == 1) newString += MoneySymbol;
                else newString += ' ';
            }

            return newString;
        }
    }
}
