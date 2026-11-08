using Microsoft.EntityFrameworkCore;
using ShotTracker.Api.Models;

namespace ShotTracker.Api.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Shot> Shots => Set<Shot>();
}