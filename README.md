# Shot Tracker

Aplicación web para registrar tiros de baloncesto desde el móvil y ver el porcentaje de acierto por zona de la pista.

Es un proyecto personal hecho en un fin de semana para practicar un desarrollo full-stack completo: interfaz, API y base de datos.

## Qué hace

- Dibuja una media pista en la que se toca para marcar dónde has tirado.
- Se confirma si el tiro entra o falla y se guarda en la base de datos.
- Muestra todos los tiros guardados (verde = entra, rojo = falla).
- Calcula intentos, aciertos y porcentaje por zona: bajo canasta, media distancia y triple.

## Tecnologías

- **Backend:** C# y ASP.NET Core (minimal API)
- **Base de datos:** SQLite con Entity Framework Core (migraciones)
- **Frontend:** HTML, CSS y JavaScript sin frameworks (`<canvas>`)

## Cómo ejecutarlo

Requisitos: SDK de .NET y la herramienta `dotnet-ef`.

```bash
dotnet tool install --global dotnet-ef
cd backend/ShotTracker.Api
dotnet ef database update
dotnet run
```

Abre en el navegador la dirección que indica la consola (`http://localhost:XXXX`).

## API

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/ping` | Comprobación de que la API responde |
| GET | `/shots` | Lista todos los tiros |
| POST | `/shots` | Guarda un tiro (`x`, `y`, `made`) |
| GET | `/stats` | Intentos, aciertos y porcentaje por zona |

## Decisiones de diseño

- **Coordenadas normalizadas (0 a 1):** un tiro no se guarda en píxeles, sino como proporción de la pista. Así es independiente del tamaño de pantalla.
- **Minimal API en lugar de controladores:** menos ficheros y suficiente para este tamaño de proyecto.
- **SQLite:** no requiere instalar nada. Al usar EF Core, pasar a SQL Server o Azure SQL implicaría cambiar el paquete y la cadena de conexión.
- **Frontend servido por la propia API (`wwwroot`):** un solo proceso y sin configurar CORS.
- **Validación en el servidor:** la API rechaza coordenadas fuera de rango y es ella quien asigna el id y la fecha.
- **Zonas por distancia a la canasta:** bajo canasta (< 0,2), media distancia (< 0,45) y triple (el resto).

## Limitaciones conocidas

- La línea del triple es circular, no la real con tramos rectos en las esquinas.
- La posición de la canasta y los radios de las zonas están duplicados en C# y JavaScript.
- Las estadísticas se calculan en memoria, lo que vale para pocos miles de tiros, pero no escalaría a millones.
- La API no tiene autenticación y no hay tests automáticos.
- No está desplegado: se ejecuta en local.

## Posibles mejoras

- Test unitarios de la clasificación por zonas.
- Deshacer el último tiro.
- Mapa de calor en lugar de puntos.
- Despliegue en Azure App Service con Azure SQL.