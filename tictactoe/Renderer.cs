namespace tictactoe;

public interface IRenderer
{
    void RenderGame(string[] fields, string turn);
    void ShowError(string error);
    void ShowMessage(string message);
}

public interface IUserInput
{
    byte GetNextMove();
}

public class ConsoleRenderer : IRenderer
{
    public void RenderGame(string[] fields, string turn)
    {
        string visualBoard = $"""

        It's {turn}'s turn:

        {fields[0]} | {fields[1]} | {fields[2]}
        ---+---+---
        {fields[3]} | {fields[4]} | {fields[5]}
        ---+---+---
        {fields[6]} | {fields[7]} | {fields[8]}

        """;
        Console.Clear();
        Console.WriteLine(visualBoard);
    }

    public void ShowError(string error)
    {
        Console.WriteLine($"[ERROR]: {error}");
        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
    }

    public void ShowMessage(string message)
    {
        Console.WriteLine(message);
    }
}

public class ConsoleUserInput : IUserInput
{
    public byte GetNextMove()
    {
        while (true)
        {
            Console.Write("Enter a position (0-8): ");
            string? input = Console.ReadLine();

            if (byte.TryParse(input, out byte position) && position < 9)
            {
                return position;
            }

            Console.WriteLine("Invalid input. Must be a number between 0 and 8.");
        }
    }
}