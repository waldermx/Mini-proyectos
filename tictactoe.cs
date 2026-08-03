
GameBoard game = new();
game.PlaceMove(FieldState.X, 3);
game.PlaceMove(FieldState.O, 2);



// 1. Obtienes visualmente el caracter de cada una de tus 9 posiciones
string c0 = game.GetMove(0);
string c1 = game.GetMove(1);
string c2 = game.GetMove(2);
string c3 = game.GetMove(3);
string c4 = game.GetMove(4);
string c5 = game.GetMove(5);
string c6 = game.GetMove(6);
string c7 = game.GetMove(7);
string c8 = game.GetMove(8);


// 2. Usas las tres comillas combinadas con el signo '$' para meter las variables
string tableroVisual = $"""
	Tablero Actual:
	   {c0} | {c1} | {c2} 
	  ---+---+---
	   {c3} | {c4} | {c5} 
	  ---+---+---
	   {c6} | {c7} | {c8} 
	""";

Console.WriteLine(tableroVisual);

// Expresión switch compacta para traducir el texto

enum FieldState : byte
{
    Null = 0,
    X = 1,
    O = 2
}

ref struct GameBoard
{
    public int _boardState {get; private set; } // 32 bits binary
    private readonly int GetFieldMask(byte position) => 3 << (2*position); // 11(2*position zeroes) 
    private readonly FieldState GetFieldState(byte position) => 
        (FieldState)StripZeros(_boardState & GetFieldMask(position), position);
    private readonly byte StripZeros(int fieldState, byte position) => 
        (byte)((fieldState >> (position * 2)) & 3); // the number
    public void PlaceMove(FieldState player, //1 para x(01), 2 para o(10)
                        byte position)
    {
        if(GetFieldState(position) != 0) 
            throw new ArgumentOutOfRangeException(nameof(position), "Token was not empty"); // can't place token on an occupied field
        _boardState |= (byte)player << (position * 2);
    }
    public string GetMove(byte position)
    {
        var state = GetFieldState(position);
        return state switch
        {
          FieldState.Null => " ",
          _ => state.ToString()  
        };
    }
}




