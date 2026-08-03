#include <stdlib.h>
#include <math.h>

// Definimos la estructura de una partícula.
// Esta MISMA estructura la replicaremos en C#.
typedef struct {
    float x, y;
    float vx, vy;
    char simbolo;
} Particula;

typedef struct {
    Particula* particulas;
    int cantidad;
    int ancho;
    int alto;
} SistemaParticulas;

// Exportación multiplataforma para el compilador
#ifdef _WIN32
    #define EXPORT __declspec(dllexport)
#else
    #define EXPORT __attribute__((visibility("default")))
#endif

// Crea el sistema y reserva memoria nativa (Heap del sistema operativo, fuera del GC de .NET)
EXPORT SistemaParticulas* CrearSistema(int cantidad, int ancho, int alto) {
    SistemaParticulas* sys = (SistemaParticulas*)malloc(sizeof(SistemaParticulas));
    sys->cantidad = cantidad;
    sys->ancho = ancho;
    sys->alto = alto;
    sys->particulas = (Particula*)malloc(sizeof(Particula) * cantidad);

    char simbolos[] = {'E', 'M', 'I', 'L', 'I', 'O'};


    for (int i = 0; i < cantidad; i++) {
        // Inicializamos todas las partículas en el centro
        sys->particulas[i].x = ancho * ((float)rand() / RAND_MAX);
        sys->particulas[i].y = alto;
        
        // Asignamos una velocidad angular aleatoria (dirección circular)
        // float angulo = ((float)rand() / RAND_MAX) * 2.0f * 3.14159f;
        float fuerza = 0.1f + ((float)rand() / RAND_MAX) * 1.0f;

        // sys->particulas[i].vx = cosf(angulo) * fuerza;
        // sys->particulas[i].vy = sinf(angulo) * fuerza * 0.5f; // Factor 0.5 para compensar la simetría de texto
        sys->particulas[i].simbolo = simbolos[rand() % 6];
    }

    return sys;
}

// Actualiza las posiciones aplicando física básica (fuerza + gravedad)
EXPORT void ActualizarSistema(SistemaParticulas* sys) {
    float gravedad = 0.02f;

    for (int i = 0; i < sys->cantidad; i++) {
        Particula* p = &sys->particulas[i];
        
        // p->x += p->vx;
        // p->y += p->vy;
        p->vy += gravedad; // Aplicar gravedad hacia abajo

        // Si rebotan o salen de la pantalla, las re-iniciamos en el centro
        if (p->x <= 0 || p->x >= sys->ancho || p->y <= 0 || p->y >= sys->alto) {
            
            // p->x = sys->ancho / 2.0f;
            // p->y = sys->alto / 2.0f;
            
            // float angulo = ((float)rand() / RAND_MAX) * 2.0f * 3.14159f;
            float fuerza = 0.1f + ((float)rand() / RAND_MAX) * 1.0f;
            // p->vx = cosf(angulo) * fuerza;
            // p->vy = sinf(angulo) * fuerza * 0.5f;
        }
    }
}

// Liberar la memoria asignada manualmente con malloc
EXPORT void DestruirSistema(SistemaParticulas* sys) {
    if (sys) {
        if (sys->particulas) free(sys->particulas);
        free(sys);
    }
}