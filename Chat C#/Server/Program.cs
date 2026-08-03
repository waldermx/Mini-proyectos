
var builder = WebApplication.CreateBuilder(args);

// Aquí agregas tus servicios (Inyección de dependencias, Base de datos, etc.)
// builder.Services.AddControllers(); 

var app = builder.Build();

// Configura las rutas (endpoints) de tu servidor
app.MapGet("/", () => "¡Tu aplicación de consola ahora es un servidor web!");

app.MapGet("/api/estado", () => new { estado = "Activo", fecha = DateTime.Now });

// Esto inicia el servidor y escucha las peticiones HTTP
app.Run();
