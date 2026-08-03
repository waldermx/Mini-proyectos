
using TicTacToe;


var renderer = new ConsoleRenderer();
var input = new ConsoleUserInput();
Game game = new(renderer, input);




while (true)
{
    game.Render();
    game.ReadPlayersMove();
}







