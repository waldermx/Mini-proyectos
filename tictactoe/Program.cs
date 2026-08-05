
using tictactoe;

var renderer = new ConsoleRenderer();
var input = new ConsoleUserInput();
GameBoard game = new(renderer, input);


bool isGameOver = false;

while (!isGameOver)
{
    game.Render();

    game.MakeMove(input.GetNextMove());

    if(game.Winner is not null)
    {
        renderer.ShowMessage($"Congrats player {game.Winner}, you have won.");
        renderer.ShowMessage("rematch?");

    }

}

Console.WriteLine("GoodBye");






