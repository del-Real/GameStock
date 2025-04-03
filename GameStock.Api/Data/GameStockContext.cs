using System;
using GameStock.Api.Entities;
using Microsoft.EntityFrameworkCore;

namespace GameStock.Api.Data;

public class GameStockContext(DbContextOptions<GameStockContext> options)
    : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    public DbSet<Genre> Genres => Set<Genre>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Genre>().HasData(
          new { Id = 1, Name = "Fighting" },
          new { Id = 2, Name = "JRPG" },
          new { Id = 3, Name = "Adventure" },
          new { Id = 4, Name = "Platforms" },
          new { Id = 5, Name = "Action RPG" },
          new { Id = 6, Name = "FPS" },
          new { Id = 7, Name = "Action" },
          new { Id = 8, Name = "Sandbox" },
          new { Id = 9, Name = "Open-world RPG" }
      );
    }
}