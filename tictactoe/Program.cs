using tictactoe;

// 1. Instantiate infrastructure adapters
IRenderer renderer = new ConsoleRenderer();
IUserInput input = new ConsoleUserInput();

// 2. Dependency injection into the application layer
GameController controller = new(renderer, input);

// 3. Start the program
controller.StartGameLoop();

Console.WriteLine("Thanks for playing. Goodbye!");