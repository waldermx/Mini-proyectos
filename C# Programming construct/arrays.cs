string[] historial = { "Login", "Update_Profile", "Checkout", "Logout" };

// Mucho más limpio y directo de leer
string ultimoPaso = historial[^1]; // "Logout"
string penultimoPaso = historial[^2]; // "Checkout"


Console.WriteLine(ultimoPaso);
Console.WriteLine(penultimoPaso);

// static void SystemArrayFunctionality()
// {
//     string[] gothicBands = {"Tones on Tail", "Bauhaus", "Sisters of Mercy"};

//     Console.WriteLine("-> Array:");

//     for (int i = 0; i < gothicBands.Length; i++)
//     {
//         Console.WriteLine(gothicBands[i]+", ");

//     }
//     Console.WriteLine("\n");

//     Array.Reverse(gothicBands);
//     Console.WriteLine("reversed array");

//     for (int i = 0; i < gothicBands.Length; i++)
//     {
//         Console.Write(gothicBands[i] + ", ");
//     }
//     Console.WriteLine("\n");

//     Console.WriteLine("cleared");
//     array
// }


// int precio = 250;

// Console.WriteLine(precio.MoneyFormat());

// public static class Monedas
// {
//     public static string MoneyFormat(this int cantidad)
//     {
//         return $"${cantidad}.00 MXN";
//     }
// }




// string saludo = "hola";

// saludo.ImprimirConTerminador("-");

// public static class MisExtensiones
// {
//     public static void ImprimirConTerminador(this string texto, string terminador)
//     {
//         Console.Write($"{texto}{terminador}");
//     }
// }


// string[] array = ["hello", "sir"];

// Array.ForEach(array, item => Console.Write(item));


// int[] array = new int[4] {1,2,3,4};

// int[] newarray = new int[50];

// array.CopyTo(newarray);

// Console.WriteLine(string.Join(" ", newarray));

// string[] arrayStrings = {"hola", "como", "estas"};



// Console.WriteLine(String.Join(" ", arrayStrings));

// Array.Clear(arrayStrings);

// Console.WriteLine(String.Join(" ", arrayStrings));


// Point punto = new(4,6);
// Console.WriteLine(punto.GetHashCode());

// Point punto2 = new(4,6);
// Console.WriteLine(punto2.GetHashCode());

// public class Point(int x, int y)
// {
//     public override int GetHashCode()
//     {
//         return HashCode.Combine(x,y);
//     }

// }


// class Program
// {
//     static void Main()
//     {
//         int[,] matriz =
//         {
//             {5,12,105,7},
//             {20,1,45,18},
//             {9, 88, 12,3}
//         };

//         for (int i = 0; i< matriz.GetLength(0); i++)
//         {
//             for (int j = 0; j < matriz.GetLength(1); j++)
//             {
//                 Console.Write(matriz[i,j]+"\t");
//             }
//         Console.WriteLine();
//         }
//     }
// }

// int[][] jagged = new int[3][];

// jagged[0] = [1];

// int[] array1 = jagged[0]; //asignamos el primer array de la matriz 

// array1[0] = 5;

// Console.WriteLine(array1[0]);
// Console.WriteLine(jagged[0][0]);

// JaggedMultidimensional();

// static void JaggedMultidimensional()
// {
//     Console.WriteLine("Multidimensional array");

//     int[][] myJagArray = new int[5][];

//     for (int i = 0; i< myJagArray.Length; i++)
//     {
//         myJagArray[i] = new int[i + 7];
//     }

//     for(int i = 0; i < 5; i++)
//     {
//         for(int j = 0; j < myJagArray[i].Length; j++)
//         {
//             Console.Write(myJagArray[i][j] + " ");
//         }
//         Console.WriteLine();
//     }
//     Console.WriteLine();
// }

// RectMultidimensionalArray();

// static void RectMultidimensionalArray()
// {
//     int[,] myMatrix;
//     myMatrix = new int[3,4];

//     for(int i = 0; i < 3; i++)
//     {
//         for (int j = 0; j<4; j++)
//         {
//             myMatrix[i, j] = i*j;
//             Console.Write(myMatrix[i,j]+"\t");
//         }
//         Console.WriteLine();
//     }

// }

// ArrayOfObjects();

// static void ArrayOfObjects(){
//     object[] myObjects = new object[4];
//     myObjects[0] = 10;
//     myObjects[1] = false;
//     myObjects[2] = new DateTime(1969, 3, 24);
//     myObjects[3] = "Form and void";

//     foreach (object obj in myObjects)
//     {
//         Console.WriteLine("Type: {0}, Value {1}", obj.GetType(), obj);
//     }
//     Console.WriteLine();
// }

// DeclareImplicitArrays();

// static void DeclareImplicitArrays()
// {
//     var a = new[] {1, 10, 100, 1000};
//     Console.WriteLine("a is a: {0}", a.ToString());

//     var b = new[] {1, 1.5, 2, 2.5};
//     Console.WriteLine("b is a: {0}", b.ToString());

//     var c = new[] {"hello", null, "world"};
//     Console.WriteLine("c is a: {0}", c.ToString());

//     Console.WriteLine();
// }

// ArrayInitialization();

// static void ArrayInitialization()
// {
//     string[] stringArray = ["one", "two", "three"];
//     Console.WriteLine("stringArray has {0} elements", stringArray.Length);

//     bool[] boolArray = [false, false, true];

//     Console.WriteLine("boolArray has {0} elements", boolArray.Length);

//     int[] intArray = [20, 23, 22, 0];

//     Console.WriteLine("intArray has  {0} elements", intArray.Length);
//     Console.WriteLine();
// }

// SimpleArrays();

// static void SimpleArrays()
// {
//     int[] myInts = new int[3];
//     foreach (int i in myInts)
//     {
//         Console.WriteLine(i);
//     }
//     string[] booksOnDotNet = new string[100];


// }