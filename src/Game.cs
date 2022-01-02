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

        public void Start(int collspeed) 
        {
            Console.Clear();

            display.CreateMap();      

            Thread move = new Thread(new ThreadStart(Move));
            move.Start();

            Thread update = new Thread(new ThreadStart(display.UpdateMap));
            update.Start();

            Thread collision = new Thread(new ParameterizedThreadStart(Collision));
            collision.Start(collspeed);

            // Главный игровой цикл
            while (isGameAvalible)
            {
                display.PrintMap(player.X, player.Y);
                player.PrintInfo(display.Height);

                player.Score++;
                display.Score = player.Score;

                Thread.Sleep(Speed);
            }

            update.Join(0);
            move.Join(0);
            collision.Join(0);
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

        // Проверка на столкновение игрока с объектом
        void Collision(object collspeed)
        {
            int speed = Convert.ToInt32(collspeed);

            while (isGameAvalible)
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

                Thread.Sleep(speed);
            }
        }
    }
}
