using System;
using System.Collections.Generic;

public class Snake
{
    private Pixel head;
    private List<int> bodyPositions;
    private string movement;
    private int score;

    public Snake(int initialX, int initialY)
    {
        head = new Pixel(initialX, initialY, ConsoleColor.Green, "■");
        bodyPositions = new List<int>();
        movement = "RIGHT";
        score = 0;
    }

    public Pixel Head => head;

    public void Draw()
    {
        head.Draw();
        DrawBody();
    }

    public void Move()
    {
        switch (movement)
        {
            case "UP":
                head.YPos--;
                break;
            case "DOWN":
                head.YPos++;
                break;
            case "LEFT":
                head.XPos--;
                break;
            case "RIGHT":
                head.XPos++;
                break;
        }
    }

    public void UpdateBodyPosition()
    {
        bodyPositions.Insert(0, head.XPos);
        bodyPositions.Insert(1, head.YPos);

        // Add the current head position to the body positions
        // This prevents the snake from leaving a trail
        bodyPositions.Insert(2, head.XPos);
        bodyPositions.Insert(3, head.YPos);

        // Remove the last two positions from the body
        bodyPositions.RemoveAt(bodyPositions.Count - 1);
        bodyPositions.RemoveAt(bodyPositions.Count - 1);
    }

    public void CheckCollision()
    {
        if (head.XPos == 0 || head.XPos == Console.WindowWidth - 1 || head.YPos == 0 || head.YPos == Console.WindowHeight - 1)
        {
            GameOver();
        }

        for (int i = 2; i < bodyPositions.Count; i += 2)
        {
            if (head.XPos == bodyPositions[i] && head.YPos == bodyPositions[i + 1])
            {
                GameOver();
            }
        }
    }

    public void ChangeDirection(string newDirection)
    {
        movement = newDirection;
    }

    private void DrawBody()
    {
        Console.ForegroundColor = ConsoleColor.White;
        for (int i = 2; i < bodyPositions.Count; i += 2)
        {
            Console.SetCursorPosition(bodyPositions[i], bodyPositions[i + 1]);
            Console.Write("■");
        }
    }

    private void GameOver()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Red;

        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2);
        Console.WriteLine("Game Over");

        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 1);
        Console.WriteLine("Your Score is: " + bodyPositions.Count / 2);

        Console.SetCursorPosition(Console.WindowWidth / 5, Console.WindowHeight / 2 + 2);
        Console.WriteLine("Press 'R' to restart or any other key to exit.");

        ConsoleKeyInfo keyInfo = Console.ReadKey();

        if (keyInfo.Key == ConsoleKey.R)
        {
            InitializeGame();
        }
        else
        {
            Environment.Exit(0);
        }
    }

    private void InitializeGame()
    {
        head = new Pixel(Console.WindowWidth / 2, Console.WindowHeight / 2, ConsoleColor.Green, "■");
        bodyPositions.Clear();
        movement = "RIGHT";
        score = 0;
    }
}
