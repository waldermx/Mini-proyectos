using System.Diagnostics;
using System.Net;

class Program
{
    static async Task Main(string[] args)
    {
        string redirectUri = "http://localhost:5005/oauth-callback/";
        
        // 1. Configurar el "oído" en el SO
        using var listener = new HttpListener();
        listener.Prefixes.Add(redirectUri);
        listener.Start();
        Console.WriteLine("Escuchando en el puerto 5005... El SO mantiene el puerto abierto.");

        // 2. Simular el login abriendo el navegador hacia un "servidor de auth" simulado
        // (Para pruebas, puedes mandarlo a una URL que tú controles o a una página cualquiera)
        string authUrl = "https://httpbin.org/get?info=SimulandoLogin_HazClickEnElLinkDeAbajo";
        Console.WriteLine($"Abriendo navegador... Por favor ve a: {redirectUri}?code=MiTokenSecreto123");
        
        // Ejecución en OS (Funciona en Linux, macOS y Windows si UseShellExecute = true)
        Process.Start(new ProcessStartInfo(authUrl) { UseShellExecute = true });
        // Abre también el link local para simular la redirección del servidor
        Process.Start(new ProcessStartInfo($"{redirectUri}?code=MiTokenSecreto123") { UseShellExecute = true });

        // 3. El Kernel despierta a la app cuando llega el tráfico de loopback
        HttpListenerContext context = await listener.GetContextAsync();
        HttpListenerRequest request = context.Request;

        // Leer el token/código que envió el navegador
        string code = request.QueryString["code"];
        Console.WriteLine($"\n¡Éxito! Token recibido del sistema operativo: {code}");

        // 4. Responderle al navegador para que el usuario sepa que terminó
        HttpListenerResponse response = context.Response;
        string responseString = "<html><body><h1>Autenticacion Completada. Ya puedes cerrar esta ventana.</h1></body></html>";
        byte[] buffer = System.Text.Encoding.UTF8.GetBytes(responseString);
        response.ContentLength64 = buffer.Length;
        await response.OutputStream.WriteAsync(buffer, 0, buffer.Length);
        
        response.OutputStream.Close();
        listener.Stop(); // El puerto se libera en el SO
        Console.WriteLine("Puerto liberado. Fin del programa.");
    }
}