using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace SimuladorNativo;

// 1. Mapeo de estructuras: Deben coincidir exactamente byte a byte con las de C
[StructLayout(LayoutKind.Sequential)]
public struct Particula
{
    public float X;
    public float Y;
    public float Vx;
    public float Vy;
    public byte Simbolo; // 'char' en C equivale a 'byte' (ASCII) en C#
}

[StructLayout(LayoutKind.Sequential)]
public unsafe struct SistemaParticulas
{
    public Particula* Particulas; // Puntero nativo al arreglo de partículas
    public int Cantidad;
    public int Ancho;
    public int Alto;
}

public static partial class MotorNativo
{
    // 2. Importación de la librería nativa usando P/Invoke
    // .NET resolverá automáticamente la extensión según el SO:
    // En Linux/WSL buscará "libengine.so", en Windows buscará "engine.dll"
    private const string LibraryName = "engine";

    [LibraryImport(LibraryName, EntryPoint = "CrearSistema")]
    public static partial IntPtr CrearSistema(int cantidad, int ancho, int alto);

    [LibraryImport(LibraryName, EntryPoint = "ActualizarSistema")]
    public static partial void ActualizarSistema(IntPtr sistemaPtr);

    [LibraryImport(LibraryName, EntryPoint = "DestruirSistema")]
    public static partial void DestruirSistema(IntPtr sistemaPtr);
}

class Program
{
    static unsafe void Main(string[] args)
    {
        Console.CursorVisible = false;
        int ancho = 80;
        int alto = 25;
        int numParticulas = 20;


        // 3. Llamada a C para reservar memoria e inicializar las partículas
        IntPtr sysPtr = MotorNativo.CrearSistema(numParticulas, ancho, alto);

        if (sysPtr == IntPtr.Zero)
        {
            Console.WriteLine("Error al asignar memoria en la biblioteca nativa.");
            return;
        }

        // Convertimos el IntPtr a un puntero tipado C# para leer la memoria directamente
        SistemaParticulas* sys = (SistemaParticulas*)sysPtr;

        // Buffer de pantalla local en C# para renderizar sin parpadeo (Double Buffering)
        char[,] buffer = new char[alto, ancho];

        Console.Clear();
        Console.WriteLine("=== MOTOR FÍSICO C + C# MULTIPLATAFORMA ===");
        Console.WriteLine("Presiona CTRL+C para salir...");
        Thread.Sleep(1000);

        try
        {
            while (true)
            {
                // A. Le pedimos a C que ejecute los cálculos físicos
                MotorNativo.ActualizarSistema(sysPtr);

                // B. Limpiamos nuestro buffer de pantalla local
                for (int y = 0; y < alto; y++)
                    for (int x = 0; x < ancho; x++)
                        buffer[y, x] = ' ';

                // C. Leemos directamente la memoria RAM administrada por C a través de punteros
                for (int i = 0; i < sys->Cantidad; i++)
                {
                    Particula p = sys->Particulas[i];

                    int px = (int)p.X;
                    int py = (int)p.Y;

                    // Dibujamos solo si está dentro de los límites
                    if (px >= 0 && px < ancho && py >= 0 && py < alto)
                    {
                        buffer[py, px] = (char)p.Simbolo;
                    }
                }

                // D. Imprimimos el buffer en consola
                Console.SetCursorPosition(0, 0);
                for (int y = 0; y < alto; y++)
                {
                    for (int x = 0; x < ancho; x++)
                    {
                        char c = buffer[y, x];
                        if (c != ' ')
                        {
                            // Colores ANSI
                            Console.Write($"\u001b[38;5;208m{c}\u001b[0m"); // Color naranja
                        }
                        else
                        {
                            Console.Write(' ');
                        }
                    }
                    Console.WriteLine();
                }

                Thread.Sleep(30); // ~30 FPS
            }
        }
        finally
        {
            // E. IMPORTANTE: Siempre liberar la memoria no administrada
            MotorNativo.DestruirSistema(sysPtr);
            Console.CursorVisible = true;
        }
    }
}