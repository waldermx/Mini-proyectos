


public interface IRenderer
{
    public void Render(List<string> fields, string turn);
}
public interface IUserInput
{


    byte GetNextMove(); // Debería retornar la posición validada (0-8)

}


public class ConsoleRenderer : IRenderer
{
    public void Render(List<string> fields, string turn)
    {

        string tableroVisual = $"""

        Es turno de {turn}:

        {fields[0]} | {fields[1]} | {fields[2]}

        ---+---+---

        {fields[3]} | {fields[4]} | {fields[5]}

        ---+---+---

        {fields[6]} | {fields[7]} | {fields[8]}

        """;
        Console.Clear();
        Console.WriteLine(tableroVisual);
    }
}
public class ConsoleUserInput : IUserInput
{
    public byte GetNextMove()
    {
        while (true)
        {
            Console.Write("Ingresa una posición (0-8): ");
            string? input = Console.ReadLine();

            if (byte.TryParse(input, out byte position) && position < 9)
            {
                return position;
            }
            Console.WriteLine("Entrada inválida. Intenta de nuevo.");
        }
    }
}
