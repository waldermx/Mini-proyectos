


public interface IRenderer
{
    public void RenderGame(List<string> fields, string turn);
    public void ShowError(string error);
    public void ShowMessage(string message);

}
public interface IUserInput
{


    byte GetNextMove(); // Debería retornar la posición validada (0-8)

}


public class ConsoleRenderer : IRenderer
{
    public void RenderGame(List<string> fields, string turn)
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
    public void ShowError(string error)
    {
        Console.WriteLine("The following error occurred: {0}", error);
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
            //todo quitar los console wl para no depender de la clase console
            Console.Write("Ingresa una posición (0-8): ");
            string? input = Console.ReadLine();

            if (byte.TryParse(input, out byte position) && position < 9)
            {
                return position;
            }
            //todo quitar los console wl para no depender de la clase console

            Console.WriteLine("Entrada inválida. Intenta de nuevo.");
        }
    }
}
