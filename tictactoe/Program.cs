
using Board;



var renderer = new ConsoleRenderer();
var input = new ConsoleUserInput();
GameBoard game = new();


bool IsGameOver = false;

while (!IsGameOver)
{
    renderer.RenderGame(game.GameFields(), game.GetTurn().ToString());
    try
    {
        game.MakeMove(game.GetTurn(), input.GetNextMove());
    }
    catch { }

    if (game.Moves > 5) game.CheckWin();

    if(game.Winner is not null)
    {
        renderer.ShowMessage("Congrats player {0}, you have won.", game.Winner);
        renderer.ShowMessage("rematch?");

    }

}

Console.WriteLine("GoodBye");






