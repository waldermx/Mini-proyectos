

namespace Board;
public enum FieldState : byte
{
    Null = 0,
    X = 1,
    O = 2
}

public class GameBoard
{
    public GameBoard()
    {
        BoardState = 0;
        FieldState player = (FieldState)(Random.Shared.Next(2) + 1); //Selects between 0 and 1, then adds 1 to adjust to players
        SetFieldState(player, 9);//set turn
    }
    public int BoardState { get; private set; } // 32 bits binary
    private int GetFieldMask(byte position) => 3 << (2 * position); // 11(2*position zeroes) 
    private int StripZeros(int fieldState, byte position) =>
        (fieldState >> (position * 2)) & 3; // the number

    //todo Hacer más eficiente la reconstrucción del array
    public List<string> GameFields()
    {
        var tokens = new List<string>(9);
        for (byte i = 0; i < 9; i++)
        {   
            tokens.Add(
                GetFieldState(i) switch
                {
                    FieldState.Null => " ",
                    _ => GetFieldState(i).ToString()
                }
            );
        }
        return tokens;

    }
    public FieldState GetFieldState(byte position) =>
        (FieldState)StripZeros(BoardState & GetFieldMask(position), position);
    public FieldState GetTurn()
    {
        return GetFieldState(9);
    }
    public void ToggleTurn()
    {
        var currentTurn = GetTurn();

        FieldState nextTurn = (FieldState)(3 - (int)currentTurn);

        // Limpiar los bits 18 y 19 (la posición 9) usando la máscara invertida
        int mask9 = ~(3 << (9 * 2));
        BoardState &= mask9;

        // Asignar el nuevo turno
        BoardState |= (int)nextTurn << (9 * 2);
    }
    public void SetFieldState(FieldState state, //1 para x(01), 2 para o(10)
                        byte position)
    {
        if (GetFieldState(position) != FieldState.Null && position<9)
            throw new InvalidOperationException($"Token in {position} is already used."); // can't place token on an occupied field

        BoardState |= (int)state << (position * 2);

        if (position < 9)
        {
            ToggleTurn();
        }
    }
}

