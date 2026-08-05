namespace  tictactoe;

public enum FieldState : byte
{
    Null = 0,
    X = 1,
    O = 2
}

public class BoardStorage
{
    private readonly int[] _playerWinMask = [
        0b010101010101010101,//x
        0b101010101010101010//o
    ];

    private int IntBoard { get; set; } = 0; 
    public FieldState GetFieldState(byte position) =>
    (FieldState)StripZeros(IntBoard & GetFieldMask(position), position);
    public void SetFieldState(FieldState state, //1 para x(01), 2 para o(10)
                        byte position)
    {
        IntBoard |= (int)state << (position * 2);
    }
    public void CleanFieldState(byte position)
    {
        var mask = ~GetFieldMask(position);
        IntBoard &= mask; //Clean turn bits
    }
    private int GetFieldMask(byte position) => 3 << (2 * position); // 11(2*position zeroes) 
    private int StripZeros(int fieldState, byte position) =>
        (fieldState >> (position * 2)) & 3; // the number
    //to be only called on the constructor
    private static readonly int[] WinCasesMasks =
    [
                        //lc        mc          fc    
        //horizontal
        0b00000000000000_00_00_00__00_00_00__11_11_11,
        0b00000000000000_00_00_00__11_11_11__00_00_00,
        0b00000000000000_11_11_11__00_00_00__00_00_00,
        //vertical
        0b00000000000000_00_00_11__00_00_11__00_00_11,
        0b00000000000000_00_11_00__00_11_00__00_11_00,
        0b00000000000000_11_00_00__11_00_00__11_00_00,
        //diagonal
        0b00000000000000_11_00_00__00_11_00__00_00_11,
        0b00000000000000_00_00_11__00_11_00__11_00_00
    ];

    private void SetWin(FieldState winner)
    {
        SetFieldState(winner, 10);
        SetFieldState((FieldState)1, 11);
    }
    private bool IsWinner(FieldState player)
    {
        for (int i = 0; i < WinCasesMasks.Length; i++)
        {
            var playerMask = _playerWinMask[(int)(player) - 1]; //adjust for index

            // 1. Selects winning fields 
            var evaluatedLine = IntBoard & WinCasesMasks[i];
            var playerMoves = WinCasesMasks[i] & playerMask;

            return playerMoves == evaluatedLine;
        }

        return false;
    }
    public bool CheckWin(FieldState player) // playerValue: 1 para X, 2 para O
    {
        if (IsWinner(player))
        {
            SetWin(player);
            return true;
        }
        else return false;
    }
}