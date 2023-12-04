public class Obstacle
{
    private string character;
    private int xPos;
    private int yPos;

    public Obstacle()
    {
        character = "*";
        Random randomNum = new Random();
        xPos = randomNum.Next(1, Console.WindowWidth);
        yPos = randomNum.Next(1, Console.WindowHeight);
    }

    public void Draw()
    {
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.SetCursorPosition(xPos, yPos);
        Console.Write(character);
    }

    public void CheckCollision(Snake snake, ref int score)
    {
        if (snake.Head.XPos == xPos && snake.Head.YPos == yPos)
        {
            score++;
            xPos = new Random().Next(1, Console.WindowWidth);
            yPos = new Random().Next(1, Console.WindowHeight);
        }
    }
}

public class Pixel
{
    public int XPos { get; set; }
    public int YPos { get; set; }
    public ConsoleColor ScreenColor { get; set; }
    public string Character { get; set; }

    public Pixel(int xPos, int yPos, ConsoleColor screenColor, string character)
    {
        XPos = xPos;
        YPos = yPos;
        ScreenColor = screenColor;
        Character = character;
    }

    public void Draw()
    {
        Console.ForegroundColor = ScreenColor;
        Console.SetCursorPosition(XPos, YPos);
        Console.Write(Character);
    }
}
