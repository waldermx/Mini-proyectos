namespace tictactoe;

public enum FieldState : byte
{
    Null = 0,
    X = 1,
    O = 2
}

public class BoardStorage
{
    private static readonly int[] WinCasesMasks =
    [
        // Horizontals
        0b000000_000000_111111,
        0b000000_111111_000000,
        0b111111_000000_000000,
        // Verticals
        0b000011_000011_000011,
        0b001100_001100_001100,
        0b110000_110000_110000,
        // Diagonals
        0b110000_001100_000011,
        0b000011_001100_110000
    ];

    private static readonly int[] PlayerWinMasks =
    [
        0b010101_010101_010101, // X (01)
        0b101010_101010_101010  // O (10)
    ];

    public int IntBoard { get; private set; } = 0;

    public FieldState GetFieldState(byte position)
    {
        int mask = GetFieldMask(position);
        int shift = position * 2;
        return (FieldState)((IntBoard & mask) >> shift);
    }

    public void SetFieldState(FieldState state, byte position)
    {
        IntBoard |= (int)state << (position * 2);
    }

    public bool IsWinner(FieldState player)
    {
        if (player == FieldState.Null) return false;

        int playerMask = PlayerWinMasks[(int)player - 1];


        foreach (int winMask in WinCasesMasks)
        {
            int evaluatedLine = IntBoard & winMask;
            int targetLine = winMask & playerMask;

            if (evaluatedLine == targetLine)
            {
                return true; // A winning line was found
            }
        }

        return false;
    }

    private static int GetFieldMask(byte position) => 3 << (position * 2);
}