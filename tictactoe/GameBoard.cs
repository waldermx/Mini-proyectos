

namespace Board;


public class GameBoard
{
    public GameBoard()
    {
        BoardState = new();
        FieldState player = (FieldState)(Random.Shared.Next(2) + 1); //Selects between 0 and 1, then adds 1 to adjust to players
        SetTurn(player);
        Moves = 0;
    }
    private BoardStorage BoardState; // 32 bits binary
    public int Moves { get; private set; } = 0;
    private void SetTurn(FieldState turn)
    {
        BoardState.SetFieldState(turn, 9);
    }
    public bool IsGameOver => BoardState.GetFieldState(11) != FieldState.Null;

    //todo Hacer más eficiente la reconstrucción del array
    public List<string> GameFields()
    {
        var tokens = new List<string>(9);
        for (byte i = 0; i < 9; i++)
        {
            tokens.Add(
                BoardState.GetFieldState(i) switch
                {
                    FieldState.Null => " ",
                    _ => BoardState.GetFieldState(i).ToString()
                }
            );
        }
        return tokens;
    }
    public FieldState GetTurn()
    {
        return BoardState.GetFieldState(9);
    }
    public void ToggleTurn()
    {
        var currentTurn = GetTurn();

        FieldState nextTurn = (FieldState)(3 - (int)currentTurn);
        BoardState.CleanFieldState(9); //clean bits
        // Assign turn bits
        BoardState.SetFieldState(nextTurn, 9);
    }
    public void MakeMove(FieldState player, byte position)
    {
        if (BoardState.GetFieldState(position) != FieldState.Null && position < 9)
            throw new InvalidOperationException($"Token in {position} is already used."); // can't place token on an occupied field
        BoardState.SetFieldState(player, position);

        Moves += 1;
    }


}

