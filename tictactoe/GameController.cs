namespace tictactoe;

public class GameBoard
{
    private readonly BoardStorage _boardStorage = new();

    public FieldState CurrentTurn { get; private set; }
    public FieldState Winner { get; private set; } = FieldState.Null;
    public int Moves { get; private set; } = 0;
    public bool IsGameOver { get; private set; }

    public GameBoard()
    {
        CurrentTurn = (FieldState)(Random.Shared.Next(2) + 1);
    }

    public bool TryMakeMove(byte position)
    {
        if (position >= 9 || _boardStorage.GetFieldState(position) != FieldState.Null || IsGameOver)
        {
            return false;
        }

        _boardStorage.SetFieldState(CurrentTurn, position);
        Moves++;

        // Victory is possible from move 5 (player 1's 3rd move)
        if (Moves >= 5 && _boardStorage.IsWinner(CurrentTurn))
        {
            Winner = CurrentTurn;
            IsGameOver = true;
            return true;
        }

        if (Moves == 9)
        {
            IsGameOver = true;
            return true;
        }

        ToggleTurn();
        return true;
    }

    private void ToggleTurn()
    {
        CurrentTurn = CurrentTurn == FieldState.X ? FieldState.O : FieldState.X;
    }

    public string[] GetBoardRepresentation()
    {
        string[] tokens = new string[9];
        for (byte i = 0; i < 9; i++)
        {
            FieldState state = _boardStorage.GetFieldState(i);
            tokens[i] = state switch
            {
                FieldState.X => "X",
                FieldState.O => "O",
                _ => " "
            };
        }
        return tokens;
    }
}


public class GameController(IRenderer renderer, IUserInput input)
{
    private readonly IRenderer _renderer = renderer;
    private readonly IUserInput _input = input;



    public void StartGameLoop()
    {
        GameBoard game = new();

        while (!game.IsGameOver)
        {
            _renderer.RenderGame(game.GetBoardRepresentation(), game.CurrentTurn.ToString());

            byte position = _input.GetNextMove();

            bool moveSuccessful = game.TryMakeMove(position);

            if (!moveSuccessful)
            {
                _renderer.ShowError($"La posición {position} es inválida o ya está ocupada.");
            }
        }

        // Renderizado final para mostrar la última jugada realizada
        _renderer.RenderGame(game.GetBoardRepresentation(), game.CurrentTurn.ToString());

        // Manejo del resultado final (CORRECCIÓN DE LÓGICA: Evaluamos enums directamente)
        if (game.Winner != FieldState.Null)
        {
            _renderer.ShowMessage($"¡Felicidades jugador {game.Winner}, has ganado!");
        }
        else
        {
            _renderer.ShowMessage("¡Es un empate!");
        }
    }
}