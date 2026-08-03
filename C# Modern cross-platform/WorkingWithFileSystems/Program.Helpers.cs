partial class Program
{
    private static void SectionTitle(string title)
    {
        WriteLine();
        ConsoleColor previousColor = ForegroundColor;

        ForegroundColor = ConsoleColor.Green;
        WriteLine($"*** {title} ***");
        ForegroundColor = previousColor;

    }
}