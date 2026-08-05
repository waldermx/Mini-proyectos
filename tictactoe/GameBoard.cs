

namespace Board;


public class GameBoard
{
    public GameBoard()
    {
        _boardState = new();
        FieldState player = (FieldState)(Random.Shared.Next(2) + 1); //Selects between 0 and 1, then adds 1 to adjust to players
        SetTurn(player);
        Moves = 0;
    }
    private readonly BoardStorage _boardState; // 32 bits binary
    public int Moves { get; private set; } = 0;
    private void SetTurn(FieldState turn)
    {
        _boardState.SetFieldState(turn, 9);
    }
    public bool IsGameOver => _boardState.GetFieldState(11) != FieldState.Null;

    //todo Hacer más eficiente la reconstrucción del array
    public List<string> GameFields()
    {
        var tokens = new List<string>(9);
        for (byte i = 0; i < 9; i++)
        {
            tokens.Add(
                _boardState.GetFieldState(i) switch
                {
                    FieldState.Null => " ",
                    _ => _boardState.GetFieldState(i).ToString()
                }
            );
        }
        return tokens;
    }
    public FieldState GetTurn()
    {
        return _boardState.GetFieldState(9);
    }
    public void ToggleTurn()
    {
        var currentTurn = GetTurn();

        var nextTurn = (FieldState)(3 - (int)currentTurn);
        _boardState.CleanFieldState(9); //clean bits
        // Assign turn bits
        _boardState.SetFieldState(nextTurn, 9);
    }
    public void MakeMove(FieldState player, byte position)
    {
        if (_boardState.GetFieldState(position) != FieldState.Null && position < 9)
            throw new InvalidOperationException($"Token in {position} is already used."); // can't place token on an occupied field
        _boardState.SetFieldState(player, position);

        Moves += 1;
    }

    public bool TryToGetWin(out FieldState winner)
    {
        _boardState.CheckWin()
    }


}

