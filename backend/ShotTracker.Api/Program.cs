using Microsoft.EntityFrameworkCore;
using ShotTracker.Api.Data;
using ShotTracker.Api.Models;
using ShotTracker.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("Default")));

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();
app.UseHttpsRedirection();

app.MapGet("/ping", () => "pong");

app.MapGet("/shots", async (AppDbContext db) =>
    await db.Shots.ToListAsync());

app.MapPost("/shots", async (Shot shot, AppDbContext db) =>
{
    if (shot.X < 0 || shot.X > 1 || shot.Y < 0 || shot.Y > 1)
        return Results.BadRequest("X i Y han d'estar entre 0 i 1.");

    shot.Id = 0;
    shot.CreatedAt = DateTime.UtcNow;

    db.Shots.Add(shot);
    await db.SaveChangesAsync();

    return Results.Created($"/shots/{shot.Id}", shot);
});

app.MapGet("/stats", async (AppDbContext db) =>
{
    var shots = await db.Shots.ToListAsync();

    var byZone = shots
        .GroupBy(s => ZoneClassifier.GetZone(s.X, s.Y))
        .Select(g => new
        {
            zone = g.Key,
            attempts = g.Count(),
            made = g.Count(s => s.Made),
            percentage = Math.Round(100.0 * g.Count(s => s.Made) / g.Count(), 1)
        });

    return Results.Ok(byZone);
});

app.Run();