using System;
using System.Threading;

namespace SpaceAdventure
{
    class Game
    {
        Display display { get; set; }
        Player player { get; set; }
        int Speed { get; set; } // Скорость обновления экрана
        bool isGameAvalible { get; set; } // Запущена ли игра

        public Game(int speed, int chance)
        {
            isGameAvalible = true;
            Speed = 10;
            display = new Display(speed, chance);
            player = new Player(display.Height);
        }

        public void Start() 
        {
            Console.Clear();

            display.CreateMap();      

            Thread move = new Thread(new ThreadStart(Move));
            move.Start();

            Thread update = new Thread(new ThreadStart(display.UpdateMap));
            update.Start();

            // Главный игровой цикл
            while (isGameAvalible)
            {
                display.PrintMap(player.X, player.Y);
                player.PrintInfo(display.Height);

                player.Score++;
                display.Score = player.Score;
              
                isCollision();

                Thread.Sleep(Speed);
            }

            update.Join(0);
            move.Join(0);
        }

        // Проверка на столкновение игрока с объектом
        void isCollision()
        {
            if (display.map[player.Y][player.X] == display.MeteorSymbol)
            {
                if (player.Health == 1) isGameAvalible = false;
                else player.Health--;
            }

            else if (display.map[player.Y][player.X] == display.MoneySymbol)
            {
                player.Money++;
            } 
                
            else if (display.map[player.Y][player.X] == display.HealthSymbol && 
                     player.Health != player.MaxHealth)
            {
                player.Health++;
            }
        }

        // Вывод информации при проигрыше
        void GameOver()
        {
            Console.Clear();

            SlowTextPrint("►◄►◄█ GAME OVER █►◄►◄\n\n");
            SlowTextPrint($"Счёт:      {player.Score}\n");
            SlowTextPrint($"Деньги:    {player.Money}");

            Console.CursorVisible = true;
            Console.ReadKey();
            Environment.Exit(0);
        }

        // Плавный вывод текста 
        void SlowTextPrint (string text)
        { 
            for (int i = 0; i < text.Length; i++)
            {
                Thread.Sleep(80);
                Console.Write(text[i]);
            }
        }

        // Передвижение игрока (только по оси X)
        void Move()
        {
            while (isGameAvalible)
            {
                switch (Console.ReadKey().Key)
                {
                    case ConsoleKey.LeftArrow:
                        player.X--;
                        break;

                    case ConsoleKey.RightArrow:
                        player.X++;
                        break;

                    case ConsoleKey.Q:
                        isGameAvalible = false;
                        break;
                }

                if (player.X < 0) player.X++;
                else if (player.X == display.Width) player.X--; 
            }
        }
    }
}
