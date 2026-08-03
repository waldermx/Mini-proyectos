using Board;

namespace TicTacToe;


public class Game
{
    private IRenderer _renderer;
    private IUserInput _userInput;
    public GameBoard _board;


    public void ReadPlayersMove()
    {
        // 1. Pide la posición a la interfaz abstraída
        byte position = _userInput.GetNextMove();

        // 2. Aplica el movimiento al tablero con el turno actual
        _board.SetFieldState(_board.GetTurn(), position);
    }
    public void Render()
    {
        _renderer.Render(_board.GameFields(), _board.GetTurn().ToString());
    }

    public Game(IRenderer renderer, IUserInput userInput)
    {
        _renderer = renderer;
        _userInput = userInput;
        _board = new GameBoard();
    }

}