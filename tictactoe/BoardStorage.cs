
public enum FieldState : byte
{
    Null = 0,
    X = 1,
    O = 2
}

public class BoardStorage
{
    public int IntBoard { get; private set; } = 0; 
    public FieldState GetFieldState(byte position) =>
    (FieldState)StripZeros(IntBoard & GetFieldMask(position), position);
    public void SetFieldState(FieldState state, //1 para x(01), 2 para o(10)
                        byte position)
    {
        IntBoard |= (int)state << (position * 2);
    }
    public void CleanFieldState(byte position)
    {
        int mask = ~GetFieldMask(position);
        IntBoard &= mask; //Clean turn bits
    }
    private int GetFieldMask(byte position) => 3 << (2 * position); // 11(2*position zeroes) 
    private int StripZeros(int fieldState, byte position) =>
        (fieldState >> (position * 2)) & 3; // the number
    //to be only called on the constructor
    public static readonly int[] WinCasesMasks =
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
    public bool CheckWinFor(int playerValue) // playerValue: 1 para X, 2 para O
    {
        for (int i = 0; i < WinCasesMasks.Length; i++)
        {
            int mask = WinCasesMasks[i];

            // 1. Calculas el patrón exacto que tendría la línea si este jugador la completó
            int expectedWinPattern = mask * playerValue;

            // 2. Aislas la línea en el tablero y comparas directamente
            return (IntBoard & mask) == expectedWinPattern;
        }
        return false;
    }
}