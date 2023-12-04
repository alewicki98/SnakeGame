using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Game game = new Game();
        game.Run();
    }
}

public class Game
{
    private Snake snake;
    private Obstacle obstacle;
    private int score;

    public Game()
    {
        Console.WindowHeight = 16;
        Console.WindowWidth = 32;
        int screenWidth = Console.WindowWidth;
        int screenHeight = Console.WindowHeight;

        snake = new Snake(screenWidth / 2, screenHeight / 2);
        obstacle = new Obstacle();

        score = 0;
    }

    public void Run()
    {
        Task.Run(async () =>
        {
            while (true)
            {
                await Task.Delay(50);
                obstacle.Draw();
                DrawWalls();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.SetCursorPosition(1, 0);
                Console.WriteLine("Score: " + score);

                snake.Draw();
                obstacle.CheckCollision(snake, ref score);
                snake.Move();
                snake.CheckCollision();
                snake.UpdateBodyPosition();
            }
        });

        while (true)
        {
            if (Console.KeyAvailable)
            {
                var key = Console.ReadKey(true).Key;
                HandleKey(key);
            }
        }
    }

    private static void DrawWalls()
    {
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 0; i < Console.WindowWidth; i++)
        {
            Console.SetCursorPosition(i, 0);
            Console.Write("■");
            Console.SetCursorPosition(i, Console.WindowHeight - 1);
            Console.Write("■");
        }

        for (int i = 0; i < Console.WindowHeight; i++)
        {
            Console.SetCursorPosition(0, i);
            Console.Write("■");
            Console.SetCursorPosition(Console.WindowWidth - 1, i);
            Console.Write("■");
        }
    }

    private void HandleKey(ConsoleKey key)
    {
        switch (key)
        {
            case ConsoleKey.UpArrow:
                snake.ChangeDirection("UP");
                break;
            case ConsoleKey.DownArrow:
                snake.ChangeDirection("DOWN");
                break;
            case ConsoleKey.LeftArrow:
                snake.ChangeDirection("LEFT");
                break;
            case ConsoleKey.RightArrow:
                snake.ChangeDirection("RIGHT");
                break;
        }
    }
}